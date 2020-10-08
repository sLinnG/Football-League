namespace Models
{
    public class TeamsComboBoxViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TeamsComboBoxViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public TeamsComboBoxViewModel()
        {
        }
    }
}
