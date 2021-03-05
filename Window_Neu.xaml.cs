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
    /// Interaktionslogik für Window_Neu.xaml
    /// </summary>
    public partial class Window_Neu : Window
    {
        public Window_Neu()
        {
            InitializeComponent();
        }

        private void btnErstellen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            string kurzbeschreibung = txtKurzbeschreibung.Text;
            string aufgabenbeschreibung = txtAufgabenbeschreibung.Text;
            bool? erledigt = chbErledigt.IsChecked;

            if (kurzbeschreibung.Length > 30) MessageBox.Show("Kurzbeschreibung darf maximal 30 Zeichen lang sein");
            if (aufgabenbeschreibung.Length > 100) MessageBox.Show("Aufgabenbeschreibung darf maximal 100 Zeichen lang sein");

            Database.Add(kurzbeschreibung, aufgabenbeschreibung, erledigt);
            mw.UpdateListView();
            this.Close();
        }
    }
}
