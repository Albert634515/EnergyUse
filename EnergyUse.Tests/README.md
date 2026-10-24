# Calculation tests

Run the regression tests with:

```powershell
dotnet test EnergyUse.Tests/EnergyUse.Tests.csproj
```

The tests create isolated temporary SQLite databases; they do not use the application database.

Coverage includes missing/overlapping rates, inclusive calendar dates, category and tariff group selection, historical estimates, explicit zero rates, percentage credits, VAT, rate changes, monthly charges in February, and netting of returned electricity.

Quantities and unit prices retain decimal precision. Each completed settlement line is rounded to two decimals with MidpointRounding.AwayFromZero. VAT is calculated from that rounded net amount and rounded by the same rule. Reports sum these completed lines, including when details are merged.
