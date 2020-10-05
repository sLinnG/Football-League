using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamGoalsScored { get; set; }
        public int AwayTeamGoalsScored { get; set; }
        public List<TeamsComboBoxViewModel> Teams { get; set; }
        public MatchViewModel(List<TeamsComboBoxViewModel> teams)
        {
            Teams = teams;
        }
        public MatchViewModel(List<TeamsComboBoxViewModel> teams, int homeTeamId, 
            int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored) : this(teams)
        {
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeTeamGoalsScored = homeTeamGoalsScored;
            AwayTeamGoalsScored = awayTeamGoalsScored;
        }
        public MatchViewModel(List<TeamsComboBoxViewModel> teams, int homeTeamId,
           int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored, int id) 
            : this(teams, homeTeamId,awayTeamId, homeTeamGoalsScored,awayTeamGoalsScored) 
        {
            Id = id;
        }
        public MatchViewModel()
        {
        }
    }
}
