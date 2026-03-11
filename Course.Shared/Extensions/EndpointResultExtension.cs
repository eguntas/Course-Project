using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Course.Shared.Extensions
{
    public static class EndpointResultExtension
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
            return result.Status switch
            {
               HttpStatusCode.OK => Results.Ok(result.Data),
               HttpStatusCode.Created => Results.Created(result.Url ?? string.Empty, result.Data),
               HttpStatusCode.BadRequest => Results.BadRequest(result),
               HttpStatusCode.NoContent => Results.NoContent(),
               HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
               _ => Results.Problem(result.Fail!)
            };
        }

        public static IResult ToGenericResult(this ServiceResult result)
        {
            return result.Status switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };
        }



    }
}
