using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype;

/// <summary>
/// This interface is used (rather than interitance) so that <br/>
/// a collection can be composed of mixed AbilityArchetype + AbilityArchetypeWildcard objects, <br/>
/// and so that AbilityInstance can refer to either of these types as the 'parent' Archetype. <br/>
/// </summary>
public interface IAbilityArchetype
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get; }
    public bool IsGroup    { get; }
    public bool IsWildcard { get; }
    #endregion

    public string          Name                  { get; }
    //public string        Description           { get; }  // Likely, but future
    public AbilityCategory AbilityCategory       { get; }
    public AbilityType     AbilityType           { get; }
    public List<string>    CommonSpecializations { get; }
    public bool            CannotUseUnskilled    { get; }
    public bool            IsAccelerated         { get; }
    public decimal         BaseXpCost            { get; }
}

/// <summary>
/// This interface is used to mark distinct (non-common-inheritance) classes <br/>
/// which are eligible to appear in lists (ComboBox / ListBox / etc) for "select an Ability",<br/>
/// where we want "ordinary" AbilityArchetypes (such as for "Athletics")<br/>
/// to appear on the same list as AbilityWildcardGroup (such as for "Craft: X").<br/>
/// <br/>
/// This is primarily useful when viewing Abilities "by Ability Type", <br/>
/// as you would want to "Add new General Ability", and find (Athletics, ..., Craft: X) on the same list,<br/>
/// despite that they are of distinct types (AbilityArchetype, AbilityArchetypeWildcard). <br/>
/// </summary>
public interface IOrdinaryArchetypeOrWildcardGroup
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get; }
    public bool IsGroup    { get; }
    public bool IsWildcard { get; }
    #endregion

    // Within a method wanting such as a parameter, certain logic will 
    // cast to (AbilityArchetype) or (AbilityWildcardGroup), to handle specific functionality.
}

// ...possibly some other Interfaces may be wanted, will find out as various ComboBoxes / Dialogs are implemented...
