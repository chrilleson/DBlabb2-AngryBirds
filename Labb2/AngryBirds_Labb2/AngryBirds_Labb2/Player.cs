using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AngryBirds_Labb2
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        [Required]
        public string Username { get; set; }
        public virtual ICollection<Score> Scoreboard { get; set; }
    }

}
