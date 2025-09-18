using Microsoft.Playwright;

namespace PlaywrightUdemy.Pages
{
    internal class LoginPage
    {
        private IPage _page;
        private ILocator LinkLogin => _page.Locator("text=Login");
        private ILocator UserNameTextBox => _page.Locator("#UserName");
        private ILocator PasswordTextBox => _page.Locator("#Password");
        private ILocator LoginButton => _page.Locator("text=Log in");
        private ILocator EmployeeDetails => _page.Locator("text='Employee Details'");
        private ILocator EmployeeListLink => _page.Locator("text='Employee List'");

        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task ClickLogin()
        {
            await LinkLogin.ClickAsync();
            await _page.WaitForURLAsync("**/Login");
        }

        public async Task Login(string userName, string password)
        {
            await UserNameTextBox.FillAsync(userName);
            await PasswordTextBox.FillAsync(password);
            await LoginButton.ClickAsync();

        }

        public async Task<bool> IsEmployeePresent() => await EmployeeDetails.IsVisibleAsync();

        public async Task ClickEmployeeListLink() => await EmployeeListLink.ClickAsync();
    }
}
