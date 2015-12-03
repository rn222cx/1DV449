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
Den input som kommer in genom formuläret måste valideras i det som får skickas såsom längd och specialtecken. 
Någon form av funktion som omvandlar skadlig kod till en sträng kan vara lämplig. Exempelvis som metoden htmlentities() i PHP [2].

#### Autentisering

Autentiseringen behandlas genom vilken roll användaren har samt om userID är undifined i message/index.js Problemet är att när användaren loggar ut får userID värdet 0 och därmed inte undifined.

Om någon har tillgång till användarens session kan denne direkt navigera till /message i url'en utan att logga in fast användaren har loggat ut [6].

En enkel lösning på problemet är att ange värdet `undifined` istället för `0` i req.session.userID = 0; som befinner sig i login/index.js

#### Session hijack

Sessionen har inte något som helst skydd för att någon annan kan använda den. Sessionen förstörs heller inte när användaren loggar ut och därmed kan angriparen fortsätta nyttja sessionen trots att användaren har loggat ut [3].

Genom en session hijack kan angriparen utföra samma förändringar som användaren har behörighet till att göra. Angripare övertar därmed användarens roll.

Det allra säkraste alternativet är att använda sig av HTTPS om man använder sig av en HTTPS anslutning.
Andra förbättringar som kan göras är att aktivera httpOnly i filen config/express.js som ger ett bättre skydd mot XSS-atacker, httpOnly hjälper till att minska risken för script tillträde på klientsidan i kakan [5].
Att skapa nytt sessions id för varje sidhämtning försvårar för angriparen då sessions id'et blir oanvändbart vid nästa sidhämtning användaren gör.
Det är lämpligt att förstöra sessionen genom att implementera `req.session.destroy()` i logout funktionen i login/index.js.

#### Hashning/kryptering av lösenord

Lösenorden har inte någon form av hashning eller kryptering.

Problemet med obehandlade lösenord är att de syns som klartext i databasen.

Det finns metoder så som md5 och sha1 för att hascha lösenorden men tyvärr går dessa metoder att bruteforca och är sårbara för rainbow-attacker.
För att få ett bra skydd med saltning kan man använda sig av bcrypt [4]

#### CSRF

Applikationen använder inte token i formulären vilket öppna för CSRF attacker.

Angriparen kan exempelvis skicka en länk till användaren som innehåller script med skadlig kod. Oftast brukar scriptet innehålla en img-tag vilken innehåller länken till den sidan angriparen vill åt samt värden han vill skicka i något formulär. Användet av en img-tagg är för att den laddas in automatiskt vid sidhämtning [7].

För att förhindra CSRF attacker kan ett gömt värde, kallad tokens, implementeras i formulären. Token värdet kan skapas som en session och ska generera ett nytt värde för varje sidhämtning [7].

#### Databas

Databasen ligger åtkomlig i static/message.db

Genom att ange sökvägen till databasen i URLen laddas databasen ner. Databasen är helt okrypterad så angriparen kan öppna och få fram vilken information som helst i databasen genom terminalen.

Genom att lägga databasen bakom root katalogen kommer inte någon användare åt databasen genom URLen. 

#### Insecure direct object references

Information om meddelanden ligger åtkomlig i JSON format genom att navigera till /message/data.
En POST funktion finns att tillgå för att radera meddelanden genom sökvägen /message/delete [13].

Genom att ange sökvägen till /message/data kan angriparen samla information om meddelanden.
Det går även radera meddelanden genom en POST förfrågan till /message/delete och skicka med messageID som data med meddelandets id som värde. 

För att förhindra obehöriga att få tillgång till ovanstående problem kan man lösa det genom Autentisering [13].

## Prestandaproblem (front-end)

MessageBoard.js och Message.js laddas in i header.html och kommer att laddas in på sidor de inte används och sänder ut felmeddelanden samt skapa onödiga HTTP requests [11].

Nedanstående filer anges i appModules/siteViews/partials/header.html men finns inte.
- 404 Not found in static/js/materialize.js
- 404 Not found in static/css/materialize.min.css

Filen ie10-viewport-bug-workaround.js anges i filen appModules/login/views/index.html men den finns inte och sänder ut ett 404 felmeddelande.

Google fonten som laddas in i headern används inte och skapa onödiga HTTP requests [11].

bilden b.jpg laddas in i sidan /message och används inte, detta skapa onödiga HTTP requests [11].

Mycket av CSS koden befinner sig nedanför HTML koden, detta ger sämre upplevelse för användaren då det visas en vit sida längre än om CSS koden hade varit i toppen av sidan [9].
All CSS kod bör flyttas till en CSS fil för bättre struktur och prestanda [8].

Samma som med CSS bör JavaScript koder som är utspridda i HTML filerna flyttas till egen fil för att få bättre prestanda och struktur [8].
JavaScript filer Läses in snabbare om de blir minifierade, det finns flera filer som bör minifieras exempelvis i katalogen static/javascript så som jquery.js och bootstrap.js. Visserligen används inte bootstrap.js i applicationen men om den skulle för framtida bruk [10].

Något som inte används och kan snabba upp applikationen är att cascha med Gzip. Det som främst behöver caschas är script och CSS [12].
Det verkar inte enkelt att använda sig av Gzip uttan något bibliotek/ramverk för node.js, exempel på sådant kan vara Zlib https://nodejs.org/api/zlib.html

