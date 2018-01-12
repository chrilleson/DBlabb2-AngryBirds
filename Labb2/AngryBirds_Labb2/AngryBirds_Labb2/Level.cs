using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AngryBirds_Labb2
{
    class Level
    {
        public int LevelID { get; set; }
        [Required]
        public string NameOfLevel { get; set; }
        public int NumberOfBirds { get; set; }
        public virtual ICollection<Score> Scoreboard { get; set; }
    }
}
