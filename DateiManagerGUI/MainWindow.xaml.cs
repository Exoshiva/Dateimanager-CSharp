using System.Runtime.InteropServices; // WICHTIG für den Effekt
using System.Windows;
using System.Windows.Interop; // WICHTIG
using System.IO; 
using System.Windows.Media; // Für Farben/Brushes
using Dateimanager1; 

namespace DateiManagerGUI;

public partial class MainWindow : Window
{
    // --- WINDOWS 11 GLASS EFFEKT SETUP ---
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_SYSTEMBACKDROP_TYPE = 38;
    private const int DWMSBT_TRANSIENTWINDOW = 3; // 3 = Acrylic (Starkes Milchglas)

    // Deine Tools
    private PQCSimulator _pqcSimulator = new PQCSimulator();
    private Questionaire _fragebogen = new Questionaire();
    
    // Status für Dark Mode (Standard: aus)
    private bool _isDarkMode = false;

    public MainWindow()
    {
        InitializeComponent();
        
        // Startet den Effekt, sobald das Fenster geladen ist
        this.Loaded += (s, e) => EnableGlassEffect();
    }

    private void EnableGlassEffect()
    {
        var hwnd = new WindowInteropHelper(this).Handle;

        // 1. Wir erzwingen LIGHT MODE (0), damit die Schrift dunkel bleibt
        int useDarkMode = 0; 
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDarkMode, sizeof(int));

        // 2. Wir aktivieren den Hintergrund-Blur (Acrylic)
        int backdropType = DWMSBT_TRANSIENTWINDOW; 
        DwmSetWindowAttribute(hwnd, DWMWA_SYSTEMBACKDROP_TYPE, ref backdropType, sizeof(int));
    }

    // --- DEINE EVENTS (Original) ---

    private void BtnTheme_Click(object sender, RoutedEventArgs e)
    {
        // Wir lassen die Funktion leer oder geben eine Info, 
        // damit das Umschalten das Design nicht zerschießt.
        _isDarkMode = !_isDarkMode;
        // Optional: Hier könntest du später Farben umschalten, wenn gewollt.
    }

    // --- TAB 1: DATEI MANAGER ---

    private void BtnLoad_Click(object sender, RoutedEventArgs e)
    {
        string pfad = txtFilePath.Text; 

        if (File.Exists(pfad))
        {
            try 
            {
                string inhalt = File.ReadAllText(pfad);
                txtFileContent.Text = inhalt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Datei nicht gefunden!");
        }
    }

    // --- TAB 2: KRYPTO LABOR ---

    private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
    {
        string klartext = txtKryptoInput.Text;
        
        if (string.IsNullOrEmpty(klartext))
        {
            MessageBox.Show("Bitte erst Text eingeben!");
            return;
        }

        if (rbPolybius.IsChecked == true) 
        {
            string key = "GEHEIM"; 
            Polybius p = new Polybius(key);
            int[] cipherArray = p.Verschluesseln(klartext);
            txtKryptoOutput.Text = string.Join(" ", cipherArray);
        }
        else if (rbPQC.IsChecked == true) 
        {
            string tempFile = "temp_input.txt";
            string targetFile = "safe.pqc";
            File.WriteAllText(tempFile, klartext);

            _pqcSimulator.DateiVerschluesselung(tempFile, targetFile);
            
            txtKryptoOutput.Text = $"Datei verschlüsselt als '{targetFile}'.";
        }
        else
        {
             // Fallback: Einfach anzeigen (oder AES implementieren)
             txtKryptoOutput.Text = "AES (Standard) gewählt - hier könnte deine AES Logik stehen.";
        }
    }

    // --- TAB 3: FRAGEBOGEN ---

    private void BtnStartQuestionnaire_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(txtProbandID.Text, out int id))
        {
            _fragebogen.StarteNeuenVersuch(id);

            // Ansicht umschalten
            pnlStartMenu.Visibility = Visibility.Collapsed;
            pnlFrage.Visibility = Visibility.Visible;

            ZeigeNaechsteFrage();
        }
        else
        {
            MessageBox.Show("Bitte gültige ID eingeben (Zahl)!");
        }
    }

    private void ZeigeNaechsteFrage()
    {
        var frage = _fragebogen.GetAktuelleFrage();

        // RadioButtons zurücksetzen
        rbAntwort1.IsChecked = false;
        rbAntwort2.IsChecked = false;
        rbAntwort3.IsChecked = false;

        if (frage != null)
        {
            lblFrageText.Text = frage.FrageText;
            rbAntwort1.Content = frage.Antworten[0];
            rbAntwort2.Content = frage.Antworten[1];
            rbAntwort3.Content = frage.Antworten[2];
        }
    }

    private void BtnWeiter_Click(object sender, RoutedEventArgs e)
    {
        int gewaehlt = -1;
        if (rbAntwort1.IsChecked == true) gewaehlt = 0;
        if (rbAntwort2.IsChecked == true) gewaehlt = 1;
        if (rbAntwort3.IsChecked == true) gewaehlt = 2;

        if (gewaehlt == -1) 
        {
            MessageBox.Show("Bitte eine Antwort wählen!");
            return;
        }

        _fragebogen.BeantworteFrage(gewaehlt);

        if (_fragebogen.IstFertig())
        {
            _fragebogen.SpeichereErgebnis();
            MessageBox.Show("Test beendet! Danke.");
            
            // Zurück zum Start
            pnlFrage.Visibility = Visibility.Collapsed;
            pnlStartMenu.Visibility = Visibility.Visible;
            txtProbandID.Text = "";
        }
        else
        {
            ZeigeNaechsteFrage();
        }
    }

    private void BtnStatistik_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(_fragebogen.GetStatistikText());
    }
}