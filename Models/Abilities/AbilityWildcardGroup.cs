using System;
using System.Collections.Generic;

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

    public string      Name        { get; protected set; }
    public string      FullName    { get; protected set; }
    //public string    Description { get; protected set; }  // Likely, but future
    public string      Category    { get; protected set; }
    public AbilityType Type        { get; protected set; }

    public string DisplayName { 
        get 
        {
            string str = string.Format("{0}: X", this.Name);
            return str;
        } 
    }

    public string DisplayFullName { 
        get 
        {
            string str = string.Format("{0}: XXX", this.FullName);
            return str;
        } 
    }

    #region Constructors
    public AbilityWildcardGroup( string category, AbilityType type, string name, string fullName )
    {
        // Note that many (but not all) AbilityWildcardGroup
        // have a corresponding AbilityCategory, which map 1:1 in membership.
        // 
        // However, such as "Law: A" or "Theology: B" have AbilityCategory.Academic,
        // rather than defining a new AbilityCategory.

        // Note that a particular AbilityArchetypeWildcard may differ
        // in AbilityType from the "parent" AbilityWildcardGroup, such as for
        //     (Living Language: A) which is AbilityType.General,  AbilityCategory.Languages
        //     (Dead Language:   B) which is AbilityType.Academic, AbilityCategory.Languages

        this.Category = category;
        this.Type     = type;
        this.Name     = name;
        this.FullName = fullName;
    }

    // Possibly also a method to create a new AbilityArchetypeWildcard (calling the ctor) associated with 'this'
    // Hmmm... perhaps that does not simplify data setup;
    // the main question is "would it be used once Serialization is available?"

    static AbilityWildcardGroup()
    {
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(AreaLore);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(OrgLore);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(Craft);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(Profession);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(LanguageLiving);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(LanguageDead);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(Law);
        AbilityWildcardGroup.AllAbilityWildcardGroups.Add(Theology);
    }
    #endregion

    #region Public methods
    public override string ToString ()
    {
        string str = string.Format("Name='{0}', Category='{1}', Type='{2}'",
            this.Name, this.Category, this.Type.Name
            );
        return str;
    }
    #endregion

    #region Setup Static Data
    // These will be used within certain ComboBox or other lists
    // TODO: Once Serialization is ready, most/all of this sort of static data will be replaced, loaded from JSON...

    public static List<AbilityWildcardGroup> AllAbilityWildcardGroups = new List<AbilityWildcardGroup>();

    public static AbilityWildcardGroup AreaLore       = new AbilityWildcardGroup(AbilityCategory.AreaLores,   AbilityType.General, "Area Lore", "Area Lore");
    public static AbilityWildcardGroup OrgLore        = new AbilityWildcardGroup(AbilityCategory.OrgLores,    AbilityType.General, "Org Lore",  "Organization Lore" );
    public static AbilityWildcardGroup Craft          = new AbilityWildcardGroup(AbilityCategory.Crafts,      AbilityType.General, "Craft",     "Craft");
    public static AbilityWildcardGroup Profession     = new AbilityWildcardGroup(AbilityCategory.Professions, AbilityType.General, "Prof",      "Profession");

    // Note that "Living Language" and "Dead Language" are both in AbilityCategory.Languages
    public static AbilityWildcardGroup LanguageLiving = new AbilityWildcardGroup(AbilityCategory.Languages, AbilityType.General,  "Lang", "Living Language");
    public static AbilityWildcardGroup LanguageDead   = new AbilityWildcardGroup(AbilityCategory.Languages, AbilityType.Academic, "Dead", "Dead Language");
    
    // Note that "Law" and "Theology" are both in AbilityCategory.Academic, rather than defining their own AbilityCategory(ies)
    public static AbilityWildcardGroup Law            = new AbilityWildcardGroup(AbilityCategory.Academic, AbilityType.Academic, "Law",      "Law");
    public static AbilityWildcardGroup Theology       = new AbilityWildcardGroup(AbilityCategory.Academic, AbilityType.Academic, "Theology", "Theology");
    #endregion

}
