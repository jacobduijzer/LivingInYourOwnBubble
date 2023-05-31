using CattleInformationSystem.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CattleInformationSystem.Web.Pages;

public class FarmDetails : PageModel
{
    private readonly IFarmRepository _farms;

    public ToggleHistory History { get; set; } = new ()
    {
        Options = new [] {"Normal", "Include history"}
    };
    
    public bool IsChecked { get; set; }
    public Farm FarmData { get; set; }
    
    public int FarmId { get; set; }
    
    public FarmDetails(IFarmRepository farms)
    {
        _farms = farms;
        History.SelectedOption = History.Options[0];
    }

    public async Task OnGetAsync(int farmId)
    {
        FarmData = await _farms.ById(farmId);
        FarmId = farmId;
    }

    public async Task OnPostAsync(int farmId, bool includeHistory)
    {
        FarmId = farmId;
        IsChecked = includeHistory;
        if (Request.Form["options"] == History.Options[0])
            History.SelectedOption = History.Options[0];
        else
            History.SelectedOption = History.Options[1];
        
        if(History.SelectedOption == History.Options[0])
            FarmData = await _farms.ById(farmId);
        else
            FarmData = await _farms.ByIdWithHistory(farmId);
    }

    
    
    
}