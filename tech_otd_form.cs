using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class tech_otd_form : Form
    {
        public tech_otd_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            string[] info = new string[2];
            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                // получаем данные из таблицы с материалами
                using (NpgsqlCommand command = new NpgsqlCommand("select * from get_stor_mat_info();", bdConnection))
                { 
                    bdConnection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int FieldsCount = reader.FieldCount;
                            for (int i = 0; i < FieldsCount; i++)
                            {
                                info[i] = reader.GetValue(i).ToString();
                            }
                            textBox1.Text += info[0] + Environment.NewLine;
                            textBox2.Text += info[1] + Environment.NewLine;
                        }
                    }
                }
                // получаем данные из таблицы с инструментами
                using (NpgsqlCommand command = new NpgsqlCommand("select * from get_stor_inst_info();", bdConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int FieldsCount = reader.FieldCount;
                            for (int i = 0; i < FieldsCount; i++)
                            {
                                info[i] = reader.GetValue(i).ToString();
                            }
                            textBox1.Text += info[0] + Environment.NewLine;
                            textBox2.Text += info[1] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] info = new string[6];
            info[0] = textBox3.Text;
            info[1] = textBox4.Text;
            info[2] = textBox5.Text;
            info[3] = textBox6.Text;
            info[4] = textBox7.Text;
            info[5] = textBox8.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand command = new NpgsqlCommand("call new_kpp(@w, @d, @mi, @km, @ii, @ki);", bdConnection);
                command.Parameters.Add("@w", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[0]);
                command.Parameters.Add("@d", NpgsqlTypes.NpgsqlDbType.Date).Value = DateTime.Parse(info[1]);
                command.Parameters.Add("@mi", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[2]);
                command.Parameters.Add("@km", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[3]);
                command.Parameters.Add("@ii", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[4]);
                command.Parameters.Add("@ki", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[5]);
                bdConnection.Open();
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] info = new string[2];
            info[0] = textBox9.Text;
            info[1] = textBox10.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                bdConnection.Open();
                NpgsqlCommand command = new NpgsqlCommand("call add_new_to_storage_mat(@mn, @mp);", bdConnection);
                command.Parameters.Add("@mn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[0];
                command.Parameters.Add("@mp", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt64(info[1]);
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] info = new string[2];
            info[0] = textBox11.Text;
            info[1] = textBox12.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                bdConnection.Open();
                NpgsqlCommand command = new NpgsqlCommand("call add_new_to_storage_inst(@in, @ip);", bdConnection);
                command.Parameters.Add("in", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[0];
                command.Parameters.Add("@ip", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt64(info[1]);
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }
    }
}
