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

        private IWebElement EmployeeListLink => wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Employee List")));
        private IWebElement CreateNewButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Create New")));
        private IWebElement SearchField => wait.Until(ExpectedConditions.ElementIsVisible(By.Name("searchTerm")));
        private IWebElement DeleteLink => wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Delete")));

        public void GoToEmployeeList()
        {
            EmployeeListLink.Click();
        }

        public void GoToCreateEmployeeForm()
        {
            GoToEmployeeList();
            CreateNewButton.Click();
        }

        public void FillEmployeeForm(string name, string salary, string duration, string grade, string email)
        {
            var nameField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Name")));
            var salaryField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Salary")));
            var durationWorkedField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("DurationWorked")));
            var gradeDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Grade")));
            var emailField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            var submitButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn")));

            nameField.Clear();
            nameField.SendKeys(name);

            salaryField.Clear();
            salaryField.SendKeys(salary);

            durationWorkedField.Clear();
            durationWorkedField.SendKeys(duration);

            // Select grade option
            var option = gradeDropdown.FindElement(By.XPath($"//option[. = '{grade}']"));
            option.Click();

            emailField.Clear();
            emailField.SendKeys(email);

            // Scroll into view to avoid click interception
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            submitButton.Click();
        }

        public void SearchEmployee(string name)
        {
            SearchField.Clear();
            SearchField.SendKeys(name);
            SearchField.SendKeys(Keys.Enter);
        }

        public void DeleteEmployee()
        {
            DeleteLink.Click();
            var confirmButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".btn")));
            confirmButton.Click();
        }
    }
}
