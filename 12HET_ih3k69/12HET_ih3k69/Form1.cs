using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace _12HET_ih3k69
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;
        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;
        Brain b=null;
        
        public Form1()
        {
            InitializeComponent();
            ga = gc.ActivateDisplay(); //játéktér létrehozása
            this.Controls.Add(ga);
            //gc.AddPlayer();
            //gc.Start(true);
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.GameOver += Gc_GameOver;
            gc.Start();
        }

        private void Gc_GameOver(object sender)
        {
            label1.BringToFront();
            generation++;
            label1.Text = $"{generation}. generáció";
            var playerList = from p in gc.GetCurrentPlayers()
                             orderby p.GetFitness() descending
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList();
            /*foreach (var item in topPerformers)
            {
                if (item.IsWinner==true)
                {
                    b = item.Brain;
                    gc.GameOver-=Gc_GameOver;   
                    return;

                }
            }*/
            var winners = from p in topPerformers
                          where p.IsWinner
                          select p;
            if (winners.Count() > 0)
            {
                b= winners.FirstOrDefault().Brain.Clone();
                gc.GameOver -= Gc_GameOver;
                button1.Visible = true;
                return;
            }
            gc.ResetCurrentLevel();
            foreach (var p in topPerformers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }
            gc.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gc.ResetCurrentLevel();
            gc.AddPlayer(b.Clone());
            gc.AddPlayer();
            ga.Focus();
            gc.Start(true);
        }
    }
}
