using Asp.Versioning.Builder;
using Course.File.API.Features.File.Delete;
using Course.File.API.Features.File.Upload;


namespace Course.File.API.Features.File
{
    public static class FileEndpointExtension
    {
        public static void AddFileEndpointExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/files").WithTags("Files").WithApiVersionSet(apiVersion)
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}
