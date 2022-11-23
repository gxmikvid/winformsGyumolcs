using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsGyumolcs
    {
    public partial class Form1 : Form
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=gyumolcsok;port=3306;password=");
        #region db_stuff
        public void DBHandler(MySqlConnection connection, string command, Action<MySqlCommand> Method)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(command, connection);
                Method(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        public void UpdateList(MySqlCommand cmd)
        {
            MySqlDataReader dr = cmd.ExecuteReader();
            gyumolcsList.Items.Clear();
            while (dr.Read())
            {
                Gyumolcs gyumolcs = new Gyumolcs(dr.GetInt32("id"), dr.GetString("nev"), dr.GetDouble("egysegar"), dr.GetDouble("mennyiseg"));
                gyumolcsList.Items.Add(gyumolcs);
            }
        }

        public void SQLUpload(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@nev", nameCont.Text);
            cmd.Parameters.AddWithValue("@egysegar", egysegarCont.Text);
            cmd.Parameters.AddWithValue("@mennyiseg", mennyisegCont.Text);
            cmd.ExecuteNonQuery();
        }

        public void SQLDelete(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@id", idCont.Text);
            cmd.ExecuteNonQuery();
        }

        public void Fill(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@nev", gyumolcsList.Text);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            idCont.Value = dr.GetInt32("id");
            nameCont.Text = dr.GetString("nev");
            egysegarCont.Text = dr.GetString("egysegar");
            mennyisegCont.Text = dr.GetString("mennyiseg");
        }

        public void SQLModify(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@nev", nameCont.Text);
            cmd.Parameters.AddWithValue("@egysegar", egysegarCont.Text);
            cmd.Parameters.AddWithValue("@mennyiseg", mennyisegCont.Text);
            cmd.Parameters.AddWithValue("@id", idCont.Value);
            cmd.ExecuteNonQuery();
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DBHandler(conn, "SELECT * FROM gyumolcsok", UpdateList);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "INSERT INTO gyumolcsok (id, nev, egysegar, mennyiseg) VALUES (NULL, @nev, @egysegar, @mennyiseg)", SQLUpload);
            DBHandler(conn, "SELECT * FROM gyumolcsok", UpdateList);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "DELETE FROM gyumolcsok WHERE id = @id", SQLDelete);
            DBHandler(conn, "SELECT * FROM gyumolcsok", UpdateList);
        }

        private void gyumolcsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBHandler(conn, "SELECT * FROM gyumolcsok WHERE nev = @nev", Fill);
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            DBHandler(conn, "UPDATE gyumolcsok SET nev = @nev, egysegar = @egysegar, mennyiseg = @mennyiseg WHERE gyumolcsok.id = @id", SQLModify);
            DBHandler(conn, "SELECT * FROM gyumolcsok", UpdateList);
        }
    }
}
