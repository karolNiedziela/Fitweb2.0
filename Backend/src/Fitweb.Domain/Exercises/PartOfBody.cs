using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exercises
{
    public enum PartOfBody
    {
        [Display(Name = "Klatka piersiowa")]
        Chest = 1,
        [Display(Name = "Nogi")]
        Legs = 2,
        [Display(Name = "Biceps")]
        Biceps = 3,
        [Display(Name = "Triceps")]
        Triceps = 4,
        [Display(Name = "Barki")]
        Shoulders = 5,
        [Display(Name = "Brzuch")]
        Abdominals = 6,
        [Display(Name = "Plecy")]
        Back = 7,
        [Display(Name = "Przedramię")]
        Forearm = 8
    }
}
