using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App11.Models
{
    [DataContract]
    public class ChatListModel
    {
        [DataMember]
        public  string id { get; set; }
        [DataMember]
        public int Sender_id { get; set; }
        [DataMember]
        public  String SenderName { get; set; }
        [DataMember]
        public int Receiver_id { get; set; }
        [DataMember]
        public  String Post_id { get; set; }
        [DataMember]
        public  String  ReceiverName { get; set; }
        [DataMember]
        public  DateTime created_at { get; set; }

        [DataMember]
        public List<messages> messages { get; set; }

    }

  
    [DataContract]
    public class messages
    {
    
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string message
        {
            get; set;
        }
        [DataMember]
        public DateTime created_at { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string user_type { get; set; }


    }

}
