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
        protected override void DrawImage(Graphics graf)
        {
            graf.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }
       
    }
    

}
