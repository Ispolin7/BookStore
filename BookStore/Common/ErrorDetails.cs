using Newtonsoft.Json;

namespace BookStore.Common
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }

        public ErrorDetails(int code, string message, string source, string trace, string inner)
        {
            this.StatusCode = code;
            this.Message = message;
            this.Source = source;
            this.StackTrace = trace;
            this.InnerException = inner;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
