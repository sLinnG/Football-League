namespace Services
{
    using System.Collections.Generic;
    using Models;
    using Models.DBModels;

    public interface IMatchesService
    {
        List<Match> GetAll();

        bool AddNewMatchToDB(int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored);

        bool EditMatch(int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored);
        
        bool Delete(int id);
        
        Match GetByID(int id);
        
        MatchViewModel GetViewModelByID(int id, List<TeamsComboBoxViewModel> listOfTeams);
        
        MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams);
        
        MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored);
        
        MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams, int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored);
    }
}