Remove messages i admin vyn fungerar inte då rad 18 i messageBoard.js är bortkommenterat. Vill man förhindra att flera ikoner dyker upp vid varje knapp trycknings tillfälle kan man köra funktionen `MessageBoard.renderMessages();` i logout funktionen.
Funktionen samt id'et kallas även för logout vilket inte är ett fullt passande namn i detta sammanhang.

Att logout länken alltid syns är förrvirande och är inte estetisk tilltalande för applikationen.

Det finns tomma filer samt filer som inte används i applikationen.

## övergripande reflektioner

Laboratioen har gett mig ett större säkerhetstänk tack vare OWASP Top 10 dokument som listar de 10 största säkerhetshålen som kan förekomma i en webbapplikation, samt en applikation som innehåller säkerhetshål som OWASP beskriver och jag kan därmed använda mig av det i praktiken.

Mycket av säkerhetshålen har vi pratat om innan men inte avnvänt oss av i praktiken. Att nu direkt använda detta ger en större inblick i det hela.
Jag har fått uppfattningen att det går inte skydda sig mot allt men det krävs heller inte mycket skydd för att försvåra det avsevärt för den som vill försöka sig på att hacka sidan.

När det kommer till front-end optimering var vissa saker väldigt självklara, exempelvis hur man ska hantera JavaScript och CSS. Men att repetera gör att det ger djupare kunskaper som sitter kvar längre.
Men vad det gäller cashning och HTTP var det mesta nytt och lärorikt, det var kanske för mig lite väl mycket om HTTP.

Applikationen var skriven i node.js som var helt nytt för mig, ändå var det inga större problem att komma igång och kunna förstå applikationen. Men samtidigt har jag knappt lärt mig något om självaste node.js i sig, en förståelse jag har fått nu gör ändå att det blir lättare att söka och ta till mig den kunskap jag behöver i detta senare.


## Referenser

[1] OWASP foundation, "Top 10 2013-A1-Injection", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A1-Injection [Hämtad: 24 november, 2015].

[2] OWASP foundation, "Top 10 2013-A3-Cross-Site Scripting", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A3-Cross-Site_Scripting_(XSS) [Hämtad: 24 november, 2015].

[3] OWASP foundation, "Top 10 2013-A2-Broken Authentication and Session Management", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A2-Broken_Authentication_and_Session_Management [Hämtad: 25 november, 2015].

[4] Wikipedia, "bcrypt", 8 November 2015 [Online] Tillgänglig: https://en.wikipedia.org/wiki/Bcrypt [Hämtad: 25 november, 2015].

[5] OWASP foundation, "Mitigating the Most Common XSS attack using HttpOnly", 12 November 2014 [Online] Tillgänglig: https://www.owasp.org/index.php/HttpOnly [Hämtad: 25 november, 2015].

[6] OWASP foundation, "Top 10 2013-A7-Missing Function Level Access Control", 23 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A7-Missing_Function_Level_Access_Control [Hämtad: 26 november, 2015].

[7] OWASP foundation, "Top 10 2013-A8-Cross-Site Request Forgery (CSRF)", 18 September 2013 [Online] https://www.owasp.org/index.php/Top_10_2013-A8-Cross-Site_Request_Forgery_(CSRF) [Hämtad: 26 november, 2015].

[8] Steve Souders, High Performance Web Sites: Combined Scripts and Stylesheets, O'Reilly, 2007. [Online-PDF] http://www.it.iitb.ac.in/frg/wiki/images/4/44/Oreilly.Seve.Suoders.High.Performance.Web.Sites.Sep.2007.pdf [Hämtad: 30 november, 2015].

[9] Steve Souders, High Performance Web Sites: Rule 5: Put Stylesheets at the Top5, O'Reilly, 2007. [Online-PDF] http://www.it.iitb.ac.in/frg/wiki/images/4/44/Oreilly.Seve.Suoders.High.Performance.Web.Sites.Sep.2007.pdf [Hämtad: 30 november, 2015].

[10] Steve Souders, High Performance Web Sites: Rule 10: Minify JavaScript, O'Reilly, 2007. [Online-PDF] http://www.it.iitb.ac.in/frg/wiki/images/4/44/Oreilly.Seve.Suoders.High.Performance.Web.Sites.Sep.2007.pdf [Hämtad: 30 november, 2015].

[11] Steve Souders, High Performance Web Sites: Rule 1: Make Fewer HTTP Requests, O'Reilly, 2007. [Online-PDF] http://www.it.iitb.ac.in/frg/wiki/images/4/44/Oreilly.Seve.Suoders.High.Performance.Web.Sites.Sep.2007.pdf [Hämtad: 30 november, 2015].

[12] Steve Souders, High Performance Web Sites: Rule 4: Gzip Components, O'Reilly, 2007. [Online-PDF] http://www.it.iitb.ac.in/frg/wiki/images/4/44/Oreilly.Seve.Suoders.High.Performance.Web.Sites.Sep.2007.pdf [Hämtad: 30 november, 2015].

[13] OWASP foundation, "Top 10 2013-A4-Insecure Direct Object References", 14 Juni 2013 [Online] Tillgänglig: https://www.owasp.org/index.php/Top_10_2013-A4-Insecure_Direct_Object_References [Hämtad: 3 december, 2015].
