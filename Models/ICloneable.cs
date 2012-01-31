using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reader.Models
{
    interface ICloneable<out T>
    {
        T Clone();
    }
}
