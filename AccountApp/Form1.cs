using AccountApp.Views;

namespace AccountApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                if (textBox1.Text == "admin" && textBox2.Text == "123")
                {
                    MainMenu mm = new MainMenu();
                    mm.Show();
                    this.Hide();
                }
                else
                {
                    using (var db = new DataContext())
                    {
                        var users = db.Users.Where(u => u.UserName == textBox1.Text && u.Password == textBox2.Text).FirstOrDefault();
                        if (users != null)
                        {
                            MainMenu mm = new MainMenu();
                            mm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username/Password is incorrect","Invalid Credentials");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter username and password", "Error");
            }
        }
    }
}