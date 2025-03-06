using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GameDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double AVG_Estamalation { get; set; }
        public DeveloperDTO _Developer { get; set; }
        public List<ReviewDTO> Reviews { get; set; } 


    }
}
