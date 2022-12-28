using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackGaming.Models
{
    public class Demande
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Game { get; set; }
        public int? CoachId { get; set; }
        public Coach? Coach { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
