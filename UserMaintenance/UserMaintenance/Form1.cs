﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;
using UserMaintenance.Properties;


namespace UserMaintenance
{

    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();

            lblLastName.Text = Resource1.FullName;
            lblFistName.Text = Resource1.FirstName;
            btnAdd.Text = Resource1.Add;
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
            btnAdd.Click += BtnAdd_Click;


        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var u = new User();
            u.FullName = txtLastName.Text + txtFirstName.Text;
            users.Add(u);
        }
    }
}
