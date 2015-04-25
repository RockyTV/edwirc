using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using sharpclient.Extensions;

using ChatSharp;

namespace sharpclient
{
    public partial class MainForm : Form
    {
        private IrcClient ircClient = null;
        private IrcUser ircUser = null;

        public MainForm()
        {
            InitializeComponent();

            tabControl.TabPages.Add("status", "status");

            ircUser = new IrcUser("nick", "user", "pass", "realname");
            ircClient = new IrcClient("irc.esper.net", ircUser);

            ircClient.RawMessageRecieved += ircClient_RawMessageRecieved;
            ircClient.UserJoinedChannel += ircClient_UserJoinedChannel;
            ircClient.ChannelMessageRecieved += ircClient_ChannelMessageRecieved;
        }

        void ircClient_RawMessageRecieved(object sender, ChatSharp.Events.RawMessageEventArgs e)
        {
            TabPage tabPage = tabControl.TabPages["status"];
            RichTextBox messageBox = tabPage.Controls["messageBox" + tabControl.TabPages.IndexOf(tabPage)] as RichTextBox;

            Console.WriteLine(e.Message);

            string message = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), e.Message.Substring(e.Message.NthIndexOf(":", 2) + 1));
            if (!e.Message.Contains("PONG")) messageBox.SafeAppendText(message);
        }

        private void tabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(TabPage))
            {
                TabPage tabPage = e.Control as TabPage;

                int tabIndex = tabControl.TabPages.IndexOf(tabPage);

                RichTextBox messageBox = new RichTextBox();
                messageBox.Name = string.Format("messageBox{0}", tabIndex);
                messageBox.Dock = DockStyle.Fill;
                messageBox.ReadOnly = true;
                messageBox.Multiline = true;
                messageBox.ScrollBars = RichTextBoxScrollBars.Both;
                messageBox.Parent = tabPage;
                messageBox.BorderStyle = BorderStyle.FixedSingle;
                messageBox.ScrollToCaret();

                RichTextBox inputBox = new RichTextBox();
                inputBox.Name = string.Format("inputBox{0}", tabIndex);
                inputBox.Dock = DockStyle.Bottom;
                inputBox.Parent = tabPage;
                inputBox.KeyPress += inputBox_KeyPress;
                inputBox.BorderStyle = BorderStyle.FixedSingle;
                inputBox.Multiline = false;
                inputBox.Size = new Size(0, 20);

                Button sendButton = new Button();
                sendButton.Name = string.Format("sendButton{0}", tabIndex);
                sendButton.Visible = false;
                sendButton.Size = new Size(0, 0);
                sendButton.Parent = tabPage;

                if (tabPage.Name != "status" && tabPage.Name.StartsWith("#"))
                {
                    SplitContainer usersPanel = new SplitContainer();
                    usersPanel.Name = string.Format("usersPanel{0}", tabIndex);
                    usersPanel.Size = new Size(150, 0);
                    usersPanel.Dock = DockStyle.Right;
                    usersPanel.Parent = tabPage;
                    usersPanel.SplitterWidth = 1;
                    usersPanel.Panel1MinSize = 20;
                    usersPanel.Orientation = Orientation.Horizontal;
                    usersPanel.FixedPanel = FixedPanel.Panel1;
                    usersPanel.SplitterDistance = 20;
                    usersPanel.IsSplitterFixed = true;

                    TextBox userInfoBox = new TextBox();
                    userInfoBox.Name = string.Format("userInfoBox{0}", tabIndex);
                    userInfoBox.Dock = DockStyle.Top;
                    userInfoBox.ReadOnly = true;
                    userInfoBox.TextAlign = HorizontalAlignment.Center;
                    userInfoBox.Parent = usersPanel.Panel1;

                    ListBox userList = new ListBox();
                    userList.Name = string.Format("userList{0}", tabIndex);
                    userList.Dock = DockStyle.Fill;
                    userList.Parent = usersPanel.Panel2;
                    userList.Sorted = true;
                }

                tabControl.SelectTab(tabPage);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tabPage = tabControl.SelectedTab;

            RichTextBox inputBox = tabPage.Controls["inputBox" + tabControl.TabPages.IndexOf(tabPage)] as RichTextBox;
            inputBox.Focus();
        }

        void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
            {
                TabPage selectedTab = tabControl.SelectedTab;
                RichTextBox messageBox = selectedTab.Controls["messageBox" + tabControl.TabPages.IndexOf(selectedTab)] as RichTextBox;
                RichTextBox inputBox = selectedTab.Controls["inputBox" + tabControl.TabPages.IndexOf(selectedTab)] as RichTextBox;
                if (selectedTab.Name == "status")
                {
                    if (inputBox.Text.StartsWith(@"/")) ircClient.SendRawMessage(inputBox.Text.Substring(1));
                    else ircClient.SendRawMessage(inputBox.Text);
                }
                else
                {
                    string sentMessage = string.Format("[{0}] <{1}> {2}", DateTime.Now.ToString("HH:mm:ss"), ircUser.Nick, inputBox.Text);
                    ircClient.Channels[selectedTab.Name].SendMessage(inputBox.Text);
                    messageBox.SafeAppendText(sentMessage);
                }

                inputBox.ResetText();

                e.Handled = true;
            }
        }

        void ircClient_UserJoinedChannel(object sender, ChatSharp.Events.ChannelUserEventArgs e)
        {
            if (e.User.Nick == ircUser.Nick)
            {
                if (!tabControl.TabPages.ContainsKey(e.Channel.Name)) tabControl.AddPage(e.Channel.Name, e.Channel.Name);
            }
        }

        void ircClient_ChannelMessageRecieved(object sender, ChatSharp.Events.PrivateMessageEventArgs e)
        {
            if (tabControl.TabPages.ContainsKey(e.PrivateMessage.Source))
            {
                TabPage tabPage = tabControl.TabPages[e.PrivateMessage.Source];

                RichTextBox messageBox = tabPage.Controls["messageBox" + tabControl.TabPages.IndexOf(tabPage)] as RichTextBox;

                string message = string.Format("[{0}] <{1}> {2}", DateTime.Now.ToString("HH:mm:ss"), e.PrivateMessage.User.Nick, e.PrivateMessage.Message);
                messageBox.SafeAppendText(message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ircClient.ConnectAsync();
        }

        private void channelTimer_Tick(object sender, EventArgs e)
        {
            foreach (IrcChannel channel in ircClient.Channels)
            {
                TabPage tabPage = tabControl.TabPages[channel.Name];
                int tabIndex = tabControl.TabPages.IndexOf(tabPage);

                SplitContainer usersPanel = tabPage.Controls["usersPanel" + tabIndex] as SplitContainer;
                TextBox userInfoBox = usersPanel.Panel1.Controls["userInfoBox" + tabIndex] as TextBox;
                ListBox userList = usersPanel.Panel2.Controls["userList" + tabIndex] as ListBox;

                userInfoBox.Text = string.Format("{0} users", channel.Users.Count());
                foreach (IrcUser user in channel.Users)
                {
                    if (!userList.Items.Contains(user.Mode + user.Nick)) userList.Items.Add(user.Mode + user.Nick);
                }
            }
        }
    }
}
