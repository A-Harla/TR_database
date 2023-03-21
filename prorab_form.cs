using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class prorab_form : Form
    {
        public prorab_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            string[] info = new string[5];
            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                // получаем данные из таблицы с материалами
                using (NpgsqlCommand command = new NpgsqlCommand("select * from get_all_works();", bdConnection))
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
                            textBox3.Text += info[2] + Environment.NewLine;
                            textBox4.Text += info[3] + Environment.NewLine;
                            textBox5.Text += info[4] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox9.Clear();
            string[] info = new string[2];
            info[0] = textBox7.Text;
            info[1] = textBox8.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                using (NpgsqlCommand command = new NpgsqlCommand("select * from all_free_workers(@bd, @ed);", bdConnection))
                {
                    command.Parameters.Add("@bd", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(info[0]);
                    command.Parameters.Add("@ed", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(info[1]);
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
                            textBox6.Text += info[0] + Environment.NewLine;
                            textBox9.Text += info[1] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] info = new string[5];
            info[0] = textBox10.Text;
            info[1] = textBox11.Text;
            info[2] = textBox12.Text;
            info[3] = textBox13.Text;
            info[4] = textBox14.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand command = new NpgsqlCommand("call add_new_worker(@f, @p, @s, @c, @w);", bdConnection);
                command.Parameters.Add("@f", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[0]; // fio
                command.Parameters.Add("@p", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[1]; // profession
                command.Parameters.Add("@s", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[2]); // salary
                command.Parameters.Add("@c", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[3]; // company
                command.Parameters.Add("@w", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(info[4]); // work_id
                bdConnection.Open();
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] info = new string[2];
            info[0] = textBox15.Text;
            info[1] = textBox16.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand command = new NpgsqlCommand("call give_work_to_worker(@f, @w);", bdConnection);
                command.Parameters.Add("@f", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[0]; // fio
                command.Parameters.Add("@w", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[1]; // work
                bdConnection.Open();
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Request req = new Request();
            req.Show();
        }
    }
}
