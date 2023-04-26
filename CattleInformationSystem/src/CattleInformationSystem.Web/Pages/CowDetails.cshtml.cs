using CattleInformationSystem.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CattleInformationSystem.Web.Pages;

public class CowDetails : PageModel
{
    private readonly ICowRepository _cows;

    public Cow CowData { get; set; }
    
    public CowDetails(ICowRepository cows)
    {
        _cows = cows;
    }
    
    public async Task OnGetAsync(int cowId)
    {
        CowData = await _cows.ById(cowId);
    }

}