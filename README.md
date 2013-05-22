## Memo

.NET service which helps learn english words. It implements SuperMemo SM-2 algorithm and use MS Translator service as dictionary.

#### Environment settings

Web.config file is missed in SuperMemo project, you can create it based on Web.config.template file. If you want to add changes to web.config then update web.config.template as well.

If you want to use MS Translator service then you need to add following parameters to app settings section of your local web.config file:  
\<add key="ClientId" value="" \/\>  
\<add key="ClientSecret" value="" \/\>

Values for these parameters you can get after registering your application in MS Azure Marketplace (see [http://www.microsoft.com/en-us/translator/](http://www.microsoft.com/en-us/translator/) for additional info).

Please, do not commit web.config file, especially with you secret keys.
