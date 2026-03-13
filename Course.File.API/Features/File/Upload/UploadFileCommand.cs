using Course.Shared;

namespace Course.File.API.Features.File.Upload
{
    public record UploadFileCommand(IFormFile file):IRequestByServiceResult<UploadFileCommandResponse>;
   
}
