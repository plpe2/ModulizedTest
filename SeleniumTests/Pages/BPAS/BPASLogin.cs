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
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationGeodetic");
            wait.UntilLoadingDisappears(driver);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
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
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationArchitectural");
            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            //Fire Zones and Fire Resistivity
            driver.EvalGens("FireAveCover");
            driver.EvalGens("FireOverAll");
            driver.EvalGens("SolidAveCover");
            driver.EvalGens("SolidOverAll");
            driver.EvalGens("PartitionsSolidConcrete");
            driver.EvalGens("PartitionsSolidMasonry");
            driver.EvalGens("PartitionsHollowUnit");
            driver.EvalGens("ProtectConcrete");
            driver.EvalGens("ProtectMasonry");
            driver.EvalGens("ProtectLathPlaster");
            driver.EvalGens("EWSolidConcrete");
            driver.EvalGens("EWSolidMasonry");
            driver.EvalGens("EWHollowUnit");
            driver.EvalGens("ColumnReinforcedConrete");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalFire']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            //Building Projections
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.EvalGens("BPFootings");
            driver.EvalGens("BPProjection");
            driver.EvalGens("BPEnroachment");
            driver.EvalGens("BPStreetWidth");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalBP']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            // Access Streets
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.EvalGens("ASDwellingUnits");
            driver.EvalGens("ASRoadway");
            driver.EvalGens("ASSidewalk");
            driver.EvalGens("ASPlantingStrip");
            driver.EvalGens("ASTotalRROW");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalAS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //Max Height of Building
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.selectDropdown("TypeOfBuildingStructureID", "Residential(R-1) Basic");
            driver.EvalGens("MHoBBHL");
            // driver.EvalGens("TowerHeight"); for not duplicating the table
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalMHoB']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[4]/div/div[1]/div/h3/a[1]/label");

            //Parking Space
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.EvalGens("PSParkingSlot");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalPS']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/a[1]/label");

            //Occupant Load
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse6']")).Displayed);
            driver.EvalGens("OLUnitArea");
            driver.EvalGens("OLNoOfOccupant");
            driver.EvalGens("OLNoOfExits");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalOL']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/a[1]/label");

            //Glazing and Opening
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.EvalGens("GaOOpeningMaterial");
            driver.EvalGens("GaODimension");
            driver.EvalGens("GaOSpacing");
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
            driver.EvalGens("LVCeilingHeight");
            driver.EvalGens("LVRoomWindow");
            driver.EvalGens("LVMinorWindow");
            driver.EvalGens("LVVentShaft");
            driver.EvalGens("LVAirDuct");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalLV']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[9]/div/div[1]/div/h3/a[1]/label");

            //Line and Grade
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[10]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse10']")).Displayed);
            driver.EvalGens("LGFrontage");
            driver.EvalGens("LGFrontageExcess");
            driver.EvalGens("LGLeft");
            driver.EvalGens("LGRight");
            driver.EvalGens("LGBack");
            driver.EvalGens("LGOtherSides");
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
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            driver.EvalGens("AttachmentPole");
            driver.EvalGens("AttachementGuyWire");
            driver.EvalGens("AttachmentTransformer");
            driver.EvalGens("AttachmentClearSpace");
            driver.ClickElement(wait, "//*[@id='btnAttachment']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.EvalGens("BldgProjVerticalClearance");
            driver.EvalGens("BldgProjHrzntlClearance");
            driver.EvalGens("BldgProjRoofSlope");
            driver.EvalGens("BldgProjConductorVolt");
            driver.ClickElement(wait, "//*[@id='btnBldgProj']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[2]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.EvalGens("CapacitorVoltage");
            driver.selectDropdown("CapacitorEnclosure", "Vault");
            driver.EvalGens("CapacitorFlammable");
            driver.ClickElement(wait, "//*[@id='btnCapacitor']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //3 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[3]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.EvalGens("ConductorsVoltage");
            driver.EvalGens("ConductorsClearance");
            driver.ClickElement(wait, "//*[@id='btnConductors']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //4 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[4]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse5']")).Displayed);
            driver.EvalGens("EmergencyCapacity");
            driver.EvalGens("EmergencyTransitionTime");
            driver.ClickElement(wait, "//*[@id='btnEmergency']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //5 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse6']")).Displayed);
            driver.EvalGens("MeteringMeteringSpace");
            driver.ClickElement(wait, "//*[@id='btnMetering']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //6 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.EvalGens("OpenSupplyVoltage");
            driver.EvalGens("OpenSupplyClearance");
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
            driver.EvalGens("ElectricalArea");
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
            driver.selectDropdown("EnclosureID", "Vault");
            driver.ClickElement(wait, "//*[@id='btnServEquip']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            //10 Save button
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[10]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[11]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse11']")).Displayed);
            driver.EvalGens("TransformerCapacity");
            driver.EvalGens("TransformerVoltage");
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
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationStructural");
            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
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
            driver.selectElement("SDRTypeofMaterialV", "metal");
            driver.EvalGens("SDRVeneerWeightV");
            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructuralSDR']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //EFRW
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.EvalGens("EFRWThickness");
            driver.EvalGens("EFRWDepth");
            driver.EvalGens("EFRWNoofColumn");
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
            driver.EvalGens("PSRCDERailingHeight");
            driver.EvalGens("PSRCDEFenceHeight");
            driver.EvalGens("PSRCDEFenceMaterial");
            driver.EvalGens("PSRCDEPassageWays");
            driver.EvalGens("PSRCDEToolsLocation");
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
            // driver.EvalGens("SDRVeneerWeightV");
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

            driver.ClickElement(wait, "//*[@id='btnSaveBldgEvalStructural']");
        }

        public void MEchanicalTest(string appNumber)
        {
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationMechanical");

            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='collapse1']/div/div[2]/div/div[1]/div[2]/label");
            driver.EvalGens("GuardFenceQty");
            driver.EvalGens("GuardArea");
            driver.ClickElement(wait, "//*[@id='btnSaveGuardings']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[1]/div/div[1]/div/h3/a[1]/label");

            //C
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[2]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse2']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse2']/div/div[2]/div/div[1]/div/label");
            driver.EvalGens("CranesHoistQty");
            driver.EvalGens("CranesCrnQty");
            driver.EvalGens("CranesWidth");
            driver.EvalGens("CranesHeight");
            driver.EvalGens("CranesGap");
            driver.ClickElement(wait, "//*[@id='btnSaveCranes']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[2]/div/div[1]/div/h3/a[1]/label");

            //H
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[3]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse3']")).Displayed);
            driver.ClickElement(wait, "//*[@id='collapse3']/div/div[2]/div/div[1]/div/label");
            driver.EvalGens("HoistHstQty");
            driver.EvalGens("HoistLoad");
            driver.EvalGens("HoistLength");
            driver.ClickElement(wait, "//*[@id='btnSaveHoist']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[3]/div/div[1]/div/h3/a[1]/label");

            //E
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[4]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse4']")).Displayed);
            driver.EvalGens("ElevatorQty");
            driver.EvalGens("ElevatorArea");
            driver.EvalGens("ElevatorPits");
            driver.EvalGens("ElevatorHoist");
            driver.EvalGens("ElevatorRopes");
            driver.EvalGens("ElevatorCounterWeight");
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
            driver.EvalGens("EscalatorFloor");
            driver.EvalGens("EscalatorWidth");
            driver.EvalGens("EscalatorSteps");
            driver.EvalGens("EscalatorSpeed");
            driver.EvalGens("EscalatorBalustrades");
            driver.EvalGens("EscalatorSize");
            driver.EvalGens("EscalatorQty");
            driver.ClickElement(wait, "//*[@id='btnSaveEscalators']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[5]/div/div[1]/div/h3/a[1]/label");

            // //BPV
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[6]/div/div[1]/div/h3/a[1]/label");

            // //RAC
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[7]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse7']")).Displayed);
            driver.EvalGens("RefTemp");
            driver.EvalGens("RefMovt");
            driver.EvalGens("RefAmmonial");
            driver.EvalGens("RefWater");
            driver.EvalGens("RefLoadContainer");
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
            driver.EvalGens("WaterOTQty");
            driver.EvalGens("WaterOTArea");
            driver.EvalGens("WaterOTHeight");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='fd1']/div/div[4]/div[3]/label");
            driver.EvalGens("WaterPTQty");
            driver.EvalGens("WaterPTArea");
            driver.EvalGens("WaterPTHeight");
            driver.ClickElement(wait, "//*[@id='fd2']/div/div[4]/div[1]/label");
            driver.ClickElement(wait, "//*[@id='fd2']/div/div[4]/div[2]/label");
            driver.ClickElement(wait, "//*[@id='btnSaveWaterPump']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div/div[2]/form/div[2]/div/div/div[8]/div/div[1]/div/h3/a[1]/label");

            //PFGS
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div[2]/div/div[2]/div/div[1]/div[2]/form/div/div/div/div[9]/div/div[1]/div/h3/label");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='collapse9']")).Displayed);
            driver.EvalGens("PipeQty");
            driver.selectDropdown("PipeSizeID", "Large");
            driver.EvalGens("PipeMeasure");
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
            driver.goToURL("http://192.168.20.71:1027/PermitEvaluation/PermitEvaluationSanitary");
            wait.UntilLoadingDisappears(driver);
            var appLocation = String.Concat("//*[contains(text(), '", appNumber, "')]");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(appLocation))).Click();
            wait.UntilLoadingDisappears(driver);

            driver.selectElement("DSDrainagePipeMaterialExcretaDS", "pvc");
            driver.EvalGens("DSDrainagePipeSizeExcretaDS");
            driver.selectElement("DSFittingMaterialExcretaDS", "pvc");
            driver.selectElement("DSDrainagePipeMaterialSanitaryDS", "pvc");
            driver.EvalGens("DSDrainagePipeSizeSanitaryDS");
            driver.selectElement("DSFittingMaterialSanitaryDS", "pvc");
            driver.selectElement("DSRoofDrianMaterialStormDS", "pvc");
            driver.EvalGens("DSDrainagePipeSizeStormDS");
            driver.selectElement("DSFittingMaterialStormDS", "pvc");
            driver.selectElement("DSDrainagePipeMaterialVentS", "pvc");
            driver.EvalGens("DSDrainagePipeSizeVentS");
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
            driver.selectDropdown("CategoryID", "AA");
            driver.ClickElement(wait, "//*[@id='btnSaveNoisePollution']");
            wait.Until(d => d.FindElement(By.XPath("//*[@id='modalbtnSaveB']")).Displayed);
            driver.ClickElement(wait, "//*[@id='closemdal']");
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[5]/div/div[1]/div/h3/a[1]/label");

            //PM
            driver.ClickElement(wait, "/html/body/div[116]/div/section/div/div/div/div[2]/div/div[2]/div/div/div[2]/div[2]/div/form/div/div[6]/div/div[1]/div/h3/a[1]/label");

            driver.ClickElement(wait, "//*[@id='btnSaveSanitaryIns']");
        }
    }
}