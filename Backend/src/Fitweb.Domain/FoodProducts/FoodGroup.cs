using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.FoodProducts
{
    public enum FoodGroup
    {
        [Display(Name = "Produkty zbożowe")]
        Cereals = 1,
        [Display(Name = "Owoce")]
        Fruit = 2,
        [Display(Name = "Warzywa")]
        Vegetables = 3,
        [Display(Name = "Produkty nabiałowe")]
        Dairy = 4,
        [Display(Name = "Mięsa, ryby, jajka")]
        MeatFishEggs = 5,
        [Display(Name = "Cukier i słodycze")]
        SugarAndSweets = 6,
        [Display(Name = "Napoje")]
        Drinks = 7,
        [Display(Name = "Alkohol")]
        Alcohol = 8,
    }
}
