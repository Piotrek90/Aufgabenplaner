using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Aufgabenplaner
{
    /// <summary>
    /// Interaktionslogik für Window_Aendern.xaml
    /// </summary>
    public partial class Window_Aendern : Window
    {
        public Window_Aendern()
        {
            InitializeComponent();
        }

        private void btnErstellen_Click(object sender, RoutedEventArgs e)
        {
            Aufgabe a = new Aufgabe();
            a.Kurzbezeichnung = txtKurzbeschreibung.Text;
            a.Aufgabenbeschreibung = txtAufgabenbeschreibung.Text;

            if (chbErledigt.IsChecked == null) 
            { 
                MessageBox.Show("Ein Fehler ist aufgetreten", "Fehler");
            }
            else
            {
                a.Erledigt = (bool)chbErledigt.IsChecked;
            }
            Database.UpdateAufgabe(a);
            Close();
        }
    }
}
