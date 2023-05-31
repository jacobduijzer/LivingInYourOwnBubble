using CattleInformationSystem.Application;
using CattleInformationSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CattleInformationSystem.Web.Pages;

public class IndexModel : PageModel
{
    private readonly AllFarmsHandler _farmsHandler;

    public List<Farm> FarmsData { get; set; }

    public IndexModel(AllFarmsHandler farmsHandler)
    {
        _farmsHandler = farmsHandler;
    }

    public async Task OnGetAsync()
    {
        var farmData = await _farmsHandler.Handler();
        FarmsData = farmData.OrderBy(x => x.FarmType.ToString()).ToList();
    }
}