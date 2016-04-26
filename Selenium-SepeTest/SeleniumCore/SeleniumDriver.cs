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


    public class SeleniumDriver : ISeleniumDriver
    {


        public IWebDriver SeleniumDriverInitialize()
        {
            //return new ChromeDriver(@"C:\Users\sukesh_k02\Documents\Visual Studio 2013\Projects\Selenium-SepeTest\Selenium-SepeTest\Packages\Selenium Drivers");
            return new ChromeDriver(@"C:\Users\sukesh_k02\Documents\Visual Studio 2013\Projects\SeleniumBAT_Final\Selenium-SepeTest\Packages\Selenium Drivers");

        }

    }
}
