# C# Dateimanager & Krypto-Tool

![Version](https://img.shields.io/badge/version-1.0.0-blue.svg)
![Status](https://img.shields.io/badge/status-stable-success.svg)
![Language](https://img.shields.io/badge/language-C%23-purple.svg)

Eine modulare Konsolenanwendung für Dateioperrationen und kryptographische Verfahren, entwickelt im Rahmen der FIAE-Umschulung.

## Features

Das Programm vereint verschiedene Werkzeuge in einer zentralen Steuerung:

### Dateimanagement

* **Textdatei lesen:** Liest beliebige Textdateien ein und gibt sie in der Konsole aus.
* **Datei kopieren:** Kopiert Dateien Byte-für-Byte (auch Binärdateien).

### Sicherheit & Kryptographie

* **AES Verschlüsselung:** Symmetrische Verschlüsselung von Texten und Dateien (mit "Müll-Zeichen" Obfuskation).
* **Polybius-Chiffre:** Klassische Verschlüsselung basierend auf einem Matrix-Quadrat und einem Schlüsselwort.
* **PQC Simulation:** Eine Simulation von **Post-Quantum Cryptography** (Kyber-Verfahren), die ein hybrides System (AES + PQC Kapselung) demonstriert.

## Installation & Start

Voraussetzung: [.NET SDK 9.0 oder höher](https://dotnet.microsoft.com/download)

1. **Repository klonen:**

   ```bash
   git clone [https://github.com/Exoshiva/Dateimanager-CSharp.git](https://github.com/Exoshiva/Dateimanager-CSharp.git)
    ```

2. **In den Ordner navigieren:**

    ```bash
    cd Dateimanager1
    ```

3. **Starten:**

    ```bash
    dotnet run
    ```

## Technologien

* **Sprache:** C# (.NET 10.0 / 9.0 kompatibel)
* **Architektur:** Modularer Aufbau mit `Separation of Concerns` (ausgelagerte Klassen).
* **Tools:** Visual Studio Code, GitLens.

## Versionshistorie

* **v1.0.0** - Initialer Release. Refactoring des Hauptmenüs auf Dictionary-Logik und Implementierung aller Basis-Algorithmen.

Erstellt von Dev (Exoshiva) | 2025

---
