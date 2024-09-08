using System;

namespace WizardMakerPrototype.Models;

public class AbilityArchetypeWildcard : AbilityArchetypeBase, ISomeInterfaceName
{
    public AbilityWildcardGroup IsWithinGroup { get; private set; }

    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return true;  } }
    public bool IsGroup    { get { return false; } }
    public bool IsWildcard { get { return true;  } }
    #endregion

}
