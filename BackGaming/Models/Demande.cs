using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BackGaming.Models
{
    public class Demande
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Game { get; set; }
        public string DiscordId { get; set; }
        public string Team { get; set; }
        public string IdInGame { get; set; }
        public string RankInGame { get; set; }
        public string Location { get; set; }

        [DefaultValue(0)]
        public int etat  {get; set;}
        public int? CoachId { get; set; }
        public Coach? Coach { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
