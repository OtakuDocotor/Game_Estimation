using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class DeveloperDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<GameDTO> Games { get; set; }
    }
}
