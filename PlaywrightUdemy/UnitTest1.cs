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
    [Parallelizable(ParallelScope.Self)]
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
    [Parallelizable(ParallelScope.Self)]
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
    [Parallelizable(ParallelScope.Self)]
    public async Task TestWithPomByRole()
    {
        var pw = await Playwright.CreateAsync();
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("http://www.eaapp.somee.com");

        var loginPage = new LoginPageByRole(page);
        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeePresent();
        Assert.IsTrue(isExist);
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
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

    [Test]
    [Parallelizable(ParallelScope.Self)]
    public async Task TestNetworkInterception()
    {
        var pw = await Playwright.CreateAsync();
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        // Блокировка картинок
        //await page.RouteAsync("**/*",
        //    async route =>
        //    {
        //        if (route.Request.ResourceType == "image")
        //        {
        //            await route.AbortAsync();
        //        }
        //        else
        //        {
        //            await route.ContinueAsync();
        //        }
        //    });

        page.Request += (_, request) => Console.WriteLine(request.Method + " --- " + request.Url);
        page.Response += (_, response) => Console.WriteLine((int)response.Status + " --- " + response.Url);

        await page.GotoAsync("https://www.flipkart.com/");
    }

    private void Page_Request(object? sender, IRequest e)
    {
        throw new NotImplementedException();
    }
}