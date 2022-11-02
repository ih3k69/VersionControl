using _7HET_ih3k69.Entities;
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
        List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;
        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }



        public Form1()
        {
            InitializeComponent();
            Factory=new BallFactory();
            createTimer.Tick += CreateTimer_Tick;
            conveyorTimer.Tick += ConveyorTimer_Tick;

        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            int jobb=0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (jobb<item.Left)
                {
                    jobb = item.Left;
                }
            }
            if (jobb>1000)
            {
                var régibb = _balls[0];
                mainPanel.Controls.Remove(régibb);
                _balls.Remove(régibb);
            }
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
           var ball= Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }
    }
}
