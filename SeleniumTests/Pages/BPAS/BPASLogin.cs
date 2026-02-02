using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;
using System.IO;

namespace SeleniumTests
{
    public class BPASLogin
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public BPASLogin(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void BPASLoginTest()
        {
            driver.goToURL("http://192.168.20.71:1027/");
            driver.FindElement(By.XPath("//*[@id='formRegister']/div/div[1]/input")).SendKeys("admin marla");
            driver.FindElement(By.XPath("//*[@id='password-field']")).SendKeys("adminP@ssw0rd");
            driver.FindElement(By.Id("btnLog")).Click();
            wait.UntilLoadingDisappears(driver);
            wait.Until(d => d.FindElement(By.XPath("//*[@id='ConfirmedLogin']")).Displayed);
            driver.FindElement(By.XPath("//*[@id='btnLogInOK']")).Click();
            wait.UntilLoadingDisappears(driver);
        }

        public void NBPEvalTest()
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationGeodetic");
            wait.UntilLoadingDisappears(driver);
            // wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), '')]"))).Click();
            // GeodeticTest();
            // ArchiTest();
        }

        public void GeodeticTest(string appNumber)
        {
            var appLocation = String.Concat("//*/td[contains(text(), '", appNumber, "')]");
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationGeodetic");
            wait.UntilLoadingDisappears(driver);
            //Search Function (Giving Stale element error)
            /* wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);
            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver); */
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[1]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[2]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='frmComplianceGeodetic']/div[3]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveGeodeticPlans']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='step-2']/div/div[2]/div/div/div[2]/div/div/div/div[1]/div/h3[2]/a/label");
            driver.ClickElement(wait, "//*[@id='btnSaveGeodeticEval']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
        }

        public void ArchiTest(string appNumber)
        {
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");

            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationArchitectural");
            wait.UntilLoadingDisappears(driver);
            //Search Function (Giving Stale element error)
            /* wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);
            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver); */
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);

            //Fire Zones and Fire Resistivity
            driver.RefactoredEvalGens(wait, "FireAveCover");
            driver.RefactoredEvalGens(wait, "FireOverAll");
            driver.RefactoredEvalGens(wait, "SolidAveCover");
            driver.RefactoredEvalGens(wait, "SolidOverAll");
            driver.RefactoredEvalGens(wait, "PartitionsSolidConcrete");
            driver.RefactoredEvalGens(wait, "PartitionsSolidMasonry");
            driver.RefactoredEvalGens(wait, "PartitionsHollowUnit");
            driver.RefactoredEvalGens(wait, "ProtectConcrete");
            driver.RefactoredEvalGens(wait, "ProtectMasonry");
            driver.RefactoredEvalGens(wait, "ProtectLathPlaster");
            driver.RefactoredEvalGens(wait, "EWSolidConcrete");
            driver.RefactoredEvalGens(wait, "EWSolidMasonry");
            driver.RefactoredEvalGens(wait, "EWHollowUnit");
            driver.RefactoredEvalGens(wait, "ColumnReinforcedConrete");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalFire']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            //Building Projections
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.RefactoredEvalGens(wait, "BPFootings");
            driver.RefactoredEvalGens(wait, "BPProjection");
            driver.RefactoredEvalGens(wait, "BPEnroachment");
            driver.RefactoredEvalGens(wait, "BPStreetWidth");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalBP']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            // Access Streets
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.RefactoredEvalGens(wait, "ASDwellingUnits");
            driver.RefactoredEvalGens(wait, "ASRoadway");
            driver.RefactoredEvalGens(wait, "ASSidewalk");
            driver.RefactoredEvalGens(wait, "ASPlantingStrip");
            driver.RefactoredEvalGens(wait, "ASTotalRROW");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalAS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //Max Height of Building
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.selectDropdown(wait, "TypeOfBuildingStructureID", "Residential(R-1) Basic");
            driver.RefactoredEvalGens(wait, "MHoBBHL");
            // driver.RefactoredEvalGens(wait, "TowerHeight"); for not duplicating the table
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalMHoB']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[4]/div/div[1]/div/h3/a[1]/label");

            //Parking Space
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.RefactoredEvalGens(wait, "PSParkingSlot");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/a[1]/label");

            //Occupant Load
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse6']")).Displayed);
            driver.RefactoredEvalGens(wait, "OLUnitArea");
            driver.RefactoredEvalGens(wait, "OLNoOfOccupant");
            driver.RefactoredEvalGens(wait, "OLNoOfExits");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalOL']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/a[1]/label");

