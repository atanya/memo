using System.Configuration;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace SuperMemo.BL
{
    public class Translator : ITranslator
    {
        private class AdmAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string scope { get; set; }
        }

        private const string TranslatorUrl = "http://api.microsofttranslator.com/v2/Http.svc/Translate";
        private const string authUrl = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

        private static string Connect()
        {
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new SecurityException("MS Translator configuration missed");
            }
            var strRequestDetails =
                string.Format(
                    "grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com",
                    HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            var webRequest = WebRequest.Create(authUrl);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            byte[] bytes = Encoding.ASCII.GetBytes(strRequestDetails);
            webRequest.ContentLength = bytes.Length;
            using (var outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            var webResponse = webRequest.GetResponse();
            string text;
            using (var stream = webResponse.GetResponseStream())
            {
                var reader = new StreamReader(stream);
                text = reader.ReadToEnd();
            }
            var token = new JavaScriptSerializer().Deserialize<AdmAccessToken>(text);


            return "Bearer " + token.access_token;
        }

        private string Translate(string header, string word, string fromLang, string toLang)
        {
            var uri = string.Format("{0}?text={1}&from={2}&to={3}", TranslatorUrl, word, fromLang, toLang);

            //string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(txtToTranslate) + "&from=en&to=es";
            
            var translationWebRequest = WebRequest.Create(uri);
            translationWebRequest.Headers.Add("Authorization", header);
            WebResponse response = null;
            response = translationWebRequest.GetResponse();
            var stream = response.GetResponseStream();
            var encode = Encoding.GetEncoding("utf-8");
            var translatedStream = new StreamReader(stream, encode);
            var xTranslation = new System.Xml.XmlDocument();
            xTranslation.LoadXml(translatedStream.ReadToEnd());
            return xTranslation.InnerText;
        }

        private string TranslateFromEnglish(string word)
        {
            return Translate(Connect(), word, "en", "ru");
        }

        private string TranslateFromRussian(string word)
        {
            return Translate(Connect(), word, "ru", "en");
        }

        public string Translate(string word)
        {
            var match = Regex.Match(word, @"[а-яА-Я]", RegexOptions.IgnoreCase);
            return match.Success ? TranslateFromRussian(word) : TranslateFromEnglish(word);
        }
    }
}
