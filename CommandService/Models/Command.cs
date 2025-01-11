using System.ComponentModel.DataAnnotations;

namespace CommandService.Models
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string HowTo { get; set; }

        [Required]
        public string ActionCommand { get; set; }

        public int PlatformId { get; set; }

        public Platform Platform { get; set; }
    }
}
