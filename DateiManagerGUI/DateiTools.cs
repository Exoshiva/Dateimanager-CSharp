using System;
using System.IO;

namespace Dateimanager1
{
    public static class DateiTools
    {
        public static void TextDateiLesen()
        {
            Console.Write("Bitte geben Sie den Dateinamen (z.B. Test.txt) ein: ");
            string dateiName = Console.ReadLine() ?? ""; 

            if (File.Exists(dateiName))
            {
                Console.WriteLine("\n--- Dateiinhalt ---");
                using (StreamReader reader = new StreamReader(dateiName))
                {
                    string? zeile;
                    while ((zeile = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(zeile);
                    }
                }
            }
            else
            {
                Console.WriteLine("Fehler: Die Datei wurde nicht gefunden!");
            }
        }

        public static void DateiKopieren()
        {
            Console.Write("Quell-Datei (Was kopieren?): ");
            string quelle = Console.ReadLine() ?? ""; 

            Console.Write("Ziel-Datei (Wie soll die Kopie heißen?): ");
            string ziel = Console.ReadLine() ?? ""; 

            if (File.Exists(quelle))
            {
                using (FileStream fsRead = new FileStream(quelle, FileMode.Open, FileAccess.Read))
                using (FileStream fsWrite = new FileStream(ziel, FileMode.Create, FileAccess.Write))
                {
                    Console.WriteLine("Kopiervorgang läuft... (Byte für Byte)");
                    int aktuellesByte;

                    while ((aktuellesByte = fsRead.ReadByte()) != -1)
                    {
                        fsWrite.WriteByte((byte)aktuellesByte);
                    }
                }
                Console.WriteLine("Kopie erfolgreich erstellt!");
            }
            else
            {
                Console.WriteLine("Die Quell-Datei existiert nicht!");
            }
        }
    }
    
}