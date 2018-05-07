using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11.ViewModels.Logic;

namespace App11.Models.SellersModel
 
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Imgurl { get; set; }
        public string Ingredients { get; set; }
        public string Methods { get; set; }
        public string Summary => new AppFunctions().Summerize(Description);
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Owner => FirstName + " " + Surname;
    }
}
