using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class Request : Form
    {
        public Request()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

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
                            textBox3.Text += info[1] + Environment.NewLine;
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
                            textBox2.Text += info[0] + Environment.NewLine;
                            textBox4.Text += info[1] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();

            string[] info = new string[3];
            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                // получаем данные из таблицы с материалами
                using (NpgsqlCommand command = new NpgsqlCommand("select * from get_all_miu();", bdConnection))
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
                            textBox11.Text += info[0] + Environment.NewLine;
                            textBox12.Text += info[1] + Environment.NewLine;
                            textBox15.Text += info[2] + Environment.NewLine;
                        }
                    }
                }
                // получаем данные из таблицы с инструментами
                using (NpgsqlCommand command = new NpgsqlCommand("select * from get_all_iiu();", bdConnection))
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
                            textBox13.Text += info[0] + Environment.NewLine;
                            textBox14.Text += info[1] + Environment.NewLine;
                            textBox16.Text += info[2] + Environment.NewLine;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string work_ = textBox8.Text;
            string inst_ = textBox9.Text;
            string kol_ = textBox10.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand comand = new NpgsqlCommand("call new_iiu(@n, @ik, @wi);", bdConnection);
                comand.Parameters.Add("@n", NpgsqlTypes.NpgsqlDbType.Varchar).Value = inst_;
                comand.Parameters.Add("@ik", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(kol_);
                comand.Parameters.Add("@wi", NpgsqlTypes.NpgsqlDbType.Varchar).Value = work_;
                bdConnection.Open();
                comand.ExecuteNonQuery();
                bdConnection.Close();
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string work_ = textBox5.Text;
            string inst_ = textBox6.Text;
            string kol_ = textBox7.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand comand = new NpgsqlCommand("call new_miu(@mn, @mk, @wi);", bdConnection);
                comand.Parameters.Add("@mn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = inst_;
                comand.Parameters.Add("@mk", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(kol_);
                comand.Parameters.Add("@wi", NpgsqlTypes.NpgsqlDbType.Varchar).Value = work_;
                bdConnection.Open();
                comand.ExecuteNonQuery();
                bdConnection.Close();
            }
        }
    }
}
