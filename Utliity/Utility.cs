using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WizardMakerPrototype;

/// <summary>
/// Static class for general-purpose utility methods of various/miscellaneous nature.
/// </summary>
public static class Utility
{
    public static int RandomInteger(Random rr, int min, int max)
    {
        if (min < 0)    { throw new ArgumentOutOfRangeException("min was < 0");    }
        if (max < min)  { throw new ArgumentOutOfRangeException("max was < min");  }
        if (max > 9999) { throw new ArgumentOutOfRangeException("max was > 9999"); }
        var random = rr.NextDouble();  // A value which is (n >= 0.0) and (n < 1.0)
        int result = min + ((int)(random * (max - 1)));  // An integer value from min..max
        return result;
    }

    public static bool RollChanceXOfN(Random rr, int chance, int outOfN)
    {
        if (chance < 0)      { throw new ArgumentException("Got chance < 0");      }
        if (outOfN < chance) { throw new ArgumentException("Got outOfN < chance"); }
        if (outOfN > 9999)   { throw new ArgumentException("Got outOfN > 9999");   }

        //bool hasPuissant = Utility.RandomInteger(rand, 0, 7) == 0;  // This is one out of 8:  (0..7 == 8), equals 0 is 1
        bool result = Utility.RandomInteger(rr, 0, outOfN - 1) <= chance;
        return result;
    }

}
