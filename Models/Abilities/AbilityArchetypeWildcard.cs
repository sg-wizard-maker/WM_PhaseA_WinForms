using System;

namespace WizardMakerPrototype.Models;

/// <summary>
/// 
/// </summary>
public class AbilityArchetypeWildcard : AbilityArchetypeBase
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return true;  } }
    public bool IsGroup    { get { return false; } }
    public bool IsWildcard { get { return true;  } }
    #endregion

    public AbilityWildcardGroup IsWithinGroup { get; private set; }

}
