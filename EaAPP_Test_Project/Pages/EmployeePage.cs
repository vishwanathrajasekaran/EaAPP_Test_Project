using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public void GoToEmployeeList()
        {
            wait.Until(d => d.FindElement(By.LinkText("Employee List"))).Click();
        }

        public void GoToCreateEmployeeForm()
        {
            GoToEmployeeList();
            wait.Until(d => d.FindElement(By.LinkText("Create New"))).Click();
        }

        public void FillEmployeeForm(string name, string salary, string duration, string grade, string email)
        {
            wait.Until(d => d.FindElement(By.Id("Name"))).SendKeys(name);
            wait.Until(d => d.FindElement(By.Id("Salary"))).SendKeys(salary);
            wait.Until(d => d.FindElement(By.Id("DurationWorked"))).SendKeys(duration);

            var gradeDropdown = wait.Until(d => d.FindElement(By.Id("Grade")));
            gradeDropdown.FindElement(By.XPath($"//option[. = '{grade}']")).Click();

            wait.Until(d => d.FindElement(By.Id("Email"))).SendKeys(email);

            wait.Until(d => d.FindElement(By.CssSelector(".btn"))).Click();
        }

        public void SearchEmployee(string name)
        {
            var searchField = wait.Until(d => d.FindElement(By.Name("searchTerm")));
            searchField.Clear();
            searchField.SendKeys(name);
            searchField.SendKeys(Keys.Enter);
        }

        public void DeleteEmployee()
        {
            wait.Until(d => d.FindElement(By.LinkText("Delete"))).Click();
            wait.Until(d => d.FindElement(By.CssSelector(".btn"))).Click();
        }
    }
}
