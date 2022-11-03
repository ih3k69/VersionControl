using _7HET_ih3k69.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _7HET_ih3k69.Entities
{
    public class Ball : Toy
    {
        public SolidBrush BallColor { get; private set; }
        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);    
        }
        protected override void DrawImage(Graphics graf)
        {
            graf.FillEllipse(BallColor, 0, 0, Width, Height);
        }
       
    }
    

}
