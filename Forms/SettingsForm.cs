using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edwirc.Forms
{
    public partial class SettingsForm : Form
    {
        private static SettingsForm singleton = new SettingsForm();
        public static SettingsForm fetch
        {
            get { return singleton; }
        }

        public UserInfo userInfo = new UserInfo();

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.nickBox.Text = userInfo.Nick;
            this.userBox.Text = userInfo.User;
            this.passwordBox.Text = userInfo.Password;
            this.realNameBox.Text = userInfo.RealName;
        }
    }
}
