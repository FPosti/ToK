# GarageArrays

En konsolapplikation i C# som simulerar ett garage. Projektet är en övning i arrayer, arv och inkapsling.

## Funktioner

- Välj garagets storlek och fyll det med exempelfordon vid start.
- Parkera och hämta ut fordon.
- Lista parkerade fordon och visa antal per fordonstyp.
- Sök på registreringsnummer, färg, antal hjul och fordonstyp. Flera sökvillkor kan kombineras.
- Förhindra dubbla registreringsnummer och parkering när garaget är fullt.

Fordonstyperna är bil, motorcykel, flygplan, buss och båt.

## Uppbyggnad

Garage hanterar fordonen i en privat array av typen Vehicle?[], där varje position motsvarar en parkeringsplats. Vehicle är en abstrakt basklass som de fem fordonstyperna ärver från. Gemensamma egenskaper finns i basklassen, medan varje fordonstyp har en egen egenskap. Konsolklasserna hanterar menyer och inmatning.

En parkeringsplats innehåller ett fordon om den är upptagen och null om den är ledig. Frågetecknet i Vehicle?[] visar att varje plats kan innehålla antingen ett fordon eller null. Från början är alla platser tomma. När ett fordon parkeras placeras det på den första lediga platsen. När fordonet hämtas ut sätts platsen till null igen så att den kan användas av ett annat fordon.

