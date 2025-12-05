using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO; 
using Dateimanager1; // Findet deine Klassen

namespace DateiManagerGUI;

public partial class MainWindow : Window
{
    // Die Werkzeuge vorbereiten
    private PQCSimulator _pqcSimulator = new PQCSimulator();
    private Questionaire _fragebogen = new Questionaire();

    public MainWindow()
    {
        InitializeComponent();
    }

    // --- TAB 1: DATEI MANAGER ---

    private void BtnLoad_Click(object sender, RoutedEventArgs e)
    {
        string pfad = txtFilePath.Text; // Name passt jetzt zum XAML

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

        if (rbPolybius.IsChecked == true) // Name passt jetzt!
        {
            string key = "GEHEIM"; 
            Polybius p = new Polybius(key);
            int[] cipherArray = p.Verschluesseln(klartext);
            txtKryptoOutput.Text = string.Join(" ", cipherArray);
        }
        else if (rbPQC.IsChecked == true) // Name passt jetzt!
        {
            string tempFile = "temp_input.txt";
            string targetFile = "safe.pqc";
            File.WriteAllText(tempFile, klartext);

            _pqcSimulator.DateiVerschluesselung(tempFile, targetFile);
            
            txtKryptoOutput.Text = $"Datei verschlüsselt als '{targetFile}'.";
        }
        else
        {
             MessageBox.Show("Wähle Polybius oder PQC aus.");
        }
    }

    // --- TAB 3: FRAGEBOGEN (Die neue Logik) ---

    private void BtnStartQuestionnaire_Click(object sender, RoutedEventArgs e)
    {
        // Versuchen, die ID aus dem Textfeld zu lesen
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