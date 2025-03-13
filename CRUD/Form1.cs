using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=dbpeople;sslMode=none");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;
        string sql;
        string  peopleid;
        int result;
         

        private void loadData()
        {
            try
            {
                sql = "SELECT PersonID, `FNAME` as 'Firstname', `LNAME` as 'Lastname', `ADDRESS` as 'Address' FROM `tblperson`";
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt); 
                dtg_list.DataSource = dt;


                txt_address.Clear();
                txt_fname.Clear();
                txt_lname.Clear();

                btn_delete.Enabled = false;
                btn_update.Enabled = false;
                btn_save.Enabled = true;

          

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                da.Dispose();
            }

        }
        private void saveData()
        {
            try
            {
                sql = "INSERT INTO `tblperson` (`FNAME`, `LNAME`, `ADDRESS`) VALUES ('" + txt_fname.Text  + "','" + txt_lname .Text + "','" + txt_address .Text + "')";
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data has been saved in the database.", "Save");
                }
                else
                {
                    MessageBox.Show("Failed to execute the query" , "error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close(); 
            }

        }
        private void updateData()
        {
            try
            {
                sql = "UPDATE `tblperson` SET `FNAME`='" + txt_fname.Text + "', `LNAME`='" + txt_lname.Text + "', `ADDRESS` ='" + txt_address.Text + "'  WHERE PersonID=" + peopleid;
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result=cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Data has been updated in the database.", "Update");
                }
                else
                {
                    MessageBox.Show("Failed to execute the query", "error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        private void deleteData()
        {
            try
            {
                sql = "DELETE FROM  `tblperson` WHERE PersonID=" + peopleid;
                con.Open();
                cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                result=cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Data has been deleted in the database.", "Delete");
                }
                else
                {
                    MessageBox.Show("Failed to execute the query", "error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            saveData();
            loadData();


        }

        private void btn_retrieve_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            updateData();
            loadData();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteData();
            loadData();
        }

        private void dtg_list_Click(object sender, EventArgs e)
        {

            peopleid = dtg_list.CurrentRow.Cells[0].Value.ToString();
            txt_fname.Text  = dtg_list.CurrentRow.Cells[1].Value.ToString();
            txt_lname .Text = dtg_list.CurrentRow.Cells[2].Value.ToString();
            txt_address .Text = dtg_list.CurrentRow.Cells[3].Value.ToString();

            btn_delete.Enabled = true;
            btn_update.Enabled = true;
            btn_save.Enabled = false;

        }
    }
}
