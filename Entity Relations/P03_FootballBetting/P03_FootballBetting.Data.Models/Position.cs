using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {

            Players = new HashSet<Player>();
        }
        [Key]
        public int PositionId { get; set; }
        public string Name { get; set; }

        #region Relations
        public ICollection<Player> Players { get; set; }
        #endregion
    }
}
