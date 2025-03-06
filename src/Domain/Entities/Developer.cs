using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Developer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<Game> Games { get; set; }
    }
}
