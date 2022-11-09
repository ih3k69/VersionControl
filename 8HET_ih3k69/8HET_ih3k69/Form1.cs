﻿using _8HET_ih3k69.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8HET_ih3k69
{
    
    public partial class Form1 : Form
    {
        List<Tick> Ticks;
        PortfolioEntities context=new PortfolioEntities();
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            CreatePortfolio();
        }
        void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });
            dataGridView2.DataSource = Portfolio;
        }
        private decimal PortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last =( from x in Ticks
                               where x.Index == item.Index.Trim() && date <= x.TradingDay
                               select x).First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
