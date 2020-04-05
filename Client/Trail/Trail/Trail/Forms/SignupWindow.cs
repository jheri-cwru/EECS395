using System;
using System.Windows.Forms;
using Trail.Helpers;

namespace Trail.Forms
{
    public partial class SignupWindow : Form
    {
        public SignupWindow()
        {
            InitializeComponent();
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            UserAuthentication authenticationFramework = new UserAuthentication();
            authenticationFramework.createUser(usernameEntry.Text, passwordEntry.Text);
            this.Hide();
        }
    }
}
