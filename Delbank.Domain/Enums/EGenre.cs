using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delbank.Domain.Enums
{
    public enum EGenre
    {
        [Description("Action")]
        Action = 1,

        [Description("Horror")]
        Horror = 2,

        [Description("Comedy")]
        Comedy = 3,

        [Description("Romance")]
        Romance = 4,

    }
}
