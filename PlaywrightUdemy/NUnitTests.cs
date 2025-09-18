using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightUdemy;

public class NUnitTests : PageTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        //var pw = await Playwright.CreateAsync();
        //var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //{
        //    Headless = false
        //});
        //var page = await browser.NewPageAsync();

        await Page.GotoAsync("http://www.eaapp.somee.com");
        await Page.ClickAsync("text=Login");

        await Page.FillAsync("#UserName", "admin");
        await Page.FillAsync("#Password", "password");
        await Page.ClickAsync("text=Log in");
        await Expect(Page.Locator("text='Employee Details'")).ToBeVisibleAsync();
    }

}