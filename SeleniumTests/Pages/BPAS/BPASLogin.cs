using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumTests.Helpers;
using System;

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

        public void GeodeticTest()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'NBP2509-00019')]"))).Click();
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
            // driver.ClickElement(wait, "//*[@id='card_one']/div[1]/div/h3/a[1]/label");

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
    }
}