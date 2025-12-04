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
        }

        public void StartMenue()
        {
            bool imFragebogen = true;
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