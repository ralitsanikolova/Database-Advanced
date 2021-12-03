using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting
{
    class FootballBettingContext : DbContext
    {
        public FootballBettingContext() { }

        public FootballBettingContext(DbContextOptions<FootballBettingContext> options) : base(options) { }

        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer("Server=.;Database=EntityRelations;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasOne(e => e.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(e => e.GameId);

                entity.HasOne(e => e.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasOne(e => e.HomeTeam)
                .WithMany(p => p.HomeGames)
                .HasForeignKey(e => e.HomeTeamId).
                OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.AwayTeam)
                .WithMany(a => a.AwayGames)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasOne(e => e.Country)
                .WithMany(e => e.Towns)
                .HasForeignKey(e => e.CountryId);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(e => e.PrimaryKitColor)
                .WithMany(p => p.PrimaryKitTeams)
                .HasForeignKey(e => e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SecondaryKitColor)
                .WithMany(s => s.SecondaryKitTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Town)
                .WithMany(t => t.Teams);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(e => e.Position)
                .WithMany(e => e.Players)
                .HasForeignKey(e => e.PositionId);

                entity.HasOne(e => e.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.TeamId);
            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(e => new { e.PlayerId, e.GameId });

                entity.HasOne(e => e.Game)
                .WithMany(e => e.PlayerStatistics)
                .HasForeignKey(e => e.GameId);

                entity.HasOne(e => e.Player)
                .WithMany(e => e.PlayerStatistics)
                .HasForeignKey(e => e.PlayerId);
            });
        }
    }
}
