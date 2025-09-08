using Microsoft.Playwright;

namespace PlaywrightUdemy;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    //[Test]
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

        await page.GotoAsync("http://www.eaapp.somee.com");
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
    }
}