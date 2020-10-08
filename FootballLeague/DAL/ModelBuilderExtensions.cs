namespace DAL
{
    using System.Data.Entity;
    using Models.DBModels;

    internal static class ModelBuilderExtensions
    {
        internal static DbModelBuilder BuildDbModels(this DbModelBuilder builder)
        {
            builder.Entity<Match>()
                .HasKey(m => m.Id);

            builder.Entity<Match>()
                .Property(m => m.HomeTeamId)
                .IsRequired();

            builder.Entity<Match>()
                .Property(m => m.AwayTeamId)
                .IsRequired();

            builder.Entity<Match>()
                .HasRequired(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .WillCascadeOnDelete(false);

            builder.Entity<Match>()
                .HasRequired(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .WillCascadeOnDelete(false);

            builder.Entity<Team>()
                .HasKey(t => t.Id);

            return builder;
        }
    }
}
