### System umożliwia kompleksowe zarządzanie zasobami uczelni, w tym:
* Rejestrację różnego rodzaju sprzętu (laptopy, kamery, projektory).
* Rejestrację użytkowników o różnych uprawnieniach i limitach (studenci, pracownicy).
* Obsługę procesu wypożyczania sprzętu z weryfikacją dostępności i limitów użytkownika.
* Obsługę zwrotów, w tym automatyczne naliczanie kar finansowych za przetrzymanie sprzętu po terminie.
* Generowanie raportów podsumowujących stan systemu (aktywne wypożyczenia, opóźnienia, zebrane kary).


### Projekt został podzielony na wyraźne warstwy logiczne (foldery), aby oddzielić model domeny od logiki biznesowej i mechanizmów przechowywania danych. 

### Dlaczego taki podział plików i klas?
Zdecydowałem się na podział na `Core`, `Repositories`, `Services` oraz `Utils`. Taki układ zapobiega powstawaniu tzw. "God Classes" (jak np. jedna wielka klasa `App` robiąca wszystko). Dzięki temu kod jest łatwiejszy w utrzymaniu, testowaniu i ewentualnej rozbudowie w przyszłości. 
* **Core (Model Domeny):** Czyste obiekty biznesowe (np. `User`, `Equipment`, `Rental`). Nie mają pojęcia o tym, jak są zapisywane czy przetwarzane.
* **Repositories (Dostęp do danych):** Klasy odpowiedzialne wyłącznie za kolekcjonowanie i wydobywanie obiektów.
* **Services (Logika Biznesowa):** Miejsce, w którym żyją reguły biznesowe (np. `RentalService` weryfikuje limity, wylicza kary i spina modele z repozytoriami).


### 1. Single Responsibility Principle (Odpowiedzialność klas)
Każda klasa w systemie ma jeden powód do zmiany:
* `Student` / `Laptop` przechowują tylko stan obiektu.
* `EquipmentRepository` służy **tylko i wyłącznie** do przetrzymywania i filtrowania listy sprzętu. Nie wypisuje nic na konsolę i nie nalicza kar.

### 2. Low Coupling (Luźne powiązania)
Starałem się unikać silnego sprzężenia klas (High Coupling). 
* Zastosowałem mechanizm **Dependency Injection**. Serwisy (np. `RentalService`) nie tworzą instancji repozytoriów za pomocą słówka `new` wewnątrz swoich klas. Repozytoria są wstrzykiwane przez konstruktor.
* Wprowadziłem interfejs generyczny `IRepository<T>`, co sprawia, że klasy wyższych warstw polegają na abstrakcji, a nie na konkretnej implementacji.

### 3. High Cohesion (Wysoka spójność)
Klasy zostały zaprojektowane tak, aby ich metody i właściwości ściśle ze sobą współpracowały:
* Rozdzieliłem logikę na osobne serwisy: `UserService`, `EquipmentService` oraz `RentalService`. Każdy serwis operuje tylko na danych w obrębie swojej wąskiej dziedziny.
* Wspólne reguły biznesowe i stałe (jak wysokość kary za dzień zwłoki czy maksymalne limity wypożyczeń) zostały przeniesione do `RentalConstants.cs`, aby uniknąć rozproszenia reguł biznesowych po całym kodzie ("Magic Numbers").
