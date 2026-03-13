using Course.Shared;

namespace Course.File.API.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
    
}
