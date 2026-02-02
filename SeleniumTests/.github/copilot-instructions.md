# SeleniumTests Copilot Instructions

## Project Overview

A modularized Selenium/C# UI automation test suite for multiple government building permit applications:

- **OnlineApplication**: Property owner application forms (login → project info → professional docs → submit)
- **WebPortal**: Permit review/approval interface
- **PTRAX**: Post-permit workflow and evaluation system
- **BPAS**: Building permit administrative system
- **Register**: User registration module

## Architecture & Key Patterns

### Page Object Model (POM)

Each application module has dedicated classes under `/Pages/{Module}/` representing user workflows:

- Classes encapsulate a logical flow (e.g., `Login`, `AppProjInfo`, `SubmitApp`)
- Constructor receives `IWebDriver` and `WebDriverWait` instances
- Methods perform multi-step interactions grouped by business purpose

**Example**: [OnlineApplication/Login.cs](Pages/OnlineApplication/Login.cs) handles login + OTP verification in one method.

### WebDriver Extensions

[Helpers/WebDriverExtensions.cs](Helpers/WebDriverExtensions.cs) provides reusable Selenium patterns:

- **Element interaction**: `selectElement()` (find by Name, clear, sendKeys), `selectDropdown()` (handles both Name and Id locators)
- **Wait helpers**: `UntilLoadingDisappears()`, `waitElementDisappear()` - poll for loading UI to vanish
- **Click resilience**: `ClickElement()` retries 3x on `ElementClickInterceptedException` (handles animation overlays)
- **Data generation**: `ProjInfoGens()`, `EvalGens()`, `addressGens()` - generate random realistic data per field

### Test Data Management

JSON-driven testing for professional data:

- [professionalData.json](Pages/OnlineApplication/professionalData.json) contains user profiles (PRC, PTR, licenses)
- [AddProf.cs](Pages/OnlineApplication/AddProf.cs) deserializes JSON with Newtonsoft.Json, iterates batch processing

## Critical Build & Test Commands

### Build & Run

```bash
cd SeleniumTests
dotnet restore
dotnet build                    # Compile
dotnet test                     # Run all tests
dotnet test --logger "console;verbosity=detailed"  # With output
```

### VS Code Debug

Press `F5` → select **Debug Selenium Tests** (configured in launch.json)

### Test Execution Notes

- **Default**: Chrome visible (comment out `--headless=new` for headless mode in [LocalWebAppTests.cs](Tests/LocalWebAppTests.cs#L34))
- **All tests [Ignore] by default** - must remove attribute to run
- **TestCategory attributes**: `[TestCategory("OnlineApplication")]` etc. - use for filtering

## Project Conventions

### Locator Strategy

Priority order (see WebDriverExtensions):

1. **By.Name** - preferred for input fields (most stable)
2. **By.Id** - specific controls (e.g., `cmbApplicationKind`)
3. **By.XPath** - complex DOM structures; scroll into view before clicking
4. **By.CssSelector** - button styling patterns

### Wait Strategy

- **Explicit waits** over implicit - `WebDriverWait` with `ExpectedConditions`
- **Default timeout**: 20-60 seconds (varies by Page class constructor)
- **Custom conditions**: Check for loading UI disappearance before next action

### Test URLs (Hardcoded - Update for Environment)

- Register: `http://192.168.20.71:1024`
- Online App Login: `http://192.168.20.71:1024/Account/Login`
- WebPortal: `http://192.168.20.71:1025/`
- Permit number format: `NBP2601-00090` (used for lookups)

## Cross-Component Data Flows

### Online Application Workflow

```
Login (username/OTP) → AppProjInfo (building details) → ProfDocInfo (upload docs) → SubmitApp → Confirmation
```

- Reused credentials: `gfandon` / `P@ssw0rd` (see [Login.cs](Pages/OnlineApplication/Login.cs))
- Project info fields auto-generated: LPIN, TDN, TCT (see `ProjInfoGens`)

### Multi-App End-to-End

`LocalWebAppTests` orchestrates cross-system journeys:

- [OnlineAppTesting()](Tests/LocalWebAppTests.cs#L74) creates permit → tracks ID
- [WebPortalTesting()](Tests/LocalWebAppTests.cs#L87) receives permit via ID lookup
- [PTRAXTesting()](Tests/LocalWebAppTests.cs#L96) transitions permit to evaluation

## Common Pitfalls & Debugging

| Issue                              | Solution                                                                            |
| ---------------------------------- | ----------------------------------------------------------------------------------- |
| `ElementClickInterceptedException` | `ClickElement()` already handles with retry; if persists, modal/overlay not closing |
| Test timeout on dropdown           | Use `refactoredSelect()` with option value wait instead of `selectDropdown()`       |
| Loading spinner not detected       | Check XPath `"//*[@id='loading']/img"` matches actual DOM; adjust in extensions     |
| Credential failures                | Verify hardcoded URLs and credentials; OTP extracted from `hidVerCode` attribute    |
| JSON deserialization errors        | Ensure `professionalData.json` path is relative to test execution directory         |

## Dependencies & NuGet Packages

- **Selenium.WebDriver** 4.23.0 - Core WebDriver
- **DotNetSeleniumExtras.PageObjects.Core** 4.14.1 - POM helpers
- **DotNetSeleniumExtras.WaitHelpers** 3.11.0 - ExpectedConditions
- **MSTest.TestFramework** 3.2.2 - Execution & assertions
- **Newtonsoft.Json** - JSON deserialization (version in .csproj)
- **Selenium.WebDriver.ChromeDriver** 129.0.\* - Auto-matches Chrome version

## Key Files Reference

- [LocalWebAppTests.cs](Tests/LocalWebAppTests.cs) - Test entry points & orchestration
- [WebDriverExtensions.cs](Helpers/WebDriverExtensions.cs) - Selenium utility library
- [Pages/](Pages/) - Module structure mirrors app topology
- [SeleniumTests.csproj](SeleniumTests.csproj) - .NET 8 target, all dependencies
