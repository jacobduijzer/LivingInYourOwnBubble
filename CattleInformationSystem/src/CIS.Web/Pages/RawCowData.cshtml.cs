using CIS.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS.Web.Pages;

public class RawCowDataModel : PageModel
{
    private readonly RawCowDataRepository _rawCowData;
    private readonly ILogger<RawCowDataModel> _logger;

    public RawCowDataModel(RawCowDataRepository rawCowDataRepository, ILogger<RawCowDataModel> logger)
    {
        _rawCowData = rawCowDataRepository;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        await _rawCowData.ProcessRawCowData();
    }
}