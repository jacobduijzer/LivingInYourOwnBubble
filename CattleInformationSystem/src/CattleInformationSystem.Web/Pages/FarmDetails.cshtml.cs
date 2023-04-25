using CattleInformationSystem.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CattleInformationSystem.Web.Pages;

public class FarmDetails : PageModel
{
    private readonly IFarmRepository _farms;

    public Farm FarmData { get; set; }
    
    public FarmDetails(IFarmRepository farms)
    {
        _farms = farms;
    }

    public async Task OnGetAsync(int farmId)
    {
        FarmData = await _farms.ById(farmId);
    }

    public string DateFormatter(DateTime? dateTime)
    {
        if (dateTime != null)
            return dateTime.Value.ToString("yyyy-MM-dd");

        return string.Empty;
    }

}