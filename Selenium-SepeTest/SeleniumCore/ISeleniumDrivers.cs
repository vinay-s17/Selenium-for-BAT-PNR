using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace LNFranceTest.SeleniumCore
{


    public interface ISeleniumDriver
    {
        IWebDriver SeleniumDriverInitialize();


    }
}
