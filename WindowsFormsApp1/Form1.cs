using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Progetto
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Chiamata alla funzione per caricare i dati nella DataGridView
            CaricaDati();
        }

        private void CaricaDati()
        {
            String ConnectionString = "server=127.0.0.1;uid=programma;pwd=1234;database=eventi";
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();


            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Query corretta
                string query = "SELECT eventi.Titolo, eventi.Descrizione, eventi.Data_Inizio, eventi.Data_Fine," +
                               "luoghi.Nome AS NomeLuogo, luoghi.Indirizzo, luoghi.Città, luoghi.Paese " +
                               "FROM eventi " +
                               "INNER JOIN luoghi ON eventi.Luogo_ID = luoghi.ID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }

                dataGridView1.DataSource = dataTable; // Inserisco i dati nella DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento del database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CaricaDati();

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            CaricaDati();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ConnectionString = "server=127.0.0.1;uid=programma;pwd=1234;database=eventi";
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                // Query con filtro per luogo e data di inizio
                string query = "SELECT eventi.ID, eventi.Titolo, eventi.Descrizione, eventi.Data_Inizio, eventi.Data_Fine, eventi.Luogo_ID, " +
                               "luoghi.Nome AS NomeLuogo, luoghi.Indirizzo, luoghi.Città, luoghi.Paese " +
                               "FROM eventi " +
                               "INNER JOIN luoghi ON eventi.Luogo_ID = luoghi.ID " +
                               "WHERE luoghi.Città = @citta AND eventi.Data_Inizio >= @dataInizio";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@citta", textBox2.Text); // Assicurati che txtCitta sia il tuo controllo TextBox per la città
                cmd.Parameters.AddWithValue("@dataInizio", dateTimePicker1.Value); // Assicurati che dateTimePickerDataInizio sia il tuo controllo DateTimePicker per la data di inizio

                DataTable dataTable = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dataTable; // Inserisco i dati filtrati nella DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il filtro del database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
}
