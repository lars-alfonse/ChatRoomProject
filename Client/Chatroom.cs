﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Chatroom : Form
    {
        Client client;
        
        public Chatroom(Client client)
        {
            
            this.client = client;
            InitializeComponent();
            Thread Reciever = new Thread(new ThreadStart(CheckMessages));
            Reciever.Start();
        }

        private void SendMessage(object sender, EventArgs e)
        {
            client.Send(Input.Text);
            Input.Text = "";
        }

        public void CheckMessages()
        {
            while (true)
            {
                client.Recieve();
            }
        }
        public void DisplayMessages(string message)
        {
            DisplayBox.AppendText(message);
        }

        public void DisplayActiveUsers()
        {
            Dictionary<string, Client> activeUsers = new Dictionary<string, Client>();
            activeUsersDisplay.DataSource = new BindingSource(activeUsers, null);
            //activeUsersDisplay.DisplayMember = client.Username;
            foreach (var user in activeUsers)
            {
                activeUsersDisplay.ValueMember = "";
            }
            
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Input.SelectAll();
            }
        }
    }
}
