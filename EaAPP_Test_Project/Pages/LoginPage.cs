using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EaAPP_Test_Project.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // ✅ Method: Navigate to home page
        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        }

        // ✅ Method: Click login link
        public void ClickLoginLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("loginLink"))).Click();
        }

        // ✅ Method: Perform login
        public void PerformLogin(string username, string password)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName"))).SendKeys(username);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password"))).SendKeys(password);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[type='submit']"))).Click();
        }

        // ✅ Optional combined method
        public void GoToLoginPage()
        {
            NavigateToHomePage();
            ClickLoginLink();
        }

        // ✅ Optional combined login method
        public void Login(string username, string password)
        {
            GoToLoginPage();
            PerformLogin(username, password);
        }
    }
}
