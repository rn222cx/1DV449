# 1DV449 messy labbage

## Säkerhetsproblem

#### SQL injections

inloggningsfunktionen i login/lib/login.js använder variablerna "username" och "password" direkt i sql-queryn utan någon form av validering och är därmed oskyddad mot SQL injections.

Exempelvis kan denna input `Bob' OR '1'='1` användas som lösenord för att komma in på sidan då koden returnerar sant, attackeraren behöver inte ens ange rätt användarnamn eftersom den alltid tar den första användaren i databastabellen. [1].

Problemet kan åtgärdas med parametriserad fråga och även någon form av whitelist-validering eller metoder liknande mysql_real_escape_string() för escape av specialtecknen i strängarna innan de skickas till databasen [1].

#### Cross-Site Scripting (XSS)

Formuläret har ingen validering mot vad som får skickas och kan därmed utsättas för XXS-attacker [2].

Genom en xss-attack kan angriparen injicera JavaScript i forumläret som då skickar användaren till en ful sida och samla information från användaren exempelvis sessions kakor utan att användaren vet om det då det sker en redirect till och från ful sidan [2]. 

För att åtgärda risken bör html context stripas exempelvis (body, attribute, JavaScript, CSS, URL).
Validering av vad som får skickas så som längd och special tecken. 
Implementation av funktion som fungerar som htmlentities() i PHP som omvandla skadlig kod till en sträng [2].

#### Autentisering

autentiseringen behandlas genom vilken roll användaren är samt om userID är undifined i message/index. Problemet är att när användare loggar ut får userID värdet 0 och där med inte undifined.

Om någon har tillgång till användarens session kan denna persson direkt navigera till /message i url'en utan att logga in fast användaren har loggat ut [6].

En enkel lösning på problemet är att ange värdet `undifined` istället för `0` i req.session.userID = 0; som befinner sig i login/index.js

#### Session hijack

Sessionen har inte något som helst skydd från att någon annan kan använda den. Sessionen förstörs heller inte när användaren loggar ut och därmed kan angriparen fortsätta nyttja sessionen även fast användaren har loggat ut [3].

Genom en session hijack kan angriparen göra samma sak som användaren har behörighet till att göra.

Det allra säkraste allternativet är att använda sig av HTTPS om man använder sig av en HTTPS anslutning.
Andra förbättringar som kan göras är att slå på httpOnly i filen config/express.js som ger ett bättre skydd mot xss atacker [5].
Det är lämpligt att förstöra sessionen genom att implementera `req.session.destroy()` i logout funktionen i login/index.js.

#### Hashning/kryptering av lösenord

Lösenorden har inte någon form av hashning eller kryptering.

Skulle någon komma över databasen kommer denna kunna se lösenorden i klartext.

Det finns metoder så som md5 och sha1 för att hascha lösenorden men tyvärr går dessa metoder att bruteforca och är sårbara för rainbow attacker.
För att få ett bra skydd med saltning kan man använda sig av bcrypt [4]

### CSRF

fortsätta skriva
Applikationen använder inte av token i sina formulär som kan leda till CSRF attacker.

Attackeraren kan exempelvis skicka en länk till användaren som innehåller script som är farligt för användaren. Oftast bruka scriptet innehålla en img tag som innehåller länken till sidan attackeraren vill åt samt värden han vill skicka i något formulär. Varför just en img tagg är för att den laddas in automatiskt vid sidladdning [7].

För att förhindra CSRF attacker kan ett gömt värde kallad för tokens implementeras i formulären. Token värdet kan skapas som en session och ska genereras ett nytt värde för varje sidhämtning [7].


## Prestandaproblem (front-end)

Remove messages i admin vyn funkar inte då rad 18 i messageBoard.js är bortkommenterat, Vill man förhindra att flera ikoner dyker upp vid varje tryck tillfälle så kan man köra functionen `MessageBoard.renderMessages();` i logout funktionen.
Funktionen samt id'et kallas även för logout som inte är fullt passande namn i detta ändamålet.

Logout länken kommer alltid att synas då den laddas in i default.html och är inte passande om man inte är inloggad.

MessageBoard.js och Message.js laddas in i header.html och kommer laddas in på sidor de inte används samt ger felmeddelanden.

nedanstående filer anges i appModules/siteViews/partials/header.html men finns inte.
404 cant be found in static/js/materialize.js
404 cant be found in static/css/materialize.min.css

filen ie10-viewport-bug-workaround.js anges i filen appModules/login/views/index.html men den finns inte och sänder ut ett 404.

css koden bör flyttas till en eller flera css filer.


## Egna övergripande reflektioner

Det finns tomma filer som inte används.



## Referenser

[1] OWASP foundation, "Top 10 2013-A1-Injection", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A1-Injection [Hämtad: 24 november, 2015].

[2] OWASP foundation, "Top 10 2013-A3-Cross-Site Scripting", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A3-Cross-Site_Scripting_(XSS) [Hämtad: 24 november, 2015].

[3] OWASP foundation, "Top 10 2013-A2-Broken Authentication and Session Management", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A2-Broken_Authentication_and_Session_Management [Hämtad: 25 november, 2015].

[4] Wikipedia, "bcrypt", 8 November 2015 [Online] Tillgänglig: https://en.wikipedia.org/wiki/Bcrypt [Hämtad: 25 november, 2015].

[5] OWASP foundation, "Mitigating the Most Common XSS attack using HttpOnly", 12 November 2014 [Online] Tillgänglig: https://www.owasp.org/index.php/HttpOnly [Hämtad: 25 november, 2015].

[6] OWASP foundation, "Top 10 2013-A7-Missing Function Level Access Control", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A7-Missing_Function_Level_Access_Control [Hämtad: 26 november, 2015].

[7] OWASP foundation, "Top 10 2013-A8-Cross-Site Request Forgery (CSRF)", 18 September 2013 [Online] https://www.owasp.org/index.php/Top_10_2013-A8-Cross-Site_Request_Forgery_(CSRF) [Hämtad: 26 november, 2015].