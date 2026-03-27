using Course.Bus.Commands;
using Course.Bus.Events;
using MassTransit;
using Microsoft.Extensions.FileProviders;
using System;

namespace Course.File.API.Consumers
{
    public class UploadCoursePictureCommandConsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {
            using var scope = serviceProvider.CreateScope();
            
            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
            var newFileName = $"{Guid.NewGuid()}_{Path.GetExtension(context.Message.FileName)}";
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);
            
            await System.IO.File.WriteAllBytesAsync(uploadPath, context.Message.picture);

            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.courseId, $"files/{newFileName}"));

        }
    }
}
