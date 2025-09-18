using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightUdemy.Pages;

namespace PlaywrightUdemy;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var pw = await Playwright.CreateAsync();
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        var page1 = context.NewPageAsync();
        var page = await browser.NewPageAsync();

        await page.GotoAsync("http://www.eaapp.somee.com", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });
        await page.ClickAsync("text=Login");
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "Test.jpg"
        });

        await page.FillAsync("#UserName", "admin");
        await page.FillAsync("#Password", "password");
        await page.ClickAsync("text=Log in");
        var isExist = await page.Locator("text='Employee Details'").IsVisibleAsync();
        Assert.IsTrue(isExist);

        await Assertions.Expect(page.Locator("text='Employee Details'")).ToBeVisibleAsync(
            new LocatorAssertionsToBeVisibleOptions
            {
                Timeout = 100
            });
    }

    [Test]
    public async Task TestWithPom()
    {
        var pw = await Playwright.CreateAsync();
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("http://www.eaapp.somee.com");

        var loginPage = new LoginPage(page);
        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeePresent();
        Assert.IsTrue(isExist);
    }

    [Test]
    public async Task TestNetwork()
    {
        var pw = await Playwright.CreateAsync();
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("http://www.eaapp.somee.com");

        var loginPage = new LoginPage(page);
        await loginPage.ClickLogin();
        var waitRequest = page.WaitForRequestAsync("**/Employee");
        var waitResponse = page.WaitForResponseAsync("**/Employee");
        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeePresent();
        Assert.IsTrue(isExist);

        await loginPage.ClickEmployeeListLink();
        var request = await waitRequest;
        var response = await waitResponse;
    }
}