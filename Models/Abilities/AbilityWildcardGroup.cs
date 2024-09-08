using System;

namespace WizardMakerPrototype.Models;

/// <summary>
/// 
/// </summary>
public class AbilityWildcardGroup : IOrdinaryArchetypeOrWildcardGroup
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return false; } }
    public bool IsGroup    { get { return true;  } }
    public bool IsWildcard { get { return true;  } }
    #endregion

    public string      Name        { get; set; }
    //public string    Description { get; set; }  // Likely, but future
    public string      Category    { get; protected set; }  // A few "wildcard groups" don't specify their own AbilityCategory, such as (Law: X, Theology: X)  which dwell within AbilityCategory.Academic
    public AbilityType Type        { get; protected set; }  // Individual AbilityArchetypeWildcard could differ, such as (Living Language: A, Dead Language: B) both being within AbilityCategory.Languages

    #region Constructors
    public AbilityWildcardGroup()
    {

    }

    // TODO: ...similar to that for AbilityArchetype

    // Possibly also a method to create a new AbilityArchetypeWildcard (calling the ctor) associated with 'this'
    #endregion

    #region Setup Static Data
    // TODO: ...create one for each "wildcard group", including (Law: X) and (Theology: X)
    // These will be __used__ within certain ComboBox or other lists
    #endregion

}
