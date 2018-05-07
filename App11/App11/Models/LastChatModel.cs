using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App11.Models
{
    public  class LastChatModel
    {

        public string name { get; set; }

        public string surname { get; set; }
        public string conversation_id { get; set; }

        public string message
        {
            get; set;
        }
        public DateTime created_at { get; set; }

        public string ImageUrl { get; set; }
        public string user_type { get; set; }
    }
}
