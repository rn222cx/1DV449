# 1DV449 messy labbage

## Säkerhetsproblem

#### SQL injections

Inloggningsfunktionen i login/lib/login.js använder variablerna "username" och "password" direkt i sql-queryn utan någon form av validering och är därmed oskyddad mot SQL injections.

Exempelvis kan denna input `Bob' OR '1'='1` användas som lösenord för att komma in på sidan då koden returnerar sant, attackeraren behöver inte ens ange rätt användarnamn eftersom den alltid tar den första användaren i databastabellen. [1].

Problemet kan åtgärdas med parametriserad fråga och även någon form av whitelist-validering eller metoder liknande mysql_real_escape_string() för escape av specialtecknen i strängarna innan de skickas till databasen [1].

#### Cross-Site Scripting (XSS)

Formuläret har ingen validering mot vad som får skickas och kan därmed utsättas för XXS-attacker [2].

För att utföra en XSS-attack kan angriparen injicera JavaScript eller HTML-kod i forumläret. Scriptet skickar användaren automatiskt till en annan domän som innehåller script för att fånga upp information om användaren. Vanligvis brukar sådan information vara sessionskakor. Allt detta kan ske utan att användaren märker någonting [2]. 

För att åtgärda risken bör HTML-context strippas t.ex. (body, attribute, JavaScript, CSS, URL).
Den input som kommer in genom formuläret måste valideras i det som får skickas såsom längd och special tecken. 
Någon form av funktion som omvandlar skadlig kod till en sträng kan vara lämplig. Exempelvis som metoden htmlentities() i PHP [2].

#### Autentisering

Autentiseringen behandlas genom vilken roll användaren har samt om userID är undifined i message/index.js Problemet är att när användaren loggar ut får userID värdet 0 och därmed inte undifined.

Om någon har tillgång till användarens session kan denne direkt navigera till /message i url'en utan att logga in fast användaren har loggat ut [6].

En enkel lösning på problemet är att ange värdet `undifined` istället för `0` i req.session.userID = 0; som befinner sig i login/index.js

#### Session hijack

Sessionen har inte något som helst skydd från att någon annan kan använda den. Sessionen förstörs heller inte när användaren loggar ut och därmed kan angriparen fortsätta nyttja sessionen trots att användaren har loggat ut [3].

Genom en session hijack kan angriparen utföra samma förändringar som användaren har behörighet till att göra. Angripare övertar där med användarens roll.

Det allra säkraste allternativet är att använda sig av HTTPS om man använder sig av en HTTPS anslutning.
Andra förbättringar som kan göras är att slå på httpOnly i filen config/express.js som ger ett bättre skydd mot XSS-atacker [5].
Det är lämpligt att förstöra sessionen genom att implementera `req.session.destroy()` i logout funktionen i login/index.js.

#### Hashning/kryptering av lösenord

Lösenorden har inte någon form av hashning eller kryptering.

Problemet med obehandlade lösenord är att de syns som klartext i databasen.

Det finns metoder så som md5 och sha1 för att hascha lösenorden men tyvärr går dessa metoder att bruteforca och är sårbara för rainbow-attacker.
För att få ett bra skydd med saltning kan man använda sig av bcrypt [4]

### CSRF

Applikationen använder inte token i formulären vilket öppna för CSRF attacker.

Angriparen kan exempelvis skicka en länk till användaren som innehåller script med skadlig kod. Oftast brukar scriptet innehålla en img-tag vilken innehåller länken till den sidan  angriparen vill åt samt värden han vill skicka i något formulär. Användet av en img-tagg är för att den laddas in automatiskt vid sidladdning [7].

För att förhindra CSRF attacker kan ett gömt värde, kallad tokens, implementeras i formulären. Token värdet kan skapas som en session och ska generera ett nytt värde för varje sidhämtning [7].


## Prestandaproblem (front-end)

Remove messages i admin vyn fungerar inte då rad 18 i messageBoard.js är bortkommenterat, Vill man förhindra att flera ikoner dyker upp vid varje knapp tryckning tillfälle så kan man köra funktionen `MessageBoard.renderMessages();` i logout funktionen.
Funktionen samt id'et kallas även för logout vilket inte är ett fullt passande namn i detta sammanhang.

Att logout länken alltid syns är förrvirande och är inte estetisk tilltalande för applikationen.

MessageBoard.js och Message.js laddas in i header.html och kommer att laddas in de på sidor de inte används samt ger felmeddelanden.

Nedanstående filer anges i appModules/siteViews/partials/header.html men finns inte.
404 Not found in static/js/materialize.js
404 Not found in static/css/materialize.min.css

Filen ie10-viewport-bug-workaround.js anges i filen appModules/login/views/index.html men den finns inte och sänder ut ett 404.

CSS koden bör flyttas till en eller flera CSS filer.


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