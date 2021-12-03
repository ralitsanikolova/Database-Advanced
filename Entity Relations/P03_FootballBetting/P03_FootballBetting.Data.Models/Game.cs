using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            PlayerStatistics = new HashSet<PlayerStatistic>();
            Bets = new HashSet<Bet>();
        }
        [Key]
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime DateTime { get; set; }

        public int HomeTeamBetRate { get; set; }

        public int AwayTeamBetRate { get; set; }

        public int DrawBetRate { get; set; }

        public int Result { get; set; }

        #region Relations
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
        public ICollection<Bet> Bets { get; set; }
        #endregion
    }
}
