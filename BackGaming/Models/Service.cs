using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace BackGaming.Models
{
    public class Service
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float? Price { get; set; }
        public string? DateCeation { get; set; }
        public string? Title { get; set; }
        public string? Game { get; set; }
        public string? Description { get; set; }
        public int? CoachId {get; set; }
        public Coach? Coach { get; set; }
        public ICollection<AchatService>? AchatServices { get; set; }

    }
}
