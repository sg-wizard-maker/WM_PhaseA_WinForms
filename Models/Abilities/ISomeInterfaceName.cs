using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models;

public interface ISomeInterfaceName
{
        #region Properties possibly useful for LINQ queries
        public bool IsSingle   { get; }
        public bool IsGroup    { get; }
        public bool IsWildcard { get; }
        #endregion

}
