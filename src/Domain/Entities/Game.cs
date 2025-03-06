using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double AVG_Estamalation { get; set; }
        public Developer _Developer { get; set; }
        public List<Review> Reviews { get; set; } 


    }
}
