using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public void GoToLoginPage()
        {
            wait.Until(d => d.FindElement(By.Id("loginLink"))).Click();
        }

        public void Login(string username, string password)
        {
            wait.Until(d => d.FindElement(By.Id("UserName"))).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys(username);

            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);

            driver.FindElement(By.Id("RememberMe")).Click();

            driver.FindElement(By.Id("loginIn")).Click();
        }

        public void LoginLinkClick()
        {
            driver.FindElement(By.Id("loginLink")).Click();
        }


        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        }


        public void PerformLogin(string username, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("UserName")).Displayed);

            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("RememberMe")).Click();
            driver.FindElement(By.Id("loginIn")).Click();
        }

    }
}
