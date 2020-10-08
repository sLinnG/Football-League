namespace Models.DBModels
{

    using System.Collections.Generic;

    public partial class Team : IIdentifiable
    {
        public Team()
        {
            this.AwayMatches = new HashSet<Match>();
            this.HomeMatches = new HashSet<Match>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }

        public virtual ICollection<Match> AwayMatches { get; set; }

        public virtual ICollection<Match> HomeMatches { get; set; }
    }
}