using System;
using System.Collections.Generic;

namespace WizardMakerPrototype.Models;

/// <summary>
/// Abstract base class for (AbilityArchetype, AbilityArchetypeWildcard).
/// </summary>
//public abstract class AbilityArchetypeBase
//{
//    #region Properties possibly useful for LINQ queries
//    // Note: These are implemented by each class, to fulfill requirements of certain Interfaces
//    //public bool IsSingle   { get; }
//    //public bool IsGroup    { get; }
//    //public bool IsWildcard { get; }
//    #endregion
//
//    public string          Name                  { get; protected set; }
//    //public string        Description           { get; protected set; }  // Likely, but future
//    public AbilityCategory Category              { get; protected set; }
//    public AbilityType     Type                  { get; protected set; }
//    public List<string>    CommonSpecializations { get; protected set; } = new List<string>();
//    public bool            CannotUseUnskilled    { get; protected set; } = false;
//    public bool            IsAccelerated         { get; protected set; } = false;
//    public decimal         BaseXpCost            { get; protected set; }
//
//}
