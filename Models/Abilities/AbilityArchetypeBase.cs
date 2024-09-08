using System;
using System.Collections.Generic;

namespace WizardMakerPrototype.Models;

public abstract class AbilityArchetypeBase
{
    public string Name        { get; set; }

    #region Properties possibly useful for LINQ queries
    // Note: These are implemented by each class, to fulfill requirements of certain Interfaces
    //public bool IsSingle   { get; }
    //public bool IsGroup    { get; }
    //public bool IsWildcard { get; }
    #endregion

    public string       Category              { get; protected set; }
    public AbilityType  Type                  { get; protected set; }
    public List<string> CommonSpecializations { get; protected set; } = new List<string>();
    public bool         CannotUseUnskilled    { get; protected set; } = false;
    public bool         IsAccelerated         { get; protected set; } = false;
    public decimal      BaseXpCost            { get; protected set; }

}
