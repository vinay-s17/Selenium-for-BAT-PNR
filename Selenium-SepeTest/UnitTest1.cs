using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using LNFranceTest.SeleniumCore;
using LNFranceTest.SeleniumCommon;
using OpenQA.Selenium.Firefox;
using System.Threading;


namespace LNFranceTest
{
    
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
            SeleniumDriver seleniumDriver = new SeleniumDriver();
            ISeleniumDriver seleniumDriverInterface = (ISeleniumDriver)seleniumDriver;
            chromeDriver = seleniumDriverInterface.SeleniumDriverInitialize();
            seleniumCommon = new SeleniumCommon.SeleniumCommon();

        }

        private TestContext testContextInstance;
        private IWebDriver chromeDriver;
        private SeleniumCommon.SeleniumCommon seleniumCommon;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        //Description :  Code to find Ajax Calls are completed
        public void AjaxCounter()
        {
            while (true)
            {
                var ajaxIsComplete = (bool)(chromeDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                //Thread.Sleep(TimeSpan.FromSeconds(10));
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        //Waits Till the page Load is completed

        public void PageLoadReady()
        {
            //WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
            WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(5));
            string state = string.Empty;
            // Test the autocomplete response - Explicit Wait 
            wait.Until(d =>
            {
                try
                {
                    state = ((IJavaScriptExecutor)chromeDriver).ExecuteScript(@"return document.readyState").ToString();
                }
                catch (InvalidOperationException)
                {

                }
                return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));

            });
        }
       
        public void LogOn()//Test Case 1 and 2
        {
            seleniumCommon.NavigateToUrl(chromeDriver, "http://pms.test.lexisnexis.fr/lpos/Account/Login");
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtName", "admin@exportcompta1", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtPassword", "12345678", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnLogin", SeleniumCommon.SeleniumCommon.FindElementsType.id);

        }
        public void LogOnInt()//Test Case 1 and 2
        {
            seleniumCommon.NavigateToUrl(chromeDriver, "http://10.87.248.91:3232/");
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtName", "adm@releasephase32", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtPassword", "12345678", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnLogin", SeleniumCommon.SeleniumCommon.FindElementsType.id);

        }         
        //For creation of a Moral Person
        public void AddPersonnel()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Fiches", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "FichesSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddPerson_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

        }

        public void AddCoordonnées()
        {
            //AddPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickUnselectableList(chromeDriver, ".k-dropdown");
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"Civilite_listbox\"]/li[26]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtDenomination", "TestRakesh", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void AddImmatriculation()
        {
            AddCoordonnées();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtSiren", "285441238", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtSiret", "2854412274340", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "VilleRCS", "SIRAC-32430", SeleniumCommon.SeleniumCommon.FindElementsType.id);

            /*seleniumCommon.SetTextBoxValue(chromeDriver, "CodeNafAutoComplete", "CU", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            IWebElement sourceTitle = chromeDriver.FindElement(By.Id("CodeNafAutoComplete"));
            sourceTitle.SendKeys("CU");
            Thread.Sleep(5000);
            IWebElement firstItem = chromeDriver.FindElement(By.Id("CodeNafAutoComplete_listbox"));
            firstItem.Click();

            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"c8f1dfb2-7dfb-456f-9313-d4e950d78d27\"]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);*/

            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void Mailing()
        {
            AddImmatriculation();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "GererLesCriteres", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            IWebElement elementToClick = chromeDriver.FindElement(By.XPath("//*[@id=\"CriteresDisponiblesList\"]/table/tbody/tr[1]/td[1]/input"));
            elementToClick.Click();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void Formules()
        {
            Mailing();
            PageLoadReady();
            AjaxCounter();
            /*seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddFormule_listbox\"]/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            AjaxCounter();
            seleniumCommon.SetTextBoxValue(chromeDriver, "ScriptFormula", "abc_test", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"FormulesList\"]/table/tbody/tr[2]/td[3]/a[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            ////*[@id="FormulesList"]/table/tbody/tr[2]/td[3]/a[1]/span -xpath for save button*/
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //*[@id="5bd9dba7-b486-4298-aba4-107c8195379e"]

        }

        public void Interlocuteurs()
        {
            Formules();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnAddInterlocuteurs", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/form/div/div/div[4]/div/table/tbody/tr[1]/td[5]/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

     [TestMethod]
        public void Dossiersconnectes()
        {
            Interlocuteurs();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }
        [TestMethod]
        public void DossierCreate()
        {
            LogOn();
            //LogOnInt();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "DossierList", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnCreate", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "2", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/div[2]/div/table/tbody/tr[1]/td", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            //Dossiersconnectes();
            seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddPerson_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickUnselectableList(chromeDriver, ".k-dropdown");
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"Civilite_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //Dossier Name
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtDenomination","Dossier_Sel"+ DateTime.Now.ToString(), SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //seleniumCommon.SetTextBoxValue(chromeDriver, "txtDenomination", "DossCreatedbySele_5Apr_1240PM", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/form/div[3]/table/tbody/tr[7]/td[2]/div[2]/div/table/tbody/tr/td[6]/a/span", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //seleniumCommon.SetTextBoxValue(chromeDriver, "ContributorName", "a", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //seleniumCommon.SetTextBoxValue(chromeDriver, "ContributorName", "admin admin", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "ContributorName", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ContributorName_listbox\"]/li[0]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //
            //seleniumCommon.ClickElements(chromeDriver, "/html/body/div[8]/div/div[2]/ul/li", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

            //seleniumCommon.SetTextBoxValue(chromeDriver, "Percentage", "51", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/form/div[3]/table/tbody/tr[7]/td[2]/div[2]/div/table/tbody/tr/td[6]/a[1]/span", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            
          }
        [TestMethod]
        public void FinancesTrack1()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Configuration", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Finances", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Codes", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            
            //seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[12]/ul/li[2]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

            //btnCreateNew
            seleniumCommon.ClickElements(chromeDriver, "btnCreateNew", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtboxCode", "000D", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //txtLibelle
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtLibelle", "SeleniumCodeDemo_6Apr_Thilesh", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            // Terminer 
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }
        /*[TestMethod]
        public void FinancesTrack1()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Configuration", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Finances", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Codes", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);

            //seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[12]/ul/li[2]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

            //btnCreateNew
            seleniumCommon.ClickElements(chromeDriver, "btnCreateNew", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtboxCode", "00SS", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //txtLibelle
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtLibelle", "LibelleBySeleniumCodeDemo_28Mar", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            // Terminer 
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }*/
        [TestMethod]
        public void GeneralConfiguration()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Configuration", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Paramétrage", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "Numérotation des dossiers", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            
            seleniumCommon.ClickElements(chromeDriver, "Finances", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Codes", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);

            //seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[12]/ul/li[2]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

            //btnCreateNew
            seleniumCommon.ClickElements(chromeDriver, "btnCreateNew", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtboxCode", "00SS", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //txtLibelle
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtLibelle", "LibelleBySeleniumCodeDemo_28Mar", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            // Terminer 
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }
        [TestMethod]
        public void GeneralMenu()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "Configuration", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            DroitAccess();
            Gestion();
            
            
            //
        }
        [TestMethod]
        public void Fiches()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Fiches", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            //PageLoadReady();
            //AjaxCounter();
            //FichesSubMenu
            seleniumCommon.ClickElements(chromeDriver, "FichesSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnApply", SeleniumCommon.SeleniumCommon.FindElementsType.id);//Click on Appliquer in filtres
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Fiches", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "FichesSubMenu2", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnCancel", SeleniumCommon.SeleniumCommon.FindElementsType.id);//Click on Annuler
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Fiches", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "FichesSubMenu3", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnApply", SeleniumCommon.SeleniumCommon.FindElementsType.id);//Click on Appliquer in filtres
            PageLoadReady();
            AjaxCounter();
          }
        [TestMethod]
        public void Recherche()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Recherche", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            //RechercheSubMenu
            seleniumCommon.ClickElements(chromeDriver, "RechercheSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Recherche", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "RechercheFullText", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "RechercheSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            //StatistiqueSubMenu
            seleniumCommon.ClickElements(chromeDriver, "StatistiqueSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
        }
        [TestMethod]
        public void DroitAccess()
        {
            //Click on Droits daccess
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "SECURITY_ACCESS_CONTROL_MENU", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            //
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[2]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            //
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[3]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[4]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[2]/nav/div/div/ul/li[5]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnApply", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
        }
        [TestMethod]
        public void Gestion()
        {
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "GeneralWorkflow", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();

        }
         [TestMethod]
        public void FinanceTrack2()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            //DossierEnCours
            seleniumCommon.ClickElements(chromeDriver, "DossierEnCours", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            //DossierFinancesSideLink
            seleniumCommon.ClickElements(chromeDriver, "DossierFinancesSideLink", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
           //Clicking on prestations
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/aside/section[1]/nav/div/div/ul/li[2]/div/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/form/div/div[1]/div[5]/table/tbody/tr/td[2]/table/tbody/tr/td[4]/div/span/span/span[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAdd_listbox\"]/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/form/div/div[4]/div[1]/fieldset/table/tbody/tr[3]/td[2]/span/span/span/span", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //CodeGuid_listbox
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"CodeGuid_listbox\"]/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //seleniumCommon.SetTextBoxValue(chromeDriver, "TarifHoraire", "2000", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //seleniumCommon.SetTextBoxValue(chromeDriver, "txtLibelle", "Prestation Created by Selenium", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "DureeText", "0100", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //Clicking on Terminer in Nouvelle prestation

            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish_FacturerHonoraires", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            //Clicking on Facturer button
            seleniumCommon.ClickElements(chromeDriver, "btnFacturer", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //btnValiderLaRepartition
            seleniumCommon.ClickElements(chromeDriver, "btnValiderLaRepartition", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
        }
        [TestMethod]
        public void Finances()
        {
            //LogOn();
            LogOnInt();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Général", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "DossierList", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnCreate", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "2", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/div[2]/div/table/tbody/tr[1]/td", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);//Selection of DP Procedure type
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddPerson_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickUnselectableList(chromeDriver, ".k-dropdown");
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"Civilite_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtDenomination", "Test_CreatedBySelenium", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);

            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/form/div[3]/table/tbody/tr[7]/td[2]/div[2]/div/table/tbody/tr/td[6]/a/span", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.SetTextBoxValue(chromeDriver, "ContributorName", "a", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //ContributorName_listbox
            //seleniumCommon.ClickElements(chromeDriver, "b96f3d84-a4ba-4a96-8e4d-fff70e4a3dca", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //seleniumCommon.ClickElements(chromeDriver, "/html/body/div[8]/div/div[2]/ul/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ContributorName_listbox\"]/li[26]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //PageLoadReady();
            //AjaxCounter();
            
            
            seleniumCommon.ClickElements(chromeDriver, "/html/body/div[8]/div/div[2]/ul/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //seleniumCommon.ClickElements(chromeDriver, "/html/body/div[8]/div/div[2]/ul/li", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);

            //seleniumCommon.SetTextBoxValue(chromeDriver, "Percentage", "50", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/div/div[1]/form/div[3]/table/tbody/tr[7]/td[2]/div[2]/div/table/tbody/tr/td[6]/a[1]/span", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);

        }
        //For creation of a physical person
        public void AddPhysicalPersonnel()
        {
            LogOn();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.HoverElements(chromeDriver, "Fiches", SeleniumCommon.SeleniumCommon.FindElementsType.linkText);
            seleniumCommon.ClickElements(chromeDriver, "FichesSubMenu", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddPerson_listbox\"]/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
        }

        public void AddCoordonnéesPhysicalPersonnel()
        {
            AddPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickUnselectableList(chromeDriver, ".k-dropdown");
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"Civilite_listbox\"]/li[2]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtName", "TestPhysicalPersonnel", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void DetailsPhysicalPersonnel()
        {
            AddCoordonnéesPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtAutresPrenoms", "AutresPrenoms_TestPPerson", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "txtNomdeNaissance", "TestPPerson_123", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"formContent\"]/div[4]/fieldset[1]/table/tbody/tr[2]/td[2]/span/span/span[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            //seleniumCommon.ClickElements(chromeDriver, "#Nationalite_listbox > li:nth-child(3)", SeleniumCommon.SeleniumCommon.FindElementsType.css);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void InformationComplementariesPhysicalPersonnel()
        {
            DetailsPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.SetTextBoxValue(chromeDriver, "Ursaaf", "Ursaaf_TestPPerson", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "UrsaafN", "UrsaafN_123", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "Retraite", "Retraite_TestPPerson", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.SetTextBoxValue(chromeDriver, "RetraiteN", "RetraiteN_123", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void MailingPhysicalPersonnel()
        {
            InformationComplementariesPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "GererLesCriteres", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            IWebElement elementToClick = chromeDriver.FindElement(By.XPath("//*[@id=\"CriteresDisponiblesList\"]/table/tbody/tr[2]/td[2]/input"));
            elementToClick.Click();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        public void FormulesPhysicalPersonnel()
        {
            MailingPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "addItem", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"ddlAddFormule_listbox\"]/li[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            AjaxCounter();
            ////*[@id="38973b85-4e71-4bbe-8d13-b71b3c1aefe6"]
            seleniumCommon.SetTextBoxValue(chromeDriver, "ScriptFormula", "abc_test", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "//*[@id=\"FormulesList\"]/table/tbody/tr[2]/td[3]/a[1]", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            ////*[@id="FormulesList"]/table/tbody/tr[2]/td[3]/a[1]/span -xpath for save button
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            //*[@id="5bd9dba7-b486-4298-aba4-107c8195379e"]
        }

        public void InterlocuteursPhysicalPersonnel()
        {
            FormulesPhysicalPersonnel();
            PageLoadReady();
            AjaxCounter();
            seleniumCommon.ClickElements(chromeDriver, "btnAddInterlocuteurs", SeleniumCommon.SeleniumCommon.FindElementsType.id);
            seleniumCommon.ClickElements(chromeDriver, "/html/body/main/form/div/div/div[4]/div/table/tbody/tr[1]/td[5]/a", SeleniumCommon.SeleniumCommon.FindElementsType.xPath);
            seleniumCommon.ClickElements(chromeDriver, "btnNext", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

        
        public void DossiersConnectesPhysicalPersonnel()
        {
            InterlocuteursPhysicalPersonnel();
            seleniumCommon.ClickElements(chromeDriver, "btnFinish", SeleniumCommon.SeleniumCommon.FindElementsType.id);
        }

    }
}
