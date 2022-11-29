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
        Random rnd=new Random(1234);

        public Form1()
        {
            InitializeComponent();
            Population=GetPopulation(@"D:\nép.csv");
            BirthProbabilities = GetBirth(@"D:\születés.csv");
            DeathProbabilities = GetDeath(@"D:\halál.csv");
            for (int i = 2005; i <=2024; i++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    //importante
                    Simstep(i, Population[j]);
                   
                }
                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", i, nbrOfMales, nbrOfFemales));

            }

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
        public void Simstep(int year, Person person)
        {
            if (person.IsAlive == false) return;
            byte kor = (byte)(year - person.BirthYear);
            double halálózásiesély = (from x in DeathProbabilities
                                   where x.Year == kor && x.Gender == person.Gender
                                   select x.Dprobability).FirstOrDefault();
            var random = rnd.NextDouble();
            if (random<=halálózásiesély)
            {
                person.IsAlive = false;
            }
            if (person.Gender==Gender.Female&&person.IsAlive)
            {
                var vanegyerek = (from x in BirthProbabilities
                                  where x.Year == kor 
                                  select x.Bprobability).FirstOrDefault();
                var randomgyerek = rnd.NextDouble();
                if (randomgyerek<=vanegyerek)
                {
                    Population.Add(new Person { BirthYear = year, Gender = (Gender)(rnd.Next(1,3)), NbrOfChildren = 0 });
                }
            }
           
        }
    }
}
