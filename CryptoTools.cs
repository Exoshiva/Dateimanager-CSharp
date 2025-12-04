using System;
using System.IO;

namespace Dateimanager1
{
    public static class CryptoTools
    {
        public static void Verschluesseln()
        {
            // Schritt A: Einlesen
            Console.WriteLine("\n--- VERSCHLÜSSELN ---");
            Console.WriteLine("Möchten Sie Text eingeben (T) oder eine Datei laden (D)?");
            string modus = (Console.ReadLine() ?? "").Trim();
            
            string originalText = "";

            if (modus.ToUpper() == "T")
            {
                Console.Write("Geben Sie den Text ein: ");
                originalText = Console.ReadLine() ?? "";
            }
            else if (modus.ToUpper() == "D")
            {
                Console.Write("Welche Datei soll verschlüsselt werden?: ");
                string pfad = Console.ReadLine() ?? "";
                if (File.Exists(pfad))
                {
                    originalText = File.ReadAllText(pfad);
                }
                else
                {
                    Console.WriteLine("Datei nicht gefunden!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }
            // Schritt B: Verschlüsseln
            string geheimText = ""; // Platzhalter für den verschlüsselten Text
            Random zufall = new Random();

            foreach(char echtesZeichen in originalText)
            {
                geheimText += echtesZeichen;

                // Zwei zufällige Müll Zeichen hinzufügen
                char zufall1 = (char)zufall.Next(33, 127); // Druckbare ASCII-Zeichen
                char zufall2 = (char)zufall.Next(33, 127); // Druckbare ASCII-Zeichen

                geheimText += zufall1;
                geheimText += zufall2;
            }

            // Schritt C: Speichern
            Console.WriteLine("\n--- Zu verschlüsselnder Text ---");
            Console.WriteLine(originalText);
            Console.WriteLine("\n--- Speichern ---");
            Console.Write("Wie soll die Datei heißen? (z.B. Geheim.txt): ");
            string zielPfad = Console.ReadLine() ?? "Geheim.txt";
            
            // Hier passiert das eigentliche Speichern auf die Festplatte:
            File.WriteAllText(zielPfad, geheimText);
            
            Console.WriteLine($"\nErfolg! Der Text wurde verschlüsselt in '{zielPfad}' gespeichert.");

        } 

        public static void Entschluesseln()
        {
            Console.WriteLine("\n--- ENTSCHLÜSSELUNG ---");
            Console.Write("Welche Datei soll entschlüsselt werden?: ");
            string pfad = Console.ReadLine() ?? "";

            if (File.Exists(pfad))
            {
                string geheimText = File.ReadAllText(pfad);
                string klarText = "";

                // Logik: Wir lesen nur jedes 3. Zeichen (Index 0, 3, 6, 9...)
                // i += 3 ist hier der entscheidende Trick!
                for (int i = 0; i < geheimText.Length; i += 3)
                {
                    klarText += geheimText[i];
                }

                Console.WriteLine("\n--- Entschlüsselter Text ---");
                Console.WriteLine(klarText);
                
                // Optional: Ergebnis speichern
                Console.WriteLine("\nSoll das Ergebnis gespeichert werden? (j/n)");
                string antwort = Console.ReadLine() ?? "";
                if (antwort.ToLower() == "j")
                {
                     Console.Write("Dateiname: ");
                     string ziel = Console.ReadLine() ?? "decoded.txt";
                     File.WriteAllText(ziel, klarText);
                     Console.WriteLine("Gespeichert!");
                }
            }
            else
            {
                Console.WriteLine("Datei nicht gefunden!");
            }
        }
    }
    
}