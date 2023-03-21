using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class raboch_form : Form
    {
        public raboch_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fio = textBox2.Text;
            string[] info;
            NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres");

            NpgsqlCommand command = new NpgsqlCommand("select * from select_workers_data(@wname);", bdConnection);
            command.Parameters.Add("@wname", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fio;
            bdConnection.Open();

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                int FieldsCount = reader.FieldCount;
                info = new string[FieldsCount];
                for (int i = 0; i < FieldsCount; i++)
                {
                    info[i] += reader.GetValue(i).ToString();
                }  
                bdConnection.Close();
                textBox1.Text = info[0];
                textBox3.Text = info[2];
                textBox4.Text = info[3];
                textBox5.Text = info[4];
            }
        }
    }
}