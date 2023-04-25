using CIS.Application;
using Refit;

namespace CIS.TestDataApp;

public interface ICowPassportApi
{
    [Post("/cowpassport")]
    Task<IApiResponse> Send([Body]CowDto cowPassport);
}