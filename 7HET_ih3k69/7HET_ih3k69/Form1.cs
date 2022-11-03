using _7HET_ih3k69.Entities;
using _7HET_ih3k69.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _7HET_ih3k69
{
    public partial class Form1 : Form
    {
        private Toy _nextToy;
        List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }

        public Form1()
        {
            InitializeComponent();
            Factory=new CarFactory();
            createTimer.Tick += CreateTimer_Tick;
            conveyorTimer.Tick += ConveyorTimer_Tick;

        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            int jobb=0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (jobb<item.Left)
                {
                    jobb = item.Left;
                }
            }
            if (jobb>1000)
            {
                var régibb = _toys[0];
                mainPanel.Controls.Remove(régibb);
                _toys.Remove(régibb);
            }
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
           var toy= Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = button3.BackColor
            };
            
        }

        void DisplayNext()
        {
            if (_nextToy != null) Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender; ;
            var colorpicker = new ColorDialog();
            colorpicker.Color = BackColor;
            if (colorpicker.ShowDialog() != DialogResult.OK) return;
            button.BackColor = colorpicker.Color;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactory
            {
                Ribbon = button5.BackColor,
                Box=button6.BackColor
            };
        }
    }
}
