### Säkerhet

ASP.NET MVC 5 inbyggt skydd mot taggar, script, SQL och XSS-attacker. 
Entity Framework och LINQ skydd mot SQL-injections.
Kommenterat ut hjälp metoden AntiForgeryToken som är skydd mot CRSF attacker då token value cachas med appcache.
All databas kommunikation sker genom användaren Appuser som har särskilda rättigheter.
