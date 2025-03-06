using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public Game Write_On { get; set; }
    }
}
