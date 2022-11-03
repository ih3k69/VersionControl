using _7HET_ih3k69.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7HET_ih3k69.Entities
{
    public class Present:Toy
    {
        public SolidBrush Ribbon { get; private set; }
        public SolidBrush Box { get; private set; }
        public Present(Color ribon, Color box)
        {
            Ribbon = new SolidBrush(ribon);
            Box = new SolidBrush(box);
        }
        protected override void DrawImage(Graphics graf)
        {
            graf.FillRectangle(Box, 0, 0, Width, Height);
            graf.FillRectangle(Ribbon, 0 ,20, (Width), (Height / 5));
            graf.FillRectangle(Ribbon, 20, 0, Width / 5, Height);
        }

    }
}
