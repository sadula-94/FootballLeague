using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Web.ViewModels.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