            // //Glazing and Opening
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.RefactoredEvalGens(wait, "GaOOpeningMaterial");
            driver.RefactoredEvalGens(wait, "GaODimension");
            driver.RefactoredEvalGens(wait, "GaOSpacing");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalGaO']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[7]/div/div[1]/div/h3/a[1]/label");

            //Architectural Accessibility
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[8]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse8']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[4]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[5]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[6]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[7]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[3]/div/div[8]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalAAccessibility']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[8]/div/div[1]/div/h3/a[1]/label");

            //Light and Ventilation
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[9]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse9']")).Displayed);
            driver.RefactoredEvalGens(wait, "LVCeilingHeight");
            driver.RefactoredEvalGens(wait, "LVRoomWindow");
            driver.RefactoredEvalGens(wait, "LVMinorWindow");
            driver.RefactoredEvalGens(wait, "LVVentShaft");
            driver.RefactoredEvalGens(wait, "LVAirDuct");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalLV']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[9]/div/div[1]/div/h3/a[1]/label");

            //Line and Grade
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[10]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse10']")).Displayed);
            driver.RefactoredEvalGens(wait, "LGFrontage");
            driver.RefactoredEvalGens(wait, "LGFrontageExcess");
            driver.RefactoredEvalGens(wait, "LGLeft");
            driver.RefactoredEvalGens(wait, "LGRight");
            driver.RefactoredEvalGens(wait, "LGBack");
            driver.RefactoredEvalGens(wait, "LGOtherSides");
            driver.ClickElement(wait, "//*[@id='btnComputeLandG']");
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalLG']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[10]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSaveBldgEval']");
        }

        public void ElectricalTest(string appNumber)
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationElectrical");
            wait.UntilLoadingDisappears(driver);
            //Search Function (Giving Stale element error)
            /* wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);
            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver); */
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);

            driver.RefactoredEvalGens(wait, "AttachmentPole");
            driver.RefactoredEvalGens(wait, "AttachementGuyWire");
            driver.RefactoredEvalGens(wait, "AttachmentTransformer");
            driver.RefactoredEvalGens(wait, "AttachmentClearSpace");
            driver.ClickElement(wait, "//*[@id='btnAttachment']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.RefactoredEvalGens(wait, "BldgProjVerticalClearance");
            driver.RefactoredEvalGens(wait, "BldgProjHrzntlClearance");
            driver.RefactoredEvalGens(wait, "BldgProjRoofSlope");
            driver.RefactoredEvalGens(wait, "BldgProjConductorVolt");
            driver.ClickElement(wait, "//*[@id='btnBldgProj']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.RefactoredEvalGens(wait, "CapacitorVoltage");
            driver.selectDropdown(wait, "CapacitorEnclosure", "Vault");
            driver.RefactoredEvalGens(wait, "CapacitorFlammable");
            driver.ClickElement(wait, "//*[@id='btnCapacitor']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //3 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.RefactoredEvalGens(wait, "ConductorsVoltage");
            driver.RefactoredEvalGens(wait, "ConductorsClearance");
            driver.ClickElement(wait, "//*[@id='btnConductors']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //4 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.RefactoredEvalGens(wait, "EmergencyCapacity");
            driver.RefactoredEvalGens(wait, "EmergencyTransitionTime");
            driver.ClickElement(wait, "//*[@id='btnEmergency']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //5 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse6']")).Displayed);
            driver.RefactoredEvalGens(wait, "MeteringMeteringSpace");
            driver.ClickElement(wait, "//*[@id='btnMetering']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //6 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.RefactoredEvalGens(wait, "OpenSupplyVoltage");
            driver.RefactoredEvalGens(wait, "OpenSupplyClearance");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[1]/div/div/div[4]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='btnOpenSupply']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //7 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[7]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[8]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse8']")).Displayed);
            driver.ClickElement(wait, "//*[@id='fieldContainer']/div/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='fieldContainer']/div/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='fieldContainer']/div/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnOverhead']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //8 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[8]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[9]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse9']")).Displayed);
            driver.RefactoredEvalGens(wait, "ElectricalArea");
            driver.ClickElement(wait, "//*[@id='collapse9']/div/div[2]/div/div/div/div[2]/fieldset/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse9']/div/div[2]/div/div/div/div[2]/fieldset/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse9']/div/div[2]/div/div/div/div[2]/fieldset/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnElecRoom']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //9 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[9]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[10]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse10']")).Displayed);
            driver.selectDropdown(wait, "EnclosureID", "Vault");
            driver.ClickElement(wait, "//*[@id='btnServEquip']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //10 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[10]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[11]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse11']")).Displayed);
            driver.RefactoredEvalGens(wait, "TransformerCapacity");
            driver.RefactoredEvalGens(wait, "TransformerVoltage");
            driver.ClickElement(wait, "//*[@id='btnTransformer']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //11 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[11]/div/div[1]/div/h3/a[1]/label");

            //12 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[12]/div/div[1]/div/h3/a[1]/label");

            //13 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[13]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSaveElectricalIns']");
        }

        public void StrucuralTest(string appNumber)
        {
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");

            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationStructural");
            wait.UntilLoadingDisappears(driver);
            //Search Function (Giving Stale element error)
            /* wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);
            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver); */
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);

            //GDCR
            driver.ClickElement(wait, "//*[@id='collapse1']/div[1]/div[2]/div/div[1]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div[1]/div[2]/div/div[2]/div/div[2]/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div[1]/div[2]/div/div[2]/div/div[3]/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div[1]/div[2]/div/div[2]/div/div[4]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralGDCR']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[1]/div/div[1]/div/h3/a[1]/label");

            //SDR
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            wait.Until(d => d.FindElement(By.Name("SDRTypeofMaterialV")).Displayed);
            driver.selectElement("SDRTypeofMaterialV", "metal");
            driver.RefactoredEvalGens(wait, "SDRVeneerWeightV");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralSDR']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //EFRW
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.RefactoredEvalGens(wait, "EFRWThickness");
            driver.RefactoredEvalGens(wait, "EFRWDepth");
            driver.RefactoredEvalGens(wait, "EFRWNoofColumn");
            driver.ClickElement(wait, "//*[@id='collapse3']/div[1]/div[2]/div/div[2]/div[2]/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralEFRW']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //PC
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse4']/div[1]/div[2]/div[2]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div[1]/div[2]/div[2]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div[1]/div[2]/div[2]/div[3]/div/label");
            driver.selectElement("PCOthers", "Complied");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralPC']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[4]/div/div[1]/div/h3/a[1]/label");

            //PSRCDE
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.RefactoredEvalGens(wait, "PSRCDERailingHeight");
            driver.RefactoredEvalGens(wait, "PSRCDEFenceHeight");
            driver.RefactoredEvalGens(wait, "PSRCDEFenceMaterial");
            driver.RefactoredEvalGens(wait, "PSRCDEPassageWays");
            driver.RefactoredEvalGens(wait, "PSRCDEToolsLocation");
            driver.ClickElement(wait, "//*[@id='collapse5']/div[1]/div[2]/div/div[6]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse5']/div[1]/div[2]/div/div[6]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralPSRCDE']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[5]/div/div[1]/div/h3/a[1]/label");

            //A/DB
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[6]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse6']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[2]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[2]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[3]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[3]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[4]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse6']/div[1]/div[2]/div/div/div[4]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralADB']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[6]/div/div[1]/div/h3/a[1]/label");

            // //P
            // driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[7]/div/div[1]/div/h3/label");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            // driver.selectElement("SDRTypeofMaterialV", "metal");
            // driver.RefactoredEvalGens(wait, "SDRVeneerWeightV");
            // driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralSDR']");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            // driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[7]/div/div[1]/div/h3/a[1]/label");

            //S
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[8]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse8']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[1]/fieldset/div/div[1]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[1]/fieldset/div/div[2]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[1]/fieldset/div/div[1]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[1]/fieldset/div/div[2]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[2]/fieldset/div/div[1]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[2]/fieldset/div/div[1]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[2]/fieldset/div/div[2]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div[1]/div[2]/div/div[2]/fieldset/div/div[2]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[8]/div/div[1]/div/h3/a[1]/label");

            //SIR
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[9]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[4]/div/button[2]");
        }

        public void MEchanicalTest(string appNumber)
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationMechanical");

            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);
            wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);

            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[2]/label");
            driver.RefactoredEvalGens(wait, "GuardFenceQty");
            driver.RefactoredEvalGens(wait, "GuardArea");
            driver.ClickElement(wait, "//*[@id='btnSaveGuardings']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[1]/div/div[1]/div/h3/a[1]/label");

            //C
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse2']/div/div[2]/div/div[1]/div/label");
            driver.RefactoredEvalGens(wait, "CranesHoistQty");
            driver.RefactoredEvalGens(wait, "CranesCrnQty");
            driver.RefactoredEvalGens(wait, "CranesWidth");
            driver.RefactoredEvalGens(wait, "CranesHeight");
            driver.RefactoredEvalGens(wait, "CranesGap");
            driver.ClickElement(wait, "//*[@id='btnSaveCranes']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //H
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse3']/div/div[2]/div/div[1]/div/label");
            driver.RefactoredEvalGens(wait, "HoistHstQty");
            driver.RefactoredEvalGens(wait, "HoistLoad");
            driver.RefactoredEvalGens(wait, "HoistLength");
            driver.ClickElement(wait, "//*[@id='btnSaveHoist']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //E
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.RefactoredEvalGens(wait, "ElevatorQty");
            driver.RefactoredEvalGens(wait, "ElevatorArea");
            driver.RefactoredEvalGens(wait, "ElevatorPits");
            driver.RefactoredEvalGens(wait, "ElevatorHoist");
            driver.RefactoredEvalGens(wait, "ElevatorRopes");
            driver.RefactoredEvalGens(wait, "ElevatorCounterWeight");
            driver.ClickElement(wait, "//*[@id='collapse4']/div/div[2]/div/div[7]/div/div[1]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div/div[2]/div/div[7]/div/div[1]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div/div[2]/div/div[7]/div/div[2]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div/div[2]/div/div[7]/div/div[2]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveElevators']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[4]/div/div[1]/div/h3/a[1]/label");

            //Esc
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.RefactoredEvalGens(wait, "EscalatorFloor");
            driver.RefactoredEvalGens(wait, "EscalatorWidth");
            driver.RefactoredEvalGens(wait, "EscalatorSteps");
            driver.RefactoredEvalGens(wait, "EscalatorSpeed");
            driver.RefactoredEvalGens(wait, "EscalatorBalustrades");
            driver.RefactoredEvalGens(wait, "EscalatorSize");
            driver.RefactoredEvalGens(wait, "EscalatorQty");
            driver.ClickElement(wait, "//*[@id='btnSaveEscalators']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/a[1]/label");

            // //BPV
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/a[1]/label");

            // //RAC
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.RefactoredEvalGens(wait, "RefTemp");
            driver.RefactoredEvalGens(wait, "RefMovt");
            driver.RefactoredEvalGens(wait, "RefAmmonial");
            driver.RefactoredEvalGens(wait, "RefWater");
            driver.RefactoredEvalGens(wait, "RefLoadContainer");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[7]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[7]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[7]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[8]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[8]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='collapse7']/div/div[2]/div/div[8]/div[3]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveRef']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[7]/div/div[1]/div/h3/a[1]/label");

            //WBPS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[8]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse8']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[2]/div[1]/div/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[2]/div[1]/div/div[2]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[2]/div[2]/div/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse8']/div/div[2]/div[2]/div[2]/div/label");
            driver.RefactoredEvalGens(wait, "WaterOTQty");
            driver.RefactoredEvalGens(wait, "WaterOTArea");
            driver.RefactoredEvalGens(wait, "WaterOTHeight");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[3]/label");
            driver.RefactoredEvalGens(wait, "WaterPTQty");
            driver.RefactoredEvalGens(wait, "WaterPTArea");
            driver.RefactoredEvalGens(wait, "WaterPTHeight");
            driver.ClickElement(wait, "//*[@id='fd2']/div/div[4]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='fd2']/div/div[4]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveWaterPump']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[8]/div/div[1]/div/h3/a[1]/label");

            //PFGS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[9]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse9']")).Displayed);
            driver.RefactoredEvalGens(wait, "PipeQty");
            driver.selectDropdown(wait, "PipeSizeID", "Large");
            driver.RefactoredEvalGens(wait, "PipeMeasure");
            driver.ClickElement(wait, "//*[@id='btnSavePipings']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[9]/div/div[1]/div/h3/a[1]/label");

            //FP
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[10]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse10']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse10']/div/div[2]/div/div/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse10']/div/div[2]/div/div/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse10']/div/div[2]/div/div/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveFire']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[10]/div/div[1]/div/h3/a[1]/label");

            // //Others
            // driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[11]/div/div[1]/div/h3/label");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse11']")).Displayed);

            // driver.ClickElement(wait, "//*[@id='']");
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            // driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[11]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSave']");
        }

        public void SanitaryTest(string appNumber)
        {
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");

            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationSanitary");
            wait.UntilLoadingDisappears(driver);
            // wait.Until(d => d.FindElement(By.XPath("//*[@id='dataTables-BuildingPermitEval']/tbody/tr[1]/td[1]")).Displayed);

            // driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            // wait.UntilLoadingDisappears(driver);
            // driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            // wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, appLocation);
            wait.UntilLoadingDisappears(driver);

            driver.selectElement("DSDrainagePipeMaterialExcretaDS", "pvc");
            driver.RefactoredEvalGens(wait, "DSDrainagePipeSizeExcretaDS");
            driver.selectElement("DSFittingMaterialExcretaDS", "pvc");
            driver.selectElement("DSDrainagePipeMaterialSanitaryDS", "pvc");
            driver.RefactoredEvalGens(wait, "DSDrainagePipeSizeSanitaryDS");
            driver.selectElement("DSFittingMaterialSanitaryDS", "pvc");
            driver.selectElement("DSRoofDrianMaterialStormDS", "pvc");
            driver.RefactoredEvalGens(wait, "DSDrainagePipeSizeStormDS");
            driver.selectElement("DSFittingMaterialStormDS", "pvc");
            driver.selectElement("DSDrainagePipeMaterialVentS", "pvc");
            driver.RefactoredEvalGens(wait, "DSDrainagePipeSizeVentS");
            driver.selectElement("DSVentStackMaterialVentS", "pvc");
            driver.selectElement("DSFittingMaterialVentS", "pvc");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPlumbingDS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            //WDS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div/div/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div/div/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div/div/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div/div/div/div[4]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div/div/div/div[5]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveWasteWater']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //SDS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.selectElement("StormDrainageRDL", "Near");
            driver.ClickElement(wait, "//*[@id='collapse3']/div[2]/div/div/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveStormDrainage']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //PVC
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse4']/div[2]/div/div/div/div[1]/div[1]/div/div/label");
            driver.ClickElement(wait, "//*[@id='collapse4']/div[2]/div/div/div/div[1]/div[1]/div/div/label");
            driver.ClickElement(wait, "//*[@id='btnSavePestVermin']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/a[1]/label");

            //NPC
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.selectDropdown(wait, "CategoryID", "AA");
            driver.ClickElement(wait, "//*[@id='btnSaveNoisePollution']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/a[1]/label");

            //PM
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSaveSanitaryIns']");
        }

        public void PlumbingTest(string appNumber)
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationPlumbing");
            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*/table[@id='dataTables-BuildingPermitEval']/*/tr[@class='odd']/td[contains(text(), '", appNumber, "')]");
            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            driver.selectElement("WSWDSPressurePipesMaterial", "pvc");
            driver.selectElement("WSWDSFittingMaterial", "pvc");
            driver.selectElement("WSWDSValveMaterial", "pvc");
            driver.ClickElement(wait, "//*[@id='collapse1']/div[2]/div[2]/div/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPlumbingWSWDS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            //PF
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[1]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[1]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[1]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[1]/div[4]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[2]/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[2]/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse2']/div[2]/div[2]/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPlumbingPF']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //WSS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse3']/div[2]/div/div[1]/div/fieldset/div/div[1]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse3']/div[2]/div/div[1]/div/fieldset/div/div[2]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse3']/div[2]/div/div[1]/div/fieldset/div/div[3]/div/label");
            driver.ClickElement(wait, "//*[@id='collapse3']/div[2]/div/div[2]/div/div/label");
            driver.ClickElement(wait, "//*[@id='btnSaveWaterSupply']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPlumbing']");
        }

        public void ElectronicsTest(string appNumber)
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationElectronics");
            wait.UntilLoadingDisappears(driver);

            driver.FindElement(By.XPath("//*[@id='txtEvalKeyword']")).SendKeys(appNumber);
            wait.UntilLoadingDisappears(driver);
            driver.ClickElement(wait, "//*[@id='btnSearchEvalRecord']");
            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*/table[@id='dataTables-BuildingPermitEval']/*/tr[@class='odd']/td[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[1]/a/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[2]/div[1]/a/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[3]/div[1]/a/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[4]/div[1]/a/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[3]/a/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[2]/div[3]/a/label");
            driver.ClickElement(wait, "//*[@id='btnSaveCompleteness']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            //Installations
            // driver.RefactoredEvalGens(wait, "");
            driver.ClickElement(wait, "//*[@id='btnSaveAll']");
            driver.RefactoredEvalGens(wait, "Capacity");
            driver.ClickElement(wait, "//*[@id='btnAddInsElectronics']");
            driver.ClickElement(wait, "//*[@id='closemdal']");

        }
    }
}