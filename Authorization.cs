using System;
using System.Windows.Forms;
using Npgsql;

namespace TR
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // кнопка выхода
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // кнопка "Войти"
        {
            string login_ = textBox1.Text;
            string pass_ = textBox2.Text;
            string role = "";

            NpgsqlConnection bdConnection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=db432goGA;Database=postgres");

            NpgsqlCommand comand = new NpgsqlCommand("select get_typename(@log, @pass);", bdConnection);
            comand.Parameters.Add("@log", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login_;
            comand.Parameters.Add("@pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_;
            bdConnection.Open();
            var reader = comand.ExecuteReader();
            reader.Read();
            role = reader.GetValue(0).ToString();
            bdConnection.Close();

            switch (role)
            {
                case "директор":
                    {
                        director_form df1 = new director_form();
                        df1.Show();
                        break;
                    }
                case "прораб":
                    {
                        prorab_form pf1 = new prorab_form();
                        pf1.Show();
                        break;
                    }
                case "тех. отдел":
                    {
                        tech_otd_form tof1 = new tech_otd_form();
                        tof1.Show();
                        break;
                    }
                case "рабочий":
                    {
                        raboch_form rf1 = new raboch_form();
                        rf1.Show();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Неверный логин или пароль! \nПопробуйте ещё раз");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        break;
                    }
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
