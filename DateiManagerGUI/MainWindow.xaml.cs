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
using DateiManager1; // zum finden der alten Klassen

namespace DateiManagerGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    // --- TAB 1: DATEI MANAGER ---

    // Button: Laden (Ersetzt "1 - Textdatei lesen")
    private void BtnLoad_Click(object sender, RoutedEventArgs e)
    {
        // Statt Console.ReadLine() nehmen wir den Text aus der TextBox
        // Hinweis: Du musst deiner TextBox im XAML den Namen x:Name="txtPfad" geben!
        // und dem Ausgabefeld x:Name="txtOutput"
        
        /* Beispiel Code (Du musst die Namen im XAML anpassen):
        string pfad = txtPfad.Text; 
        if(File.Exists(pfad)) 
        {
            string inhalt = File.ReadAllText(pfad);
            txtOutput.Text = inhalt;
        }
        else 
        {
            MessageBox.Show("Datei nicht gefunden!");
        }
        */
        MessageBox.Show("Hier muss noch der Code rein, sobald die XAML Namen stimmen!");
    }

    // --- TAB 2: KRYPTO LABOR ---

    // Button: Verschlüsseln (Ersetzt "3 - Verschlüsseln" und Wrapper)
    private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
    {
        // Wir prüfen, welcher RadioButton gewählt ist
        // Angenommen du hast RadioButtons: rbAES, rbPolybius, rbPQC
        
        /*
        if (rbPolybius.IsChecked == true)
        {
             // Logik aus PolybiusWrapper()
             string key = "GEHEIM"; // Oder aus einem extra Textfeld
             Polybius p = new Polybius(key);
             // ... verschlüsseln ...
        }
        else if (rbPQC.IsChecked == true)
        {
             // Logik aus PqcWrapper()
             _pqcSimulator.DateiVerschluesselung(txtInput.Text, "safe.pqc");
        }
        else
        {
             // Standard AES
        }
        */
        MessageBox.Show("Verschlüsselung gestartet (Logik wird noch verknüpft)");
    }

    // --- TAB 3: FRAGEBOGEN ---
    
    private void BtnStartQuestionnaire_Click(object sender, RoutedEventArgs e)
    {
        // Da der Fragebogen wahrscheinlich Console.WriteLine benutzt, 
        // müssen wir den Fragebogen für WPF umschreiben oder in einem separaten Fenster öffnen.
        MessageBox.Show("Der Fragebogen muss für WPF angepasst werden.");
    }
}