using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Juan.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:20)]
        public string Title { get; set; }
        [StringLength(maximumLength: 250)]
        public string Desc { get; set; }
        [StringLength(maximumLength: 20)]
        public string Tag { get; set; }
        [StringLength(maximumLength: 100)]
        public string? Image { get; set; }
        public string Button { get; set; }
        public string URL { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
