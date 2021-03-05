using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aufgabenplaner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Database.CheckConnection();
            UpdateListView();
        }

        public Aufgabe selectedAufgabe { get; set; }

        public void UpdateListView()
        {

            lstView.ItemsSource = Database.SucheAlle();
        }

        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            Window_Neu neu = new Window_Neu();
            neu.Show();
            neu.Closing += Neu_Closing; 
        }

        private void Neu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateListView();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateListView();
        }

        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAufgabe != null)
            {
                Database.AufgabeLoeschen(selectedAufgabe);
                UpdateListView();
            }
            else
            {
                MessageBox.Show("Keine Aufgabe Ausgewählt", "Aufgabe auswählen");
            }
        }

        private void lstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAufgabe = ((sender as ListView).SelectedItem) as Aufgabe;
            Debug.WriteLine("Ausgewählte aufgabe: " + selectedAufgabe?.Kurzbezeichnung);
        }

        private void btnAendern_Click(object sender, RoutedEventArgs e)
        {
            selectedAufgabe = ((sender as ListView).SelectedItem) as Aufgabe;
            if (selectedAufgabe != null)
            {
                Window_Aendern wa = new Window_Aendern();
                wa.txtKurzbeschreibung.Text = selectedAufgabe.Kurzbezeichnung;
                wa.txtAufgabenbeschreibung.Text = selectedAufgabe.Aufgabenbeschreibung;
                wa.chbErledigt.IsChecked = selectedAufgabe.Erledigt;
                wa.Show();
                wa.Closing += Wa_Closing;
            }
        }

        private void Wa_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateListView();
        }
    }
}
