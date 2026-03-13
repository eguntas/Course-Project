using Course.Shared;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Net;

namespace Course.File.API.Features.File.Upload
{
    public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
           if(request.file.Length == 0)
                return ServiceResult<UploadFileCommandResponse>.Error("Invalid file" , "File is empty or null", HttpStatusCode.BadRequest);

           var newFileName = $"{Guid.NewGuid()}_{Path.GetExtension(request.file.FileName)}";
           var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath! , newFileName);

            await using var stream = new FileStream(uploadPath, FileMode.Create);
            
            await request.file.CopyToAsync(stream, cancellationToken);

            var response = new UploadFileCommandResponse(newFileName, $"files/{newFileName}", request.file.FileName);
            
            return ServiceResult<UploadFileCommandResponse>.Created(response, response.FilePath);
        }
    }
}
