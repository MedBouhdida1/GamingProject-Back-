
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackGaming.Models
{
    public class AchatService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        public string? Time { get; set; }    
    }
}
