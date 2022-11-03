using _7HET_ih3k69.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7HET_ih3k69.Entities
{
    public class PresentFactory: IToyFactory
    {
        public Color Ribbon { get; set; }
        public Color Box { get; set; }
        public Toy CreateNew()
        {
            return new Present(Ribbon, Box);
        }
    }
}
