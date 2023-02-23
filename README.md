# HortBot

HortBot ist ein Programm welches die unoffizielle API von `HortPro` nutzt um die Anwesenheit und Abwesenheit eines Kindes
in einem TelegramBot anzeigen kann.

Folgend der Ablauf des Programms:

* Automatische Anmeldung als Nutzer bei `HortPro`
* Cookie der Anmeldung in eine Datei speichern (wird f�r weitere API Aufrufe ben�tigt)
* Holen der registrieten Kinder mittels API (aktuell wird nur 1 Kind unters�tzt)
* Holen der aktuellen Pr�senz des 1. Kindes (Anwesenheit/Abwesenheit)
* In einem TelegramBot die Meldung Pr�senz des 1. Kindes an die Bot-Mitglieder senden

Das Holen der aktuellen Pr�senz passiert jede Minute erneut so dass der TelegramBot immer auf dem aktuellen Stand ist.

# Konfiguration

In der Datei `appsettings.json` muss der TelegramBot-Token und die Login Informationen f�r HortPro eingegeben werden.

# Kompilieren und Ausf�hren

Entwickelt wurde das Programm in *C#* **.NET 7.0** mit **Microsoft Visual Studio 2022**
Mittels **Microsoft Visual Studio 2022** kann es kompiliert und ausgef�hrt werden.

# Installation

Eine Installation steht aktuell noch nicht bereit.

### Abgrenzung:
HortBot wurde eigenst�ndig entwickelt und arbeitet nicht mit `hortpro.de` bzw. deren Unternehmen/Entwickler zusammen.
F�r HortBot wird eine **unoffizielle API** von `HortPro` genutzt!


###### - Entwickelt 2023 in Berlin/Germany -