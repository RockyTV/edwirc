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

using edwirc.Extensions;
using edwirc.Forms;

using ChatSharp;
using edwirc.Controls;

namespace edwirc
{
    public partial class MainForm : Form
    {
        private IrcClient ircClient = null;
        private IrcUser ircUser = null;

        public MainForm()
        {
            InitializeComponent();

            tabControl.TabPages.Add("status", "status");

            ircUser = new IrcUser(SettingsForm.fetch.userInfo.Nick, SettingsForm.fetch.userInfo.User, SettingsForm.fetch.userInfo.Password, SettingsForm.fetch.userInfo.RealName);
            ircClient = new IrcClient("irc.esper.net", ircUser);

            ircClient.RawMessageRecieved += ircClient_RawMessageRecieved;
            ircClient.UserJoinedChannel += ircClient_UserJoinedChannel;
            ircClient.ChannelMessageRecieved += ircClient_ChannelMessageRecieved;
            ircClient.ChannelTopicReceived += ircClient_ChannelTopicReceived;
        }

        void ircClient_RawMessageRecieved(object sender, ChatSharp.Events.RawMessageEventArgs e)
        {
            TabPage tabPage = tabControl.TabPages["status"];
            RichTextBox messageBox = tabPage.Controls["messageBox" + tabControl.TabPages.IndexOf(tabPage)] as RichTextBox;

            Console.WriteLine(e.Message);

            string message = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), e.Message.Substring(e.Message.NthIndexOf(":", 2) + 1));
            if (!e.Message.Contains("PONG") || !e.Message.Contains("PING")) messageBox.SafeAppendText(message);
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
                messageBox.BorderStyle = BorderStyle.None;
                messageBox.ScrollToCaret();

                Console.WriteLine(messageBox.Rtf);

                LineSeparator lineSeparator = new LineSeparator();
                lineSeparator.Name = string.Format("separatorInputBox{0}", tabIndex);
                lineSeparator.Dock = DockStyle.Bottom;
                lineSeparator.Parent = tabPage;

                TextBox inputBox = new TextBox();
                inputBox.Name = string.Format("inputBox{0}", tabIndex);
                inputBox.Dock = DockStyle.Bottom;
                inputBox.Parent = tabPage;
                inputBox.KeyPress += inputBox_KeyPress;
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
                    userList.ItemHeight = (int)this.Font.Size;
                    userList.Sorted = true;
                }

                tabControl.SelectTab(tabPage);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage tabPage = tabControl.SelectedTab;
            int tabIndex = tabControl.TabPages.IndexOf(tabPage);

            RichTextBox messageBox = tabPage.Controls["messageBox" + tabIndex] as RichTextBox;
            TextBox inputBox = tabPage.Controls["inputBox" + tabControl.TabPages.IndexOf(tabPage)] as TextBox;

            inputBox.Focus();
        }

        void inputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
            {
                TabPage selectedTab = tabControl.SelectedTab;
                RichTextBox messageBox = selectedTab.Controls["messageBox" + tabControl.TabPages.IndexOf(selectedTab)] as RichTextBox;
                TextBox inputBox = selectedTab.Controls["inputBox" + tabControl.TabPages.IndexOf(selectedTab)] as TextBox;
                if (selectedTab.Name == "status")
                {
                    if (inputBox.Text.StartsWith(@"/")) ircClient.SendRawMessage(inputBox.Text.Substring(1));
                    else ircClient.SendRawMessage(inputBox.Text);
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputBox.Text))
                    {
                        if (inputBox.Text.StartsWith(@"/")) ircClient.SendRawMessage(inputBox.Text.Substring(1));
                        else
                        {
                            string sentMessage = string.Format("[{0}] <{1}> {2}", DateTime.Now.ToString("HH:mm:ss"), ircUser.Nick, inputBox.Text);
                            ircClient.Channels[selectedTab.Name].SendMessage(inputBox.Text);
                            messageBox.SafeAppendText(sentMessage);
                        }
                    }
                }

                inputBox.ResetText();
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.ControlKey && e.KeyChar == (char)Keys.K)
            {
                TabPage selectedTab = tabControl.SelectedTab;
                RichTextBox messageBox = selectedTab.Controls["messageBox" + tabControl.TabPages.IndexOf(selectedTab)] as RichTextBox;
                TextBox inputBox = selectedTab.Controls["inputBox" + tabControl.TabPages.IndexOf(selectedTab)] as TextBox;

                char kChar = (char)3;

                inputBox.SafeAppendText(kChar.ToString());
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

                int channelOpsCount = 0;
                string infoBoxText = "{0} ops, {1} users";
                if (channel.UsersByMode.ContainsKey('o'))
                {
                    channelOpsCount = channel.UsersByMode['o'].Count();
                    if (channelOpsCount == 1) infoBoxText = "{0} op, {1} users";
                    else infoBoxText = "{0} ops, {1} users";
                }

                userInfoBox.Text = string.Format(infoBoxText, channelOpsCount, channel.Users.Count());

                List<string> usersToAdd = new List<string>();
                usersToAdd.AddRange(channel.Users.Select(u => u.Nick));
                foreach (KeyValuePair<char, UserCollection> usersByMode in channel.UsersByMode)
                {
                    if (usersByMode.Key == '@' || usersByMode.Key == 'o')
                    {
                        foreach (IrcUser op in usersByMode.Value)
                        {
                            if (!userList.Items.Contains("@" + op.Nick)) userList.Items.Add("@" + op.Nick);
                            usersToAdd.Remove(op.Nick);
                        }
                    }
                    else if (usersByMode.Key == '+' || usersByMode.Key == 'v')
                    {
                        foreach (IrcUser voice in usersByMode.Value)
                        {
                            if (!userList.Items.Contains("+" + voice.Nick)) userList.Items.Add("+" + voice.Nick);
                            usersToAdd.Remove(voice.Nick);
                        }
                    }
                }
                foreach (IrcUser user in channel.Users)
                {
                    if (!userList.Items.Contains(user.Nick) && usersToAdd.Contains(user.Nick))
                        userList.Items.Add(user.Nick);
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(this);
        }


        void ircClient_ChannelTopicReceived(object sender, ChatSharp.Events.ChannelTopicEventArgs e)
        {
            if (ircClient.Channels.Contains(e.Channel))
            {
                TabPage tabPage = tabControl.TabPages[e.Channel.Name];
                int tabIndex = tabControl.TabPages.IndexOf(tabPage);

                RichTextBox messageBox = tabPage.Controls["messageBox" + tabIndex] as RichTextBox;
                messageBox.SafeAppendText("*** Topic for " + tabPage.Name + ": " + e.Topic);
            }
        }
    }
}
