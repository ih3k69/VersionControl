using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
           
            btnAdd.Text = Resource1.Add;
            btnfajl.Text = Resource1.WriteFile;
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
            btnAdd.Click += BtnAdd_Click;
            btnfajl.Click += Btnfajl_Click;


        }

        private void Btnfajl_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (var item in users)
                    {
                        sw.WriteLine($"{item.ID} {item.FullName}");
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var u = new User();
            u.FullName = txtLastName.Text;
            users.Add(u);
        }
    }
}
