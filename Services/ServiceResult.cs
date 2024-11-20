using Repositories.Products;
using System.Net;
using System.Text.Json.Serialization;

namespace Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }

        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count() == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public string? UrlAsCreated { get; set; }
      
        public static ServiceResult<T> Success(T data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T> { Data = data, StatusCode = httpStatusCode };
        }
        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
        {
            return new ServiceResult<T> { Data = data, StatusCode = HttpStatusCode.Created,UrlAsCreated = urlAsCreated };
        }

        public static ServiceResult<T> Fail(List<string> message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T> { ErrorMessage = message, StatusCode = httpStatusCode };
        }

        public static ServiceResult<T> Fail(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T> { ErrorMessage = [message], StatusCode = httpStatusCode };
        }
    }

    public class ServiceResult
    {


        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count() == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ServiceResult { StatusCode = httpStatusCode };
        }

        public static ServiceResult Fail(List<string> message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult { ErrorMessage = message, StatusCode = httpStatusCode };
        }

        public static ServiceResult Fail(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult { ErrorMessage = [message], StatusCode = httpStatusCode };
        }
    }
}
