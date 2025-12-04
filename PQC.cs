using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/*
    Dieses Programm simuliert eine Post-Quantum Cryptography (PQC) Verschlüsselung.
*/
namespace Dateimanager1
{
    ///<summary>
    /// Diese Klasse simuliert eine Post-Quantum Cryptography (PQC) Verschlüsselung
    /// Es wird AES für die Daten (schnell) und PQC für den Schlüsselaustausch (sicher) verwenden.
    ///</summary>
    public class PQCSimulator
    {
        // Simuliert "Kyber-1024" Public Key
        public string PublicKey {get; private set;}
        // Simuliert "Kyber-1024" Private Key
        private string PrivateKey;

        public PQCSimulator ()
        {
            //Erklärung:
            // PQC-Schlüssel sind mathematisch sehr komplex und können nicht einfach generiert werden.
            // Daher simuliere ich hier ein Schlüsselpaar mit zufälligen Strings. (In echt käme das aus einer Libary)
            PrivateKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            PublicKey = "PQC-KYBER-KEY-" + Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        ///<summary>
        /// Verschlüsselt eine Datei quantensicher
        /// Ablauf:
        /// 1. Generiere ein zufälliges AES-Schlüssel (Session-Key).
        /// 2. Verschlüssele die Datei mit AES.
        /// 3. "Kapselt" den AES-Schlüssel mit dem PQC Public Key.
        /// </summary>
        public void DateiVerschluesselung(string klartextPfad, string zielPfad)
        {
            if (!File.Exists(klartextPfad))
            {
                Console.WriteLine("Fehler: Quelldatei nicht gefunden!");
                return;
            }

            // Schritt A: Einen frischen AES-Schlüssel erzeugen
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256; // AES-256
                aes.GenerateKey();
                aes.GenerateIV();

                // Schritt 2: Die Datei mit diesem AES-Schlüssel verschlüsseln
                byte[] dateiInhalt = File.ReadAllBytes(klartextPfad);
                byte[] verschlüsselteDaten;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(dateiInhalt, 0, dateiInhalt.Length);
                    }
                    verschlüsselteDaten = ms.ToArray();
                }

                // Schritt 3: "Kapseln" des AES-Schlüssels mit dem PQC Public Key (simuliert)
                // In der Realität würde hier stehen: Kyber.Encapsulate(aes.Key, PublicKey);
                string gekapselterKey = $"[PQC-Kapsel MIT {PublicKey}]:" + Convert.ToBase64String(aes.Key);

                // Schritt 4: Alles in die Zieldatei schreiben
                // Ein einfaches Textformat, um Key, IV und Daten zu speichern
                using (StreamWriter sw = new StreamWriter(zielPfad))
                {
                    sw.WriteLine("--- PQC HYBRID FILE ---");
                    sw.WriteLine("IV: " + Convert.ToBase64String(aes.IV));
                    sw.WriteLine("EncapsulatedKey: " + gekapselterKey);
                    sw.WriteLine("Data: " + Convert.ToBase64String(verschlüsselteDaten));
                }

                Console.WriteLine($"Erfolg! Datei wurde hybrid verschlüselt in '{zielPfad}' gespeichert.");
                Console.WriteLine($"(AES verschlüsselt die Daten, PQC schützt den AES-Key)");   
            }
                
        }

        ///<summary>
        /// Entschlüsselt eine PQC-verschlüsselte Datei
        /// Ablauf: 
        /// 1. Liest die PQC-Kapselung aus der Datei.
        /// 2. "Entkapselt" den AES-Schlüssel mit dem Private-Key.
        /// 3. Entschlüsselt die Datei mit AES.
        /// </summary>  
        public void DateiEntschluesseln(string pqcDateiPfad, string zielPfad)
        {
            if (!File.Exists(pqcDateiPfad))
            {
                Console.WriteLine("Fehler: Datei nicht gefunden.");
                return;
            }

            try
            {
                string[] zeilen = File.ReadAllLines(pqcDateiPfad);
                // KORREKTUR: Hier stand "File", beim Speichern aber "FILE". Muss gleich sein.
                if (zeilen.Length < 4 || zeilen[0] != "--- PQC HYBRID FILE ---")
                {
                    Console.WriteLine("Fehler: Das ist keine gültige PQC-Datei aus diesem Programm");
                    return;
                }

                // Schritt 1: Lese IV, Kapselung und Daten
                string ivString = zeilen[1].Replace("IV: ", ""  );
                string kapselString = zeilen[2].Replace("EncapsulatedKey: ", "");
                string dataString = zeilen[3].Replace("Data: ", "");

                // 2. Den AES-Key "entkapseln" (simuliert)
                // Wir tun so, als würden wir den PrivateKey, um die Kapsel zu öffnen
                // In echt: Kyber.Decapsulate(kapselString, PrivateKey);
                // Da es eine Simulation ist, schneiden wir den Key einfach ab
                string keyBase64 = kapselString.Substring(kapselString.IndexOf("]:") + 2);

                byte[] iv = Convert.FromBase64String(ivString);
                byte[] key = Convert.FromBase64String(keyBase64);
                byte[] chiperBytes = Convert.FromBase64String(dataString);

                // Schritt 3: Mit dem zurückgewonnenen AES-Key die Datei entschlüsseln
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    
                    using (var ms = new MemoryStream(chiperBytes))
                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var reader = new BinaryReader(cs)) // BinaryReader zum Lesen der Bytes
                    {
                        // Wir lesen alles in einem Puffer
                        // KORREKTUR: Variable ohne 'ü', damit sie zum Rest passt
                        byte[] entschluesselteBytes = new byte[chiperBytes.Length];
                        // Liest die entschlüsselten Bytes
                        // Hinweis: Die Länge ist hier vereinfachtr, in echt müsste man genauer lesen
                        int anzahl = reader.Read(entschluesselteBytes, 0, entschluesselteBytes.Length);

                        // Nur die echten Bytes werden geschrieben
                        using (var fs = new FileStream(zielPfad,  FileMode.Create))
                        {
                            fs.Write(entschluesselteBytes, 0, anzahl);
                        }
                    }
                }
                Console.WriteLine($"Erfolg! Datei wurde entschlüsselt: {zielPfad}");
            
            } // KORREKTUR: Try-Block hier geschlossen
            catch (Exception ex)
            {
                Console.WriteLine("Entschlüsselung fehlgeschlagen: " + ex.Message);
            }
        }

    }
}