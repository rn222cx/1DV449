# 1DV449

## Finns det några etiska aspekter vid webbskrapning. Kan du hitta något rättsfall?

För att visa god etik gäller det att respektera ägaren och hans budskap genom att visa vart källan kommer ifrån och inte ta all cred själv. En annan viktig aspekt kan vara att inte överbelasta servern och följa de regler ägaren har angivit.

Enligt wikipedia vann Facebook 2009 ett av de första upphovsrätts brotten mot en känd webb skrapare.

2011 blev Aaron Swartz gripen för att ha stulit JSTOR artiklar efter att du anslutit en laptop till MIT(universitet) nätverket i en omärkt och olåst garderob. Straffet landade på $1 miljon i böter plus 35 år i fängelse.

## Finns det några riktlinjer för utvecklare att tänka på om man vill vara "en god skrapare" mot serverägarna?

Man borde fråga webbsidans ägare om lov innan.
Kolla igenom både robots.txt och readme om det finns.
Identifiera sig exempelvis genom att skicka med sin email i HTTP headern så ägaren kan ta kontakt.
Inte överbelasta ägarens server.
Inte skrapa copyright skyddat material.
Testa lokalt innan man använder sin skrapnings applikation så den är under kontroll.

## Begränsningar i din lösning- vad är generellt och vad är inte generellt i din kod?

Inga länkar är hårdkodade och skrapan bör fungera även vid namnbyte av länknamn så länge det är i samma position. Detta gäller dock inte json anropet då både "/check?day=" och "&movie=" är hårdkodade vid namngivelse.
Skrapan är skapad för tre personer samt tre dagar, ändras detta kommer skrapan troligen inte fungera. 
Skrapan har inget beroende på hur många filmer det finns eller vad de heter.

## Vad kan robots.txt spela för roll?

robots.txt ger instruktioner till sökmotorer gällande hur de kan spindla.
Exempel på instruktioner kan vara vilka filer som får skrapas eller inte skrapa, delay på skrapningen, sökväg till sitemap filen.
robots.txt är inget skydd mot robotar utan bara regler som kan följas om man har god etik och moral.