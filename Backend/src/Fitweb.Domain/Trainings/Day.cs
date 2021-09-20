using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Trainings
{
    public enum Day
    {
        [Display(Name = "Poniedziałek")]
        Monday = 1,
        [Display(Name = "Wtorek")]
        Tuesday = 2,
        [Display(Name = "Środa")]
        Wednesday = 3,
        [Display(Name = "Czwartek")]
        Thursday = 4,
        [Display(Name = "Piątek")]
        Friday = 5,
        [Display(Name = "Sobota")]
        Saturday = 6,
        [Display(Name = "Niedziela")]
        Sunday = 7
    }
}
