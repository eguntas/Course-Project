using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Course.Shared
{

    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;



  
    public class ServiceResult
    {
        [JsonIgnore] public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        [JsonIgnore] public bool IsSuccess => Fail is null;
        [JsonIgnore] public bool IsFailure => !IsSuccess;
        public static ServiceResult SuccessAsNoContent() => new ServiceResult { Status = HttpStatusCode.NoContent };
        public static ServiceResult ErrorNoFound() => new ServiceResult
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "The request resource not found"
            }
        };

        public static ServiceResult ErrorFromProblemDetails(Refit.ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Status = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult { Fail = problemDetails, Status = exception.StatusCode };

        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                Fail = problemDetails,
                Status = statusCode
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                Status = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                Status = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation error occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                }
            };
        }

    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        [JsonIgnore] public string? Url { get; set; }
        public static ServiceResult<T> Success(T data) => new ServiceResult<T> { Status = HttpStatusCode.OK, Data = data };

        public static ServiceResult<T> Created(T data, string url) => new ServiceResult<T>
        {
            Status = HttpStatusCode.Created,
            Data = data,
            Url = url
        };

        public new static ServiceResult<T> ErrorFromProblemDetails(Refit.ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Status = exception.StatusCode,
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult<T> { Fail = problemDetails, Status = exception.StatusCode };

        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T> 
            { 
                Fail = problemDetails, 
                Status = statusCode 
            };
        }

        public new static ServiceResult<T> Error(string title,string description ,HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                Status = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                Status = statusCode,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string , object?> errors)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Validation error occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                }
            };
        }


    }

}
