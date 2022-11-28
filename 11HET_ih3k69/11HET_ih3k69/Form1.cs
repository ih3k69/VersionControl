﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _11HET_ih3k69.Entities;
using _11HET_ih3k69.Entities.Egyén;

namespace _11HET_ih3k69
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
            Population=GetPopulation(@"D:\nép.csv");
            BirthProbabilities = GetBirth(@"D:\születés.csv");
            DeathProbabilities = GetDeath(@"D:\halál.csv");
        }
        public List<Person> GetPopulation (string csvpath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr=new StreamReader(csvpath,Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(sor[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), sor[1]),
                        NbrOfChildren = int.Parse(sor[2])
                    });
                }
            }
            return population;
        }
        public List<BirthProbability> GetBirth(string csvpath)
        {
            List<BirthProbability> születés=new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    születés.Add(new BirthProbability()
                    {
                        Year = int.Parse(sor[0]),
                        NbrOfChildren = int.Parse(sor[1]),
                        Bprobability=double.Parse(sor[2])
                    });
                }
            }
            return születés;
        }
        public List<DeathProbability> GetDeath(string csvpath)
        {
            List<DeathProbability> halálok=new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    halálok.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender),sor[0]),
                        Year = int.Parse(sor[1]),
                        Dprobability = double.Parse(sor[2])
                    });
                }
            }
            return halálok;
        }
    }
}
