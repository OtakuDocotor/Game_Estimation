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
        public double AverageScore { get; set; }
        public DeveloperDTO Developer { get; set; }
        public List<ReviewDTO> Reviews { get; set; } 
    }
}
