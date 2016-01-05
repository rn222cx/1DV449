### Inledning

Projektet är en väderapplikation som kan söka upp väder prognoser i hela världen med hjälp av API:erna från GeoNames.com samt openWeatherMap.com.
Applikationen fungerar genom att användaren söker på en plats och får upp matchande platser i en lista. Vid val av plats kommer en sida visa en femdygns prognos över platsen.
Jag valde att göra en väderapplikation då det fanns möjlighet att bygga vidare på det rekomenderade projektet i 1DV409 kursen.

Det finns väldigt många liknande applikationer, kanske inte så konstigt med tanke på att flera utav dem har något form av API som man kan använda sig av och gör det enkelt att skapa sin egna väderapplikation. 

### Schematisk bild

![Schematisk bild](SchematiskBild.png)

### Säkerhet och prestandaoptimering

ASP.NET MVC 5 inbyggt skydd mot taggar, script, SQL och XSS-attacker. 
Entity Framework och LINQ skydd mot SQL-injections.
Kommenterat ut hjälp metoden AntiForgeryToken som är skydd mot CRSF attacker då token value cachas med appcache.
All databas kommunikation sker genom användaren Appuser som har särskilda rättigheter.

### Offline-first

### Risker med din applikation

### Egen reflektion kring projektet

### betygshöjande med din applikation


