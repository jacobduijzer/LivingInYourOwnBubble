using CIS.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS.Web.Pages;

public class CowsModel: PageModel
{
    private readonly IGetRepository<Cow> _cows;
    private readonly ILogger<RawCowDataModel> _logger;
    
    public List<Cow> Cows { get; private set; }

    public CowsModel(IGetRepository<Cow> cows, ILogger<RawCowDataModel> logger)
    {
        _cows = cows;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        Cows = await _cows.Get(x => true);
    }
}