namespace DAL
{
    using System.Data.Entity;
    using Models.DBModels;

    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(string connectionStringSettings)
            : base(connectionStringSettings ??
                  "data source=localhost;initial catalog=FootballLeague;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FootballLeagueDbContext>());
        }

        public virtual DbSet<Match> Matches { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            => base.OnModelCreating(modelBuilder.BuildDbModels());
    }
}
