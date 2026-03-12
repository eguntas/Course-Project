using System.ComponentModel.DataAnnotations;

namespace Course.Discount.API.Options
{
    public class MongoOption
    {
        [Required]
        public string ConnectionString { get; set; }
        [Required]
        public string DatabaseName { get; set; }
    }
}
