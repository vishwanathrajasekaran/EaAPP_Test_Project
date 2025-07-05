using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EaAPP_Test_Project.Pages
{
    public class EmployeePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public EmployeePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement EmployeeListLink => driver.FindElement(By.LinkText("Employee List"));
        private IWebElement CreateNewButton => driver.FindElement(By.LinkText("Create New"));
        private IWebElement NameField => driver.FindElement(By.Id("Name"));
        private IWebElement SalaryField => driver.FindElement(By.Id("Salary"));
        private IWebElement DurationWorkedField => driver.FindElement(By.Id("DurationWorked"));
        private IWebElement GradeDropdown => driver.FindElement(By.Id("Grade"));
        private IWebElement EmailField => driver.FindElement(By.Id("Email"));
        private IWebElement SubmitButton => driver.FindElement(By.CssSelector(".btn"));
        private IWebElement SearchField => driver.FindElement(By.Name("searchTerm"));
        private IWebElement DeleteLink => driver.FindElement(By.LinkText("Delete"));

        public void GoToEmployeeList()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(EmployeeListLink)).Click();
        }

        public void GoToCreateEmployeeForm()
        {
            GoToEmployeeList();
            wait.Until(ExpectedConditions.ElementToBeClickable(CreateNewButton)).Click();
        }

        public void FillEmployeeForm(string name, string salary, string duration, string grade, string email)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Name"))).SendKeys(name);
            SalaryField.SendKeys(salary);
            DurationWorkedField.SendKeys(duration);
            GradeDropdown.FindElement(By.XPath($"//option[. = '{grade}']")).Click();
            EmailField.SendKeys(email);
            SubmitButton.Click();
        }

        public void SearchEmployee(string name)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("searchTerm")));
            SearchField.Clear();
            SearchField.SendKeys(name);
            SearchField.SendKeys(Keys.Enter);
        }

        public void DeleteEmployee()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(DeleteLink)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButton)).Click();
        }
    }
}
