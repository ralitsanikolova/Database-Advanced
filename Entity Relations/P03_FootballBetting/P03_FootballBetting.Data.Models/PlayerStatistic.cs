namespace P03_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public int ScoredGoals { get; set; }

        public string Assists { get; set; }

        public double MinutesPlayed { get; set; }

        #region Relation
        public Game Game { get; set; }
        public Player Player { get; set; }
        #endregion 
    }
}
