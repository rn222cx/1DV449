Demo: http://45.55.147.164/maps/
Reserv Demo: http://easyfinding.se/

## Vad finns det för krav du måste anpassa dig efter i de olika API:erna?
Sveriges radio har inga krav på sitt API så länge det inte använts på ett sätt som skulle kunna skada Sveriges Radios oberoende eller trovärdighet.

Google maps kräver en nyckel för att kunna använda deras API. Om man överstiger 25.000 kart laddningar på ett dygn under 90 dagar måste man uppgradera sig till premium.

## Hur och hur länga cachar du ditt data för att slippa anropa API:erna i onödan?
Jag hämtar all data via SR's API med hjälp av PHP och cascha det i en json fil.
Om anropet ger status kod 200 samt om filen är äldre än 5 minuter kommer ny data hämtas.

## Vad finns det för risker kring säkerhet och stabilitet i din applikation?
Jag har ingen validering om vilken data som kommer från SR så skadlig kod kan drabba applikationen.
Kravet för att applikationen ska behålla sin stabilitet är att google maps api är stabilt.

## Hur har du tänkt kring säkerheten i din applikation?
Att inte använda några input fält som kan skicka in skadlig kod.

## Hur har du tänkt kring optimeringen i din applikation?
All css och javascript bibliotek är av minimerad version.
Applikationen är helt dynamisk och kräver inga sidomladdningar.
