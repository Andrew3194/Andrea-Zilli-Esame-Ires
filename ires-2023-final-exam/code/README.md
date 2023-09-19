L'esame consta di 5 punti da realizzare su un'applicazione già esistente.
L'applicazione è una web API che implementa la possibilità di disporre dei container su una nave.
Lo spazio per la posa dei container sulla nave si considera una griglia di NxM spot disponibili, ognuno identificato dalle sue coordinate X e Y (0-based).
Ogni container è largo 1 spot, e può essere lungo da 1 a 3 spot.
Ogni container ha un tipo diverso, e ci sono dei tipi incompatibili per i quali i due container non possono stare vicini.

Realizzare i seguenti 5 punti:

1. Impostazione progetto
Collegarsi al repository di GitHub tramite link fornito dal docente.
Fare un clone del repository in locale.
Creare un proprio repository, configurando i giusti "ignore", e aggiungere il docente come collaboratore.
Caricare il progetto finito sul proprio repository entro la scadenza.
Consegnare codice pulito e ordinato, senza commenti non indispensabili e senza stringhe di connessione hardcoded
(Le stringhe di connessione possono essere salvate nel launchSettings.json, o nell'appsetting.json, purché siano ignorati da Git).

2. Script SQL
Creare gli script di creazione dello schema db per SQL Server, seguendo quanto si trova nelle due entity in C#.
Configure i nomi di tabelle e colonne in modo che la mappatura tra i DbSet e proprietà in C# e i nomi a database sia automatica.
Creare uno script di riempimento di dati che:
- inserisca l'elenco di spot, in modo che siano riempite tutte le celle di una griglia NxM (quindi se si immagina una griglia 4x5, devono esserci 20 spot!)
- inserisca alcuni container
- aggiorni gli spot occupati da quei container settando correttamente la FK.

3. Dependency Injection
Correggere la dependency injection dell'applicazione, ovvero:
    a. configurare nei Services il lifecycle del DbContext, settandolo per usare SqlServer
    b. iniettare correttamente il DbContext nel controller.

4. Aggiunta di un nuovo container
Nel metodo di aggiunta di un container non c'è alcuna validazione. Aggiungere le dovute validazioni (no container più lunghi di 3 spot, no container di un tipo null o sconosciuto). In caso di errore, va restituito un oggetto
Inoltre, non è stato implementato il controllo sui tipi incompatibili. Modificare l'algoritmo in modo che, se il container è di tipo "cibo" non venga salvato vicino a un container di tipo "velenoso" (e viceversa), e un container di tipo "elettronica" non stia vicino a un container di tipo "infiammabile" (e viceversa).
Due container sono "vicini" se occupano spot contigui (lungo X o lungo Y). Se si toccano solo in diagonale, non si considerano vicini.

5. GET degli spot liberi
Realizzare un endpoint in get con url "/get-empty-spots", che restituisca una lista di AvailableSpotDto riportanti posizione X e Y di tutti gli spot liberi (cioè non occupati da un container), ordinati per X e poi per Y.
Usare una query di LINQ in forma asincrona per pescare i dati dal database.
