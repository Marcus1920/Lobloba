using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App11.Views.Buyers
{

    public class BuyersHomeMasterMenuItem
    {
        public BuyersHomeMasterMenuItem()
        {
            TargetType = typeof(BuyersHomeMasterDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}