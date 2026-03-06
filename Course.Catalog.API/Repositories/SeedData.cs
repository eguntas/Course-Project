using Course.Catalog.API.Features.Categories;
using Course.Catalog.API.Features.Courses;

namespace Course.Catalog.API.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExtension(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new() {Id = NewId.NextSequentialGuid() , Name = "Development" },
                    new() {Id = NewId.NextSequentialGuid() , Name = "Business" },
                    new() {Id = NewId.NextSequentialGuid() , Name = "IT" }

                };
                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Courses.Any())
            {
                var categories = await dbContext.Categories.FirstAsync();
                var randomUserId = NewId.NextSequentialGuid();
                List<Course.Catalog.API.Features.Courses.Course> courses =
                    [
                        new(){
                            Id = NewId.NextSequentialGuid() ,
                            Name = "C#",
                            Description = "C# Course",
                            Price = 100,
                            UserId = randomUserId,
                            Created = DateTime.UtcNow,
                            Feature = new Feature{ Duration=100 , Rating = 5 , EducatorFullName = "C# Educator"},
                            CategoryId = categories.Id

                        },
                         new(){
                            Id = NewId.NextSequentialGuid() ,
                            Name = "Java",
                            Description = "Java Course",
                            Price = 200,
                            UserId = randomUserId,
                            Created = DateTime.UtcNow,
                            Feature = new Feature{ Duration=70 , Rating = 4 , EducatorFullName = "Java Educator"},
                            CategoryId = categories.Id

                        }
                    ];

                dbContext.Courses.AddRange(courses);
                await dbContext.SaveChangesAsync();
               
            }

        }
    }
}
