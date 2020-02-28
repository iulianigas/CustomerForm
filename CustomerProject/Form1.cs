using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CustomerProject
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=IULIAN.\SQLEXPRESS;Initial Catalog=TabelaClienti;Integrated Security=True");


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)   //reset
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)    //adaugare
        {
            bool succesfull = true;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into dbo.Clienti(CustomerID, FirstName, LastName, Adress, City, Country) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Invalid insert");
                succesfull = false;
                MessageBox.Show(ex.Message);

            }
            con.Close();
            display_data();

            if (succesfull)
                MessageBox.Show("Client inserted succesfully");
        }

        public void display_data()        //afisare date din tabela
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Clienti";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)   //afisare tabela cu datele introduse initial
        {
            display_data();
        }

        private void button4_Click(object sender, EventArgs e)     //stergere
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Clienti where CustomerID = '" + textBox1.Text + "' or FirstName = '" + textBox2.Text + "' or LastName = '" +
                textBox3.Text + "' or Adress = '" + textBox4.Text + "' or City = '" + textBox5.Text + "' or Country = '" + textBox6.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display_data();
            MessageBox.Show("Client deleted succesfully");
        }

        private void button5_Click(object sender, EventArgs e)     //cautare
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Clienti where CustomerID = '" + textBox1.Text + "' or FirstName = '" + textBox2.Text + "' or LastName = '" + 
                textBox3.Text + "' or Adress = '" + textBox4.Text + "' or City = '" + textBox5.Text + "' or Country = '" + textBox6.Text + "'";
            cmd.ExecuteNonQuery();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    string LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    string Adress = reader.GetString(reader.GetOrdinal("Adress"));
                    string City = reader.GetString(reader.GetOrdinal("City"));
                    string Country = reader.GetString(reader.GetOrdinal("Country"));
                    //int CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID"));
                    MessageBox.Show(FirstName);
                    this.textBox2.Text = FirstName;
                    this.textBox3.Text = LastName;
                    this.textBox4.Text = Adress;
                    this.textBox5.Text = City;
                    this.textBox6.Text = Country;
                }

            }

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)    //modificare 
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Clienti SET FirstName = @FirstName, LastName = @LastName, Adress = @Adress, City = @City, Country = @Country  WHERE CustomerID = '"
                + textBox1.Text + "'";
            cmd.Parameters.AddWithValue("@FirstName", this.textBox2.Text);
            cmd.Parameters.AddWithValue("@LastName", this.textBox3.Text);
            cmd.Parameters.AddWithValue("@Adress", this.textBox4.Text);
            cmd.Parameters.AddWithValue("@City", this.textBox5.Text);
            cmd.Parameters.AddWithValue("@Country", this.textBox6.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            display_data();
            MessageBox.Show("Client updated succesfully");
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
        }



    }
}
