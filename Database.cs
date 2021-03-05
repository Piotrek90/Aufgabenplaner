using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aufgabenplaner
{
    class Database
    {
        private static string myConnectionString = "server=localhost;uid=root;pwd=;database=test;";
        private static MySqlConnection conn = new MySqlConnection(myConnectionString);

        public static bool CheckConnection()
        {
            bool result = false;
            try
            {
                conn.Open();
                result = conn.Ping();
                MessageBox.Show("Connection hergestellt\n");
            }
            catch (MySqlException ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            return result;
        }

        internal static List<Aufgabe> SucheAlle()
        {
            List<Aufgabe> pListe = new List<Aufgabe>();
            
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                con.Open();
                string strSQL = "SELECT * FROM aufgaben";
                MySqlCommand cmd = new MySqlCommand(strSQL, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Aufgabe a = new Aufgabe()
                        {
                            Id = reader.GetInt32("Id"),
                            Kurzbezeichnung = reader.GetString("kurzbezeichnung"),
                            Aufgabenbeschreibung = reader.GetString("aufgabenbeschreibung"),
                            Erledigt = reader.GetBoolean("erledigt")

                        };
                        pListe.Add(a);
                    }
                }
                else
                {
                    MessageBox.Show("Keine Aufgabe Mitarbeiter gefunden", "Datenbank leer");

                }
                reader.Close();
                con.Close();
            }
            return pListe;
        }

        internal static void UpdateAufgabe(Aufgabe a)
        {
            string sql = "UPDATE aufgaben SET kurzbezeichnung = @titel, aufgabenbeschreibung = @description, erledigt = @erledigt WHERE id= @id;";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@titel", a.Kurzbezeichnung);
            cmd.Parameters.AddWithValue("@description", a.Aufgabenbeschreibung);
            cmd.Parameters.AddWithValue("@erledigt", a.Erledigt);

            int affected = cmd.ExecuteNonQuery();

            MessageBox.Show("Felder geändert: " + affected, "Änderung");
        }

        internal static void AufgabeLoeschen(Aufgabe aufgabe)
        {
            string sql = "DELETE FROM aufgaben WHERE Id = @id LIMIT 1;";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", aufgabe.Id);

            int a = cmd.ExecuteNonQuery();

            MainWindow mw = new MainWindow();
            mw.UpdateListView();
            conn.Close();
        }

        internal static void Add(string kurzbezeichnung, string aufgabenbeschreibung, bool? erledigt)
        {
            string sql = "INSERT INTO aufgaben (kurzbezeichnung, aufgabenbeschreibung, erledigt) VALUES ( @title, @description, @status);";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@title", kurzbezeichnung);
            cmd.Parameters.AddWithValue("@description", aufgabenbeschreibung);
            cmd.Parameters.AddWithValue("@status", erledigt);

            int a = cmd.ExecuteNonQuery();

            MessageBox.Show( a + " Datensatz eingefügt ", "Neuer Datensatz");
            conn.Close();
            
        }

        /*
        internal static List<Aufgabe> SucheNachAbteilung(string abt)
        {
            List<Aufgabe> ergebnisListe = new List<Aufgabe>();

            string strSQL = "SELECT * FROM Aufgabeal WHERE Abt = @x";
            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
            cmd.Parameters.AddWithValue("@x", abt);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Aufgabe p = new Aufgabe()
                    {
                        Pknr = reader.GetInt32("Pknr"),
                        Vorname = reader.GetString("Vorname"),
                        Name = reader.GetString("Name"),
                        Strasse = reader.GetString("Strasse"),
                        Plz = reader.GetString("Plz"),
                        Ort = reader.GetString("Ort"),
                        Abt = reader.GetString("Abt")

                    };
                    ergebnisListe.Add(p);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Fehler", "Keine Aufgabe in der Abteilung: " + abt + " gefunden");
                ergebnisListe.Clear();
            }
            return ergebnisListe;
        }

        internal static List<Aufgabe> SucheNachOrt(string ort)
        {
            List<Aufgabe> ergebnisListe = new List<Aufgabe>();

            string strSQL = "SELECT * FROM Aufgabeal WHERE Ort = @x";
            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
            cmd.Parameters.AddWithValue("@x", ort);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Aufgabe p = new Aufgabe()
                    {
                        Pknr = reader.GetInt32("Pknr"),
                        Vorname = reader.GetString("Vorname"),
                        Name = reader.GetString("Name"),
                        Strasse = reader.GetString("Strasse"),
                        Plz = reader.GetString("Plz"),
                        Ort = reader.GetString("Ort"),
                        Abt = reader.GetString("Abt")

                    };
                    ergebnisListe.Add(p);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Fehler", "Keine Aufgabe aus dem Ort: " + ort + " gefunden");
                ergebnisListe.Clear();
            }
            return ergebnisListe;
        }

        internal static List<Aufgabe> SucheNachPlz(string plz)
        {
            List<Aufgabe> ergebnisListe = new List<Aufgabe>();

            string strSQL = "SELECT * FROM Aufgabeal WHERE Plz = @x";
            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
            cmd.Parameters.AddWithValue("@x", plz);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Aufgabe p = new Aufgabe()
                    {
                        Pknr = reader.GetInt32("Pknr"),
                        Vorname = reader.GetString("Vorname"),
                        Name = reader.GetString("Name"),
                        Strasse = reader.GetString("Strasse"),
                        Plz = reader.GetString("Plz"),
                        Ort = reader.GetString("Ort"),
                        Abt = reader.GetString("Abt")

                    };
                    ergebnisListe.Add(p);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Fehler", "Keine Aufgabe aus dem PLZ: " + plz + " gefunden");
                ergebnisListe.Clear();
            }
            return ergebnisListe;
        }

        internal static List<Aufgabe> SucheNachStrasse(string strasse)
        {
            List<Aufgabe> ergebnisListe = new List<Aufgabe>();

            string strSQL = "SELECT * FROM Aufgabeal WHERE Strasse = @x";
            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
            cmd.Parameters.AddWithValue("@x", strasse);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Aufgabe p = new Aufgabe()
                    {
                        Pknr = reader.GetInt32("Pknr"),
                        Vorname = reader.GetString("Vorname"),
                        Name = reader.GetString("Name"),
                        Strasse = reader.GetString("Strasse"),
                        Plz = reader.GetString("Plz"),
                        Ort = reader.GetString("Ort"),
                        Abt = reader.GetString("Abt")

                    };
                    ergebnisListe.Add(p);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Fehler", "Keine Aufgabe aus der " + strasse + " Strasse gefunden");
                ergebnisListe.Clear();
            }
            return ergebnisListe;
        }

        internal static List<Aufgabe> SucheNachVorname(string vorname)
        {
            List<Aufgabe> ergebnisListe = new List<Aufgabe>();

            string strSQL = "SELECT * FROM Aufgabeal WHERE Vorname = @x";
            MySqlCommand cmd = new MySqlCommand(strSQL, conn);
            cmd.Parameters.AddWithValue("@x", vorname);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Aufgabe p = new Aufgabe()
                    {
                        Pknr = reader.GetInt32("Pknr"),
                        Vorname = reader.GetString("Vorname"),
                        Name = reader.GetString("Name"),
                        Strasse = reader.GetString("Strasse"),
                        Plz = reader.GetString("Plz"),
                        Ort = reader.GetString("Ort"),
                        Abt = reader.GetString("Abt")

                    };
                    ergebnisListe.Add(p);
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Fehler", "Keine Aufgabe mit dem Vornamen: " + vorname + " gefunden");
                ergebnisListe.Clear();
            }
            return ergebnisListe;
        }*/
    }
}
