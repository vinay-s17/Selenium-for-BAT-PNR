using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;


namespace LNFranceTest.SeleniumCommon
{
    class SeleniumCommon
    {

        public enum FindElementsType
        {
            id = 1,
            name = 2,
            linkText = 3,
            xPath = 4,
            css = 5
        }


        public void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement GetElementsUsingId(IWebDriver driver, string id)
        {
            return driver.FindElement(By.Id(id));
        }

        public IWebElement GetElementsUsingName(IWebDriver driver, string name)
        {
            return driver.FindElement(By.Name(name));
        }

        public IWebElement GetElementsUsingLinkText(IWebDriver driver, string linkText)
        {
            return driver.FindElement(By.LinkText(linkText));
        }

        public IWebElement GetElementsUsingXpath(IWebDriver driver, string xPath)
        {
            return driver.FindElement(By.XPath(xPath));
        }
        public IWebElement GetElementsUsingCss(IWebDriver driver, string css)
        {
            return driver.FindElement(By.CssSelector(css));
        }




        public void SetTextBoxValue(IWebDriver driver, string idValue, string value, FindElementsType elementType)
        {
            switch (elementType)
            {
                case FindElementsType.id:
                    GetElementsUsingId(driver, idValue).SendKeys(value);
                    break;
                case FindElementsType.name:
                    GetElementsUsingName(driver, idValue).SendKeys(value);
                    break;
                case FindElementsType.linkText:
                    GetElementsUsingLinkText(driver, idValue).SendKeys(value);
                    break;
                case FindElementsType.xPath:
                    GetElementsUsingXpath(driver, idValue).SendKeys(value);
                    break;
                case FindElementsType.css:
                    GetElementsUsingCss(driver, idValue).SendKeys(value);
                    break;
            }
        }

        public void ClickElements(IWebDriver driver, string idValue, FindElementsType elementType)
        {
            switch (elementType)
            {
                case FindElementsType.id:
                    GetElementsUsingId(driver, idValue).Click();
                    break;
                case FindElementsType.name:
                    GetElementsUsingName(driver, idValue).Click();
                    break;
                case FindElementsType.linkText:
                    GetElementsUsingLinkText(driver, idValue).Click();
                    break;
                case FindElementsType.xPath:
                    GetElementsUsingXpath(driver, idValue).Click();
                    break;
                case FindElementsType.css:
                    GetElementsUsingCss(driver, idValue).Click();
                    break;
            }
        }


        public void HoverElements(IWebDriver driver, string idValue, FindElementsType elementType)
        {

            switch (elementType)
            {
                case FindElementsType.id:
                    HoverAction(GetHoverElementsUsingId(driver, idValue), driver);
                    break;
                case FindElementsType.name:
                    HoverAction(GetHoverElementsUsingName(driver, idValue), driver);
                    break;
                case FindElementsType.linkText:
                    HoverAction(GetHoverElementsUsingLinkText(driver, idValue), driver);
                    break;
                case FindElementsType.xPath:
                    HoverAction(GetHoverElementsUsingXpath(driver, idValue), driver);
                    break;
                case FindElementsType.css:
                    HoverAction(GetHoverElementsUsingCss(driver, idValue), driver);
                    break;
            }

        }

        private void HoverAction(IWebElement element, IWebDriver driver)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }


        public IWebElement GetHoverElementsUsingId(IWebDriver driver, string id)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public IWebElement GetHoverElementsUsingName(IWebDriver driver, string name)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
        }

        public IWebElement GetHoverElementsUsingLinkText(IWebDriver driver, string linkText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
        }

        public IWebElement GetHoverElementsUsingXpath(IWebDriver driver, string xPath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }
        public IWebElement GetHoverElementsUsingCss(IWebDriver driver, string css)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
        }

        public void ClickUnselectableList(IWebDriver driver, string idValue)
        {
            IList<IWebElement> cancelDivs = driver.FindElements(By.CssSelector(idValue));
            cancelDivs[0].Click(); //zero-base index
        }


        public void SelectDropDownListByText(IWebDriver driver, string idValue, string textValue, FindElementsType elementType)
        {

            switch (elementType)
            {
                case FindElementsType.id:
                    var selectElement1 = new SelectElement(GetElementsUsingId(driver, idValue));
                    selectElement1.SelectByText(textValue);

                    break;
                case FindElementsType.name:
                    var selectElement2 = new SelectElement(GetElementsUsingName(driver, idValue));
                    selectElement2.SelectByText(textValue);
                    break;
                case FindElementsType.linkText:
                    var selectElement3 = new SelectElement(GetElementsUsingLinkText(driver, idValue));
                    selectElement3.SelectByText(textValue);

                    break;
                case FindElementsType.xPath:
                    var selectElement4 = new SelectElement(GetElementsUsingXpath(driver, idValue));
                    selectElement4.SelectByText(textValue);

                    break;
                case FindElementsType.css:
                    var selectElement5 = new SelectElement(GetElementsUsingCss(driver, idValue));
                    selectElement5.SelectByText(textValue);
                    break;
            }

        }
        public void SelectDropDownListByValue(IWebDriver driver, string idValue, string textValue, FindElementsType elementType)
        {

            switch (elementType)
            {
                case FindElementsType.id:
                    var selectElement1 = new SelectElement(GetElementsUsingId(driver, idValue));
                    selectElement1.SelectByValue(textValue);

                    break;
                case FindElementsType.name:
                    var selectElement2 = new SelectElement(GetElementsUsingName(driver, idValue));
                    selectElement2.SelectByValue(textValue);
                    break;
                case FindElementsType.linkText:
                    var selectElement3 = new SelectElement(GetElementsUsingLinkText(driver, idValue));
                    selectElement3.SelectByValue(textValue);

                    break;
                case FindElementsType.xPath:
                    var selectElement4 = new SelectElement(GetElementsUsingXpath(driver, idValue));
                    selectElement4.SelectByValue(textValue);

                    break;
                case FindElementsType.css:
                    var selectElement5 = new SelectElement(GetElementsUsingCss(driver, idValue));
                    selectElement5.SelectByValue(textValue);
                    break;
            }

        }




    }
}
