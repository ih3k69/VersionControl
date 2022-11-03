using _7HET_ih3k69.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7HET_ih3k69.Entities
{
    public class CarFactory:IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
