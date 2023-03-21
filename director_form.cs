using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class director_form : Form
    {
        public director_form()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string[] info = new string[3];
            info[0] = textBox2.Text;
            info[1] = textBox3.Text;
            info[2] = textBox4.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                bdConnection.Open();
                NpgsqlCommand command = new NpgsqlCommand("call add_to_building(@wn, @bd, @ed);", bdConnection);
                command.Parameters.Add("@wn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = info[0];
                command.Parameters.Add("@bd", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(info[1]);
                command.Parameters.Add("@ed", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(info[2]);
                command.ExecuteNonQuery();
                bdConnection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                bdConnection.Open();
                NpgsqlCommand command = new NpgsqlCommand("select all_mat_price();", bdConnection);
                var reader = command.ExecuteScalar();
                textBox1.Text = reader.ToString();

                command = new NpgsqlCommand("select all_inst_price();", bdConnection);
                reader = command.ExecuteScalar();
                textBox5.Text = reader.ToString();

                bdConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prorab_form pf1 = new prorab_form();
            pf1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tech_otd_form tof1 = new tech_otd_form();
            tof1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Request req = new Request();
            req.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string type_ = textBox6.Text;
            string log_ = textBox7.Text;
            string pass_ = textBox8.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand comand = new NpgsqlCommand("call add_new_user(@tn, @l, @p);", bdConnection);
                comand.Parameters.Add("@tn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = type_;
                comand.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Varchar).Value = log_;
                comand.Parameters.Add("@p", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_;
                bdConnection.Open();
                comand.ExecuteNonQuery();
                bdConnection.Close();
                MessageBox.Show("Пользователь успешно добавлен");
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string log_ = textBox9.Text;

            using (NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres"))
            {
                NpgsqlCommand comand = new NpgsqlCommand("call delete_user(@l);", bdConnection);
                comand.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Varchar).Value = log_;
                bdConnection.Open();
                comand.ExecuteNonQuery();
                bdConnection.Close();
                MessageBox.Show("Пользователь успешно удалён");
                textBox9.Text = "";
            }
        }
    }
    
}

