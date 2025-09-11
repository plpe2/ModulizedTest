# SeleniumTests (C# + MSTest + Selenium)

## 📦 Setup
1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
2. Install [Visual Studio Code](https://code.visualstudio.com/) with:
   - C# Dev Kit extension
   - .NET Test Explorer (optional)
3. Ensure you have Chrome installed (the NuGet ChromeDriver will match it).

## ▶️ Running Tests
```bash
cd SeleniumTests
dotnet restore
dotnet test
```

## 🐞 Debugging in VS Code
1. Open folder in VS Code
2. Go to Testing view (beaker icon) → Run/Debug
3. Or press `F5` → select **Debug Selenium Tests**

## ⚙️ Customization
- Update the URL in `LocalWebAppTests.cs` with your VM web app address
- Update expected title to match your app
