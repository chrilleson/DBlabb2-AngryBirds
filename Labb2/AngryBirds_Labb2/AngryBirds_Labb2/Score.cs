using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AngryBirds_Labb2
{
    class Score
    {
        [Key]
        public int ScoreID { get; set; }
        public virtual Level Level { get; set; }
        public virtual Player Player { get; set; }
        public int Highscore { get; set; }
    }
}
