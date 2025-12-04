using System;
using System.Collections.Generic; // Für Listen

namespace Dateimanager1
{
    public class Polybius
    {
        // Die Matrix: 6 Zeilen, 5 Spalten 
        private char[,] matrix = new char[6, 5];
        
        // Das Alphabet + Leerzeichen (27 Zeichen). Der Rest der Matrix bleibt leer.
        private string basisAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";

        // Der Konstruktor wird aufgerufen, wenn wir 'new Polybius()' sagen
        public Polybius(string schluesselWort)
        {
            GeneriereMatrix(schluesselWort);
        }

        // Die Hilfsmethode Baut die Matrix basierend auf dem Schlüsselwort
        private void GeneriereMatrix(string key)
        {
            key = key.ToUpper().Replace("J", "I"); // J wird zu I
            string saubererKey = "";

            // 1. Schlüsselwort bereinigen (Doppelte entfernen)
            foreach (char c in key)
            {
                // Wenn Buchstabe noch nicht im Key ist und ein Buchstabe/Leerzeichen ist
                if (!saubererKey.Contains(c) && basisAlphabet.Contains(c))
                {
                    saubererKey += c;
                }
            }

            // 2. Rest des Alphabets auffüllen
            string kompletterString = saubererKey;
            foreach (char c in basisAlphabet)
            {
                if (!kompletterString.Contains(c))
                {
                    kompletterString += c;
                }
            }

            // 3. In die Matrix füllen
            int index = 0;
            for (int zeile = 0; zeile < 6; zeile++)
            {
                for (int spalte = 0; spalte < 5; spalte++)
                {
                    if (index < kompletterString.Length)
                    {
                        matrix[zeile, spalte] = kompletterString[index];
                        index++;
                    }
                }
            }
        }

        // Methode zum Verschlüsseln
        public int[] Verschluesseln(string klartext)
        {
            klartext = klartext.ToUpper();
            List<int> ergebnisListe = new List<int>();

            foreach (char zeichen in klartext)
            {
                // Wir suchen das Zeichen in der Matrix
                bool gefunden = false;
                for (int z = 0; z < 6; z++)
                {
                    for (int s = 0; s < 5; s++)
                    {
                        if (matrix[z, s] == zeichen)
                        {
                            // PDF Logik: Zeile und Spalte als Zahl (z.B. 1 und 1 -> 11)
                            // Achtung: Array Indices starten bei 0, also rechnen wir +1.
                            int code = (z + 1) * 10 + (s + 1); 
                            ergebnisListe.Add(code);
                            gefunden = true;
                            break; // Innere Schleife abbrechen
                        }
                    }
                    if (gefunden) break; // Äußere Schleife abbrechen
                }
            }
            return ergebnisListe.ToArray(); // Liste in Array umwandeln 
        }

        // Methode zum Entschlüsseln
        public string Entschluesseln(int[] zahlenCode)
        {
            string klartext = "";

            foreach (int zahl in zahlenCode)
            {
                // Rückrechnung: 15 -> Zeile 1, Spalte 5
                // Da +1 gerechnet wurde, muss jetzt -1 gerechnet werden für den Index
                int zeile = (zahl / 10) - 1;
                int spalte = (zahl % 10) - 1;

                // Sicherheitscheck, falls ungültige Zahlen kommen
                if (zeile >= 0 && zeile < 6 && spalte >= 0 && spalte < 5)
                {
                    klartext += matrix[zeile, spalte];
                }
                else
                {
                    klartext += "?";
                }
            }
            return klartext;
        }
        
        // Matrix anzeigen lassen (damit man sieht, was passiert)
        public void MatrixAnzeigen()
        {
            Console.WriteLine("\n--- Aktuelle Polybius-Matrix ---");
            for (int z = 0; z < 6; z++)
            {
                for (int s = 0; s < 5; s++)
                {
                    char c = matrix[z, s];
                    if (c == '\0') c = '_'; // markiert leere Felder
                    Console.Write($"[{c}] ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------");
        }
    }
}