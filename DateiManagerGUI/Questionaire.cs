using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text; // Neu für StringBuilder (Statistik)
using Dateimanager1;

/*
    Dies ist die Datei Questionaire.cs, die das Fragebogen-Menü implementiert.
    Sie enthält Methoden zum Starten und Beenden des Fragebogens sowie zur Anzeige des Menüs.
    (Angepasst für WPF: Keine Console-Befehle mehr)
*/


namespace Dateimanager1
{
    // Datenklasse für eine einzelne Frage
    public class FrageModell
    {
        public string FrageText { get; set; } = "";
        public List<string> Antworten { get; set; } = new List<string>();
        public int RichtigeAntwortIndex { get; set; } // 0, 1 oder 2
    }

    // Datenklasse für Probanden 
    public class Proband
    {
        public int ProbandenID { get; set; }
        public List<long> AntwortZeitMs { get; set; } = new List<long>();
        public int AnzahlKorrekteAntworten { get; set; }
    }

    public class Questionaire
    {
        // Hier werden alle durchgeführten Tests gespeichert
        private static List<Proband> alleErgebnisse = new List<Proband>();

        // Hier werden alle Fragen gespeichert
        private static List<FrageModell> fragenKatalog = new List<FrageModell>();

        // --- ZUSTANDS-VARIABLEN FÜR DIE GUI (Neu) ---
        private Proband _aktuellerProband;
        private int _aktuellerFrageIndex;
        private DateTime _startZeitFrage;

        // Der Konstruktor Läd die Fragen
        public Questionaire()
        {
            InitialisiereFragen();
        }

        private void InitialisiereFragen()
        {
            // Fragekatalog noch nicht implementiert -> Doch, hier:
            fragenKatalog.Clear();

            // Frage 1
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Thomas Mann schrieb welchen Roman?",
                Antworten = new List<string> { "Die Pest", "Der Zauberberg", "Der Untertan" },
                RichtigeAntwortIndex = 1
            });

            // Frage 2
            fragenKatalog.Add(new FrageModell
            {

                FrageText = "Napoleon wurde entgültig besiegt in der Schlacht von?",
                Antworten = new List<string> { "Waterloo", "Marengo", "Austerlitz" },
                RichtigeAntwortIndex = 0
            });

            // Frage 3
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Der Schall pflanzt sich in der Luft mit welcher Geschwindigkeit fort?",
                Antworten = new List<string> { "33 km/h", "330 m/s", "3300 m/s" },
                RichtigeAntwortIndex = 1
            });

            // Frage 4
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Welche Erfindung machte Thomas Alva Edison nicht?",
                Antworten = new List<string> { "Die Glühlampe", "Den Phonograph", "Die Enigma" },
                RichtigeAntwortIndex = 2
            });

            // Frage 5
            fragenKatalog.Add(new FrageModell
            {
                FrageText = "Welcher Architekt nannte sich Le Corbusier?",
                Antworten = new List<string> { "Charles-Edouard Jeanneret-Gris", "Frank Wright", "Mies van der Rohe" },
                RichtigeAntwortIndex = 0
            });

        }

        // Ersetzt "StarteVersuch" (Teil 1: Initialisierung)
        public void StarteNeuenVersuch(int probandenID)
        {
            // --- NEUER VERSUCH ---
            
            _aktuellerProband = new Proband();
            _aktuellerProband.ProbandenID = probandenID;
            _aktuellerProband.AnzahlKorrekteAntworten = 0;
            _aktuellerProband.AntwortZeitMs = new List<long>();

            _aktuellerFrageIndex = 0;
            // Zeit Starten (initial für die erste Frage vorbereiten)
            _startZeitFrage = DateTime.Now;
        }

        // Ersetzt die foreach-Schleife: Liefert die nächste Frage an die GUI
        public FrageModell? GetAktuelleFrage()
        {
            if (_aktuellerFrageIndex < fragenKatalog.Count)
            {
                // Zeit Starten (Reset für die neue Frage)
                _startZeitFrage = DateTime.Now;
                return fragenKatalog[_aktuellerFrageIndex];
            }
            return null; // Keine Fragen mehr
        }

        // Ersetzt "StarteVersuch" (Teil 2: Auswertung der Antwort)
        public void BeantworteFrage(int gewaehlterIndex)
        {
            if (_aktuellerFrageIndex >= fragenKatalog.Count) return;

            // Zeit Stoppen
            DateTime ende = DateTime.Now;
            TimeSpan dauer = ende - _startZeitFrage;

            // Dauer in ms speichern
            _aktuellerProband.AntwortZeitMs.Add((long)dauer.TotalMilliseconds);

            // Antworten prüfen ("1" = Index 0, kommt jetzt direkt von der GUI)
            FrageModell aktuelleFrage = fragenKatalog[_aktuellerFrageIndex];
            
            if (gewaehlterIndex == aktuelleFrage.RichtigeAntwortIndex)
            {
                _aktuellerProband.AnzahlKorrekteAntworten++;
            }

            // Index hochzählen für die nächste Frage
            _aktuellerFrageIndex++;
        }

        // Prüft, ob wir fertig sind (damit die GUI weiß, wann Schluss ist)
        public bool IstFertig()
        {
            return _aktuellerFrageIndex >= fragenKatalog.Count;
        }

        // Ersetzt "StarteVersuch" (Teil 3: Speichern)
        public void SpeichereErgebnis()
        {
            // 3.Speichern und Ende
            alleErgebnisse.Add(_aktuellerProband);
        }

        // Ersetzt "ZeigeStatistik" (Gibt jetzt Text zurück statt Console.WriteLine)
        public string GetStatistikText()
        {
            // ****** AUSWERTUNG **********
            StringBuilder sb = new StringBuilder();

            if (alleErgebnisse.Count == 0)
            {
                sb.AppendLine("Noch keine Versuche durchgeführt.");
            }
            else
            {
                sb.AppendLine($"Anzahl der Probanden: {alleErgebnisse.Count}");
                sb.AppendLine("--------------------------------");

                // Hier baue ich später die detaillierte Statistik aus der PDF ein.
                // Für jetzt zeigen ich eine einfache Übersicht:
                foreach (var p in alleErgebnisse)
                {
                    sb.AppendLine($"Proband {p.ProbandenID}: {p.AnzahlKorrekteAntworten} von 5 richtig.");
                }
            }

            return sb.ToString();
        }
    }
}