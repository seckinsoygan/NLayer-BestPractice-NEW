using System.Net;
using System.Text.Json.Serialization;

namespace Services;
public class ServiceResult<T>
{
    public T? Data { get; init; }
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public bool isSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool isFail => !isSuccess;
    [JsonIgnore] public HttpStatusCode Status { get; set; }

    //static factory methods
    public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult<T> { Data = data, Status = status };
    }
    public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T> { ErrorMessage = errorMessage, Status = status };
    }
    public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T> { ErrorMessage = [errorMessage], Status = status };
    }

}

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore] public bool isSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore] public bool isFail => !isSuccess;
    [JsonIgnore] public HttpStatusCode Status { get; set; }

    //static factory methods
    public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult { Status = status };
    }
    public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult { ErrorMessage = errorMessage, Status = status };
    }
    public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult { ErrorMessage = [errorMessage], Status = status };
    }

}
