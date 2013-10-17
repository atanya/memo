namespace SuperMemo.Models
{
    public class ResponseObject
    {
        public bool Status { get; set; }
        public object Data { get; set; }

        public ResponseObject(bool status, object data)
        {
            Status = status;
            Data = data;
        }

        public static ResponseObject Success(object data)
        {
            return new ResponseObject(true, data);
        }
        
        public static ResponseObject Failure(object data)
        {
            return new ResponseObject(false, data);
        }
    }
}