using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UserDTO Author { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
