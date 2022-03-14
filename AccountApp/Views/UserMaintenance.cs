using AccountApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountApp.Views
{
    public partial class UserMaintenance : Form
    {
        public UserMaintenance()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var users = db.Users.Where(u => u.UserName == usernameTxt.Text).FirstOrDefault();
                if (users != null)
                {
                    firstNameTxt.Text = users.FirstName;
                    lastnameTxt.Text = users.LastName;
                    phoneTxt.Text = users.PhoneNumber;
                    passwordTxt.Text = users.Password;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var users = new User();
                users.UserName = usernameTxt.Text;
                users.FirstName = firstNameTxt.Text;
                users.LastName=lastnameTxt.Text;
                users.PhoneNumber = phoneTxt.Text;
                users.Password = passwordTxt.Text;
                db.Users.Add(users);
                db.SaveChanges();
                MessageBox.Show("User created", "Success");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
                if (control is TextBox)
                {
                    TextBox? textBox = (control as TextBox);
                    textBox!.Clear();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                var users = db.Users.Where(u => u.UserName == usernameTxt.Text).FirstOrDefault();
                if (users != null)
                {
                    db.Users.Remove(users);
                    db.SaveChanges();

                    foreach (Control control in this.Controls)
                        if (control is TextBox)
                        {
                            TextBox? textBox = (control as TextBox);
                            textBox!.Clear();
                        }
                    MessageBox.Show("User Deleted", "Success");
                }
            }
        }
    }
}
