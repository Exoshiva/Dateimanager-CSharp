using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Dateimanager1
{
    class Program
    {
        // TEIL 1: Das Hauptprogramm 
        static void Main(string[] args)
        {
            bool programmLauft = true;

            // Das Dictonary für die Menüauswahl

            var menue = new Dictionary<string, Action>()
            {
                {"1", DateiTools.TextDateiLesen},
                {"2", DateiTools.DateiKopieren},
                {"3", CryptoTools.Verschluesseln},
                {"4", CryptoTools.Entschluesseln},
                // Hier werden die Wrapper (Hilfs-Methoden) für Polybius und PQC aufgerufen
                {"5", () => PolybiusWrapper()},
                {"6", () => PqcWrapper()},
                {"7", () => QuestionaireWrapper()}, // Startet das neue Fragebogen-Menü
                // Hier wird das Programm beendet
                {"8", () => programmLauft = false}
            };

            // Beginn der Hauptschleife
            while (programmLauft)
            {   
                Console.Clear(); // Konsole säubern für bessere Übersicht
                Console.WriteLine("Was möchten Sie tun?");
                Console.WriteLine("1 - Textdatei lesen (Aufgabe 1)");
                Console.WriteLine("2 - Datei kopieren (Aufgabe 2)");
                Console.WriteLine("3 - Verschlüsseln (Aufgabe 3)"); 
                Console.WriteLine("4 - Entschlüsseln (Aufgabe 3)");
                Console.WriteLine("5 - Polybius Verschlüsselung (Aufgabe 4)");
                Console.WriteLine("6 - Datei mit PQC verschlüsseln (Bonusaufgabe)"); 
                Console.WriteLine("7 - Fragebogen Menü (Fragebogen (Psychologisches Institut)");
                Console.WriteLine("8 - Beenden"); 
                Console.Write("Auswahl: ");

                string wahl = Console.ReadLine() ?? "";
                // Durchführen der gewählten Aktion (dies ersetzt die lange if-else Kette)
                if(menue.ContainsKey(wahl))
                {
                    try 
                    {
                        menue[wahl].Invoke();                 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte versuchen Sie es erneut.");
                }
                if(programmLauft)
                {
                    Console.WriteLine("\nDrücken Sie Enter um zum Hauptmenü zurückzukehren...");
                }
            } 

            Console.WriteLine("Programm beendet. Tschüss!");
        } // Ende der Hauptschleife

        // Hilfsmethoden (Wrapper)
        static void PolybiusWrapper()
        {
            Console.WriteLine("\n--- POLYBIUS VERSCHLÜSSELUNG ---");
            Console.Write("Geben Sie das Schlüsselwort ein: ");
            string key = Console.ReadLine() ?? "";
            Polybius p = new Polybius(key); // Hier erstellen wir eine Instanz der Polybius-Klasse
            Console.WriteLine("Geben Sie den Klartext ein: ");
            string klartext = Console.ReadLine() ?? "";
            int[] ergebnis = p.Verschluesseln(klartext);
            Console.WriteLine("Ergebnis: " + string.Join(" ", ergebnis));
        }

        static void PqcWrapper()
        {
            // Hier wird das Objekt der PQC-Simulation erstellt
            PQCSimulator simulator = new PQCSimulator();
            Console.WriteLine("\n--- PQC HYBRID SIMULATION --- ");
            PQCSimulator();
            Console.WriteLine("(V)erschlüsseln oder (E)ntschlüsseln?");
            string modus = (Console.ReadLine() ?? "").ToUpper();
            if (modus == "V")
            {
                Console.Write("Welche Datei soll verschlüsselt werden? (Pfad): ");
                string quelle = Console.ReadLine() ?? "";
                Console.Write("Wie soll die Datei heißen? (z.B. safe.pqc): ");
                string ziel = Console.ReadLine() ?? "";
                simulator.DateiVerschluesselung(quelle, ziel);
            }
            else if (modus == "E")
            {
                Console.Write("Welche PQC-Datei soll entschlüsselt werden?: ");
                string quelle = Console.ReadLine() ?? "";
                Console.Write("Wie soll die wiederhergestellte Datei heißen?: ");
                string ziel = Console.ReadLine() ?? "original_restored.txt";
                simulator.DateiEntschluesseln(quelle, ziel);
            }
            else
            {
                Console.WriteLine("Ungültige Auswahl.");
            }
        }

        // TEIL 2: Die Methoden des Programms

        // Hilfsmethode für "Drücken Sie Enter.."
        static void DrueckeEnter()
        {
            Console.WriteLine("\nDrücken Sie eine Taste, um fortzufahren...");
            Console.ReadLine();
        }
        
        // Wrapper-Methode für das Fragebogen-Menü (Neu hinzugefügt)
        static void QuestionaireWrapper()
        {
            Questionaire q = new Questionaire();
            q.StartMenue();
        }
        
    }
}


