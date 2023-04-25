using System.Net;
using CIS.Application;
using CIS.Domain;
using Refit;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CIS.TestDataApp;

[Binding]
public class FromBirthTillDeath : RefitFixture<ICowPassportApi>
{
    private readonly RefitFixture<ICowPassportApi> _refitFixture;
    private CowDto _cow;
    private IApiResponse _result;

    public FromBirthTillDeath(RefitFixture<ICowPassportApi> refitFixture)
    {
        _refitFixture = refitFixture;
    }

    [Given(@"the cow")]
    public void GivenTheCow(Table table)
    {
        _cow = table.CreateInstance<CowDto>();
    }

    [Given(@"with the following events")]
    public void GivenWithTheFollowingEvents(Table table)
    {
        _cow.Events = table.CreateSet<CowEventDto>().ToList();
    }

    [When(@"sending it to the API")]
    public async Task WhenSendingItToTheApi()
    {
        _result = await _refitFixture
            .GetRestClient("http://localhost:5047")
            .Send(_cow);
    }

    [Then(@"the cow data should be accepted")]
    public void ThenTheCowDataShouldBeAccepted()
    {
        Assert.Equal(HttpStatusCode.Accepted, _result.StatusCode);
    }
}