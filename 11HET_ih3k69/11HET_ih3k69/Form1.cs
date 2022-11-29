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
using _11HET_ih3k69.Entities;
using _11HET_ih3k69.Entities.Egyén;

namespace _11HET_ih3k69
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        OpenFileDialog ofd = new OpenFileDialog();
        List<int> Nők = new List<int>();
        List<int> Férfiak = new List<int>();
        Random rnd = new Random(1234);

        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Minimum = 2005;
            numericUpDown1.Maximum = 2025;
            textBox1.Text = @"D:\nép-teszt.csv";
        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
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
            List<BirthProbability> születés = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    születés.Add(new BirthProbability()
                    {
                        Year = int.Parse(sor[0]),
                        NbrOfChildren = int.Parse(sor[1]),
                        Bprobability = double.Parse(sor[2])
                    });
                }
            }
            return születés;
        }
        public List<DeathProbability> GetDeath(string csvpath)
        {
            List<DeathProbability> halálok = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    halálok.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), sor[0]),
                        Year = int.Parse(sor[1]),
                        Dprobability = double.Parse(sor[2])
                    });
                }
            }
            return halálok;
        }
        public void Simstep(int year, Person person)
        {
            if (person.IsAlive == false) return;
            byte kor = (byte)(year - person.BirthYear);
            double halálózásiesély = (from x in DeathProbabilities
                                      where x.Year == kor && x.Gender == person.Gender
                                      select x.Dprobability).FirstOrDefault();
            var random = rnd.NextDouble();
            if (random <= halálózásiesély)
            {
                person.IsAlive = false;
            }
            if (person.Gender == Gender.Female && person.IsAlive)
            {
                var vanegyerek = (from x in BirthProbabilities
                                  where x.Year == kor
                                  select x.Bprobability).FirstOrDefault();
                var randomgyerek = rnd.NextDouble();
                if (randomgyerek <= vanegyerek)
                {
                    Population.Add(new Person { BirthYear = year, Gender = (Gender)(rnd.Next(1, 3)), NbrOfChildren = 0 });
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Simulation();
            DisplayResult();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != DialogResult.OK) return;
             textBox1.Text=ofd.FileName;
           
        }
        void Simulation()
        {
            Population = GetPopulation(textBox1.Text);
            BirthProbabilities = GetBirth(@"D:\születés.csv");
            DeathProbabilities = GetDeath(@"D:\halál.csv");
            for (int i = 2005; i <= numericUpDown1.Value; i++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    //importante
                    Simstep(i, Population[j]);

                }
                int férfi = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nő = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Nők.Add(nő);
                Férfiak.Add(férfi);
            }
        }
        void DisplayResult()
        {
            for (int i = 2005; i <= numericUpDown1.Value; i++)
            {
                string sor = $"Szimulációs év: {i}\n\tFiúk:{Férfiak[i - 2005]}\n\tNők:{Nők[i - 2005]}\n\n";
                richTextBox1.AppendText(sor);
            }
        }
    }
}
