using System;
using System.Collections.Generic;

namespace WizardMakerPrototype.Models;

/// <summary>
/// 
/// </summary>
public class AbilityArchetypeWildcard : IAbilityArchetype
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return true;  } }
    public bool IsGroup    { get { return false; } }
    public bool IsWildcard { get { return true;  } }
    #endregion

    #region Public Properties
    public string Name {
        get
        {
            string parentName = IsWithinGroup.Name;
            string str = string.Format("{0}: {1}", parentName, this.SpecificAbility);
            return str;
        }
    }

    public string FullName
    {
        get
        {
            string parentFullName = IsWithinGroup.FullName;
            string str = string.Format("{0}: {1}", parentFullName, this.SpecificAbility);
            return str;
        }
    }

    //public string        Description           { get; protected set; }  // Likely, but future
    public AbilityCategory Category              { get; protected set; }
    public AbilityType     Type                  { get; protected set; }
    public List<string>    CommonSpecializations { get; protected set; } = new List<string>();
    public bool            CannotUseUnskilled    { get; protected set; } = false;
    public bool            IsAccelerated         { get; protected set; } = false;
    public decimal         BaseXpCost            { get; protected set; }

    public AbilityWildcardGroup IsWithinGroup   { get; protected set; }
    public string               SpecificAbility { get; protected set; }
    #endregion

    #region Constructors
    public AbilityArchetypeWildcard(AbilityWildcardGroup group, string specific, AbilityCategory category, AbilityType type,
        List<string> specializations = null, bool cannotUseUnskilled = false, bool isAccelerated = false)
    {
        this.IsWithinGroup   = group;
        this.Category        = category;
        this.Type            = type;
        this.SpecificAbility = specific;

        this.CommonSpecializations = specializations ?? new List<string>();
        this.BaseXpCost            = IsAccelerated ? 1m : 5m;
        this.CannotUseUnskilled    = cannotUseUnskilled;
        this.IsAccelerated         = isAccelerated;
    }

    static AbilityArchetypeWildcard()
    {
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { AreaLoreOwnCovenant, AreaLoreCovDurenmar  } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { OrgLoreTheChurch,    OrgLoreOrderOfHermes } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { CraftCarpentry,      CraftBlacksmith      } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { ProfScribe,          ProfApothecary       } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LangEnglish,         LangGerman           } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LangLatin,           LangClassicalGreek   } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LawCivilAndCanon,    LawCodeOfHermes      } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { TheologyChristian,   TheologyJudaic       } );
    }
    #endregion

    #region Public methods
    public override string ToString ()
    {
        string str = string.Format("Name='{0}', WCGroup='{1}', Category='{2}', Type='{3}'",
            this.Name, this.IsWithinGroup.Name, this.Category, this.Type.Name
            );
        return str;
    }
    #endregion

    #region Static data for Common Specializations
    private static List<string> emptyListOfSpecialties = new List<string>();
    #endregion

    #region Static Data setup
    // TODO: Once Serialization is ready, most/all of this sort of static data will be replaced, loaded from JSON...

    public static List<AbilityArchetypeWildcard> AllAbilityArchetypeWildcards = new List<AbilityArchetypeWildcard>();
    private const bool  NO_UNSKILLED = true;
    private const bool YES_UNSKILLED = false;

    // Area Lores
    public static AbilityArchetypeWildcard AreaLoreOwnCovenant  = new AbilityArchetypeWildcard(AbilityWildcardGroup.AreaLore, "(Your own Covenant)", AbilityCategory.AreaLores,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard AreaLoreCovDurenmar  = new AbilityArchetypeWildcard(AbilityWildcardGroup.AreaLore, "Durenmar",            AbilityCategory.AreaLores,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    // Org Lores
    public static AbilityArchetypeWildcard OrgLoreTheChurch     = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "The Church",           AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties);
    public static AbilityArchetypeWildcard OrgLoreOrderOfHermes = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "Order of Hermes",      AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    // Crafts
    public static AbilityArchetypeWildcard CraftCarpentry       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Carpentry",              AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftBlacksmith      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Blacksmith",             AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    // Professions
    public static AbilityArchetypeWildcard ProfScribe           = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Scribe",            AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard ProfApothecary       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Apothecary",        AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    // Living Languages
    public static AbilityArchetypeWildcard LangEnglish          = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "English",       AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangGerman           = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "German",        AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    // Dead Languages
    public static AbilityArchetypeWildcard LangLatin            = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Latin",           AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangClassicalGreek   = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Classical Greek", AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    // Law
    public static AbilityArchetypeWildcard LawCivilAndCanon     = new AbilityArchetypeWildcard(AbilityWildcardGroup.Law, "Civil and Canon",          AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LawCodeOfHermes      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Law, "Code of Hermes",           AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    // Theology
    public static AbilityArchetypeWildcard TheologyChristian    = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Christian",           AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard TheologyJudaic       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Judaic",              AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);

    #endregion
}
