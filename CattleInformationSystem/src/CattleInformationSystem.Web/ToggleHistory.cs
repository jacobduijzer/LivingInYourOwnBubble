namespace CattleInformationSystem.Web;

public class ToggleHistory
{
    public String[] Options { get; set; }
    public String SelectedOption { get; set; }

    public String GetActive(String option)
    {
        return option == SelectedOption ? "active" : "";
    }
}