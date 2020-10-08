namespace Models.DBModels
{
    public partial class Match : IIdentifiable
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamGoalsScored { get; set; }

        public int AwayTeamGoalsScored { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual Team HomeTeam { get; set; }
    }
}
