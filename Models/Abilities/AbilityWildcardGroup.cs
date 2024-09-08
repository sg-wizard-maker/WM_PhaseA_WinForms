using System;

namespace WizardMakerPrototype.Models;

public class AbilityWildcardGroup : ISomeInterfaceName
{
    public string Name        { get; set; }
    public string Description { get; set; }

    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return false; } }
    public bool IsGroup    { get { return true;  } }
    public bool IsWildcard { get { return true;  } }
    #endregion

}
