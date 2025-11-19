using Microsoft.Playwright;

namespace PlaywrightUdemy.Pages
{
    internal class LoginPageByRole
    {
        private IPage _page;
        private ILocator LinkLogin => _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" });
        private ILocator UserNameTextBox => _page.GetByLabel("UserName");
        private ILocator PasswordTextBox => _page.GetByLabel("Password");
        private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" });
        private ILocator EmployeeDetails => _page.Locator("text='Employee Details'");
        private ILocator EmployeeListLink => _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" });

        public LoginPageByRole(IPage page)
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
