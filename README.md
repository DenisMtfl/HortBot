# HortBot

HortBot ist ein Programm welches die unoffizielle API von `HortPro` nutzt um die Anwesenheit und Abwesenheit eines Kindes
in einem TelegramBot anzeigen kann.

![image](https://user-images.githubusercontent.com/1503277/221037838-8b9b760c-1908-411f-8cbc-c49ac1407c72.png)

Folgend der Ablauf des Programms:

* Automatische Anmeldung als Nutzer bei `HortPro`
* Cookie der Anmeldung in eine Datei speichern (wird für weitere API Aufrufe benötigt)
* Holen der registrieten Kinder mittels API (aktuell wird nur 1 Kind untersützt)
* Holen der aktuellen Präsenz des 1. Kindes (Anwesenheit/Abwesenheit)
* In einem TelegramBot die Meldung Präsenz des 1. Kindes an die Bot-Mitglieder senden

Das Holen der aktuellen Präsenz passiert jede Minute erneut so dass der TelegramBot immer auf dem aktuellen Stand ist.

# Konfiguration

In der Datei `appsettings.json` muss der TelegramBot-Token und die Login Informationen für HortPro eingegeben werden.

# Kompilieren und Ausführen

Entwickelt wurde das Programm in *C#* **.NET 7.0** mit **Microsoft Visual Studio 2022**
Mittels **Microsoft Visual Studio 2022** kann es kompiliert und ausgeführt werden.

# Publishing für Linux

dotnet publish -c Release -r linux-x64 --self-contained=true -p:PublishSingleFile=true -p:GenerateRuntimeConfigurationFiles=true -o artifacts

# Docker Image

https://hub.docker.com/repository/docker/deniscubic/hortbot/general

# Installation

Eine Installation steht aktuell noch nicht bereit.

### Abgrenzung:
HortBot wurde eigenständig entwickelt und arbeitet nicht mit `hortpro.de` bzw. deren Unternehmen/Entwickler zusammen.
Für HortBot wird eine **unoffizielle API** von `HortPro` genutzt!


###### - Entwickelt 2023 in Berlin/Germany -
