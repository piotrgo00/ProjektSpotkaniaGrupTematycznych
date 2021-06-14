# ProjektSpotkaniaGrupTematycznych
## Funkcjonalności
Stworzona aplikacja internetowa służy do ułatwienia tworzenia spotkań osób o podobnych zainteresowaniach.  

Niezalogowany użytkownik może zarejestrować się w serwisie podając swoje imię, nazwisko, nazwę użytkownika, e-mail oraz hasło. Po zatwierdzeniu formularza serwer wysyła na wcześniej podanego maila prośbę o potwierdzenie utworzenia konta. Po potwierdzeniu adresu e-mail użytkownik może się zalogować.  

Niezalogowany użytkownik ma możliwość zresetowania hasła do własnego profilu, procedura odzyskiwania hasła jest bezpieczna (do jej wykonania potrzebny jest dostęp do konta e-mail). 

 Zalogowany użytkownik może stworzyć grupę, która posiada nazwę, opis, miasto oraz po dołączeniu innych osób do niej posiada także listę uczestników, opcjonalnie grupa może także posiadać jedną z kategorii. Użytkownik, który stworzył grupę jest jej właścicielem, może on zatwierdzać oraz odrzucać prośby o dołączenie, może także dowolnie ją edytować.  

Zalogowany użytkownik może dowolnie dodawać, edytować oraz usuwać kategorie.  

Użytkownik może wyszukiwać grupy według kategorii, nazwy oraz miasta, grupy wyświetlane są według najnowszej daty utworzenia.  

Zalogowany użytkownik może wysłać prośby o dołączenie do grupy, może ją także opuścić w dowolnym momencie. 

Zalogowany użytkownik może przeglądać listę grup oraz spotkań, do których jest zapisany. 

Administrator grupy ma możliwość organizacji spotkań grupy. Spotkanie ma temat, opis, datę, godzinę, lokalizację oraz link do spotkania online i limit osób.  

Na profilu grupy dostępna jest historia spotkań, osoby, które są członkami tejże grupy oraz dostępna tylko dla admina lista próśb o dołączenie do grupy. 

Użytkownik może zapisać się lub wypisać się do/ze spotkania grupy, do której należy. Do spotkania nie może dołączyć więcej osób niż jest określone w informacjach o spotkaniu. 

Uczestnik spotkania może wygenerować i pobrać bilet wstępu na spotkanie w postaci pliku PDF. Bilet wstępu powinien zawierać kod QR, numer biletu, nazwę grupy, temat oraz miejsce spotkania, a także datę i godzinę spotkania. 

Aplikacja ma estetyczny wygląd. 

## Wykożystane biblioteki
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- SendGrid - wysyłanie maili do potwierdzenia rejestracji oraz odzyskiwania hasła
