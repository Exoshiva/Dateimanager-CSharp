using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

/*
    Dies ist die Datei Questionaire.cs, die das Fragebogen-Menü implementiert.
    Sie enthält Methoden zum Starten und Beenden des Fragebogens sowie zur Anzeige des Menüs.
*/


namespace Dateimanager1
{
    // Datenklasse für eine einzelne Frage
    public class FrageModell
    {
        public string FrageText {get; set;} = "";
        public List<string> Antworten {get; set;} = new List<string>();
        public int RichtigeAntwortIndex {get; set;} // 0, 1 oder 2
    }

    // Datenklasse für Probanden 
    public class Proband
    {
        public int ProbandenID {get; set;}
        public List<long> AntwortZeitMs {get; set;} = new List<long>();
        public int AnzahlKorrekteAntworten {get; set;}
    }

    public class Questionaire
    {
        // Hier werden alle durchgeführten Tests gespeichert
        private static List<Proband> alleErgebnisse = new List<Proband>();

        // Hier werden alle Fragen gespeichert
        private static List<FrageModell> fragenKatalog = new List<FrageModell>();

        // Der Konstruktor Läd die Fragen
        private void InitialisiereFragen()
        {
            // Fragekatalog noch nicht implementiert
            fragenKatalog.Clear();

            // Frage 1
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Thomas Mann schrieb welchen Roman?",
                Antworten = new List<string>{"Die Pest", "Der Zauberberg", "Der Unterran"},
                RichtigeAntwortIndex = 1
            });

            // Frage 2
            fragenKatalog.Add(new FrageModell
            {
                
                FrageText = "Napoleon wurde entgültig besiegt in der Schlacht von?",
                Antworten = new List<string>{"Waterloo", "Marego", "Austerlitz"},
                RichtigeAntwortIndex = 0
            });

            // Frage 3
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Der Schall pflanzt sich in der Luft mit welcher Geschwindigkeit fort?",
                Antworten = new List<string>{"33 km/h", "330 m/s", "3300 m/s"},
                RichtigeAntwortIndex = 1
            });

            // Frage 4
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Welche Erfindung machte Thomas Alva Edison nicht?",
                Antworten = new List<string> {"Die Glühlampe", "Den Phonograph", "Die Enigma"},
                RichtigeAntwortIndex = 2
            });

            // Frage 5
            fragenKatalog.Add(new FrageModell
            {
               FrageText = "Welcher Architekt nannte sich Le Cornuiser?",
               Antworten = new List<string> {"Charles-Edouard Jeanert-Gris", "Frank Wright", "Mies van der Rohe"},
               RichtigeAntwortIndex =  0
            });

        }

        public void StartMenue()
        {
            bool imFragebogen = true;

            // Lokales Dictonary nur für dieses Untermenü
            var subMenue = new Dictonary<string, Action>()
            {
                {"1", StartVersuch}, // Ruft Methode StarteVersuch() auf
                {"2", StartVersuch}, // Ruft Methode ZeigeStatistik()auf
                {"3", () => imFragebogen = false}
            };

            while(imFragebogen)
            {
                Console.Clear(); // Konsole säubern für bessere Übersicht
                Console.WriteLine("=== PSYCHOLOGISCHES INSTITUT - FRAGEBOGEN MENÜ ===");
                Console.WriteLine("Teiluntersuchung: 5 Fragen");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("<1> Starte neuen Fragebogen"); 
                Console.WriteLine("<2> Auswertung der Versuche anzeigen");
                Console.WriteLine("<3> Zurück zum Hauptmenü");
                Console.WriteLine("\nIhre Wahl: ");

                string wahl = Console.ReadLine() ?? ""; 

                // Hier nutze ich switch damit der User bei Falscheingabe wieder ins Fragebogen Menü zurückkehren 
                // kann ohne das ganze Programm neu durchlaufen zu müssen

                switch(wahl)
                {
                    case "1":
                    StarteVersuch();
                    break; // Springt aus dem switch, Schleife läuft weiter

                    case "2":
                    ZeigeStatistik();
                    break; // Springt aus dem switch, Schleife läuft weiter

                    case "3":
                    // Nur hier wird die Schleife beendet
                    imFragebogen = false;
                    break;

                    default:
                    // Das ist der "Else"-Fall für Falscheingaben
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUngültige Eingabe! Bitte 1, 2, oder 3 wählen.");
                    Console.ResetColor();
                    Console.WriteLine("Drücken Sie Enter..");
                    Console.ReadLine();
                    // imFragebogen ist immer noch true -> Menü kommt wieder!
                    break;
                } 
            }
        }

        private void StarteVersuch()
        {
            // Noch nicht implementiert
        }
        private void ZeigeStatistik()
        {
            // Noch nicht implementiert
        }
    }
}