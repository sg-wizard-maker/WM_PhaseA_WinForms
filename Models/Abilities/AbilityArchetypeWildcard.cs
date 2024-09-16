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
    public AbilityCategory AbilityCategory       { get; protected set; }
    public AbilityType     AbilityType           { get; protected set; }
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
        this.AbilityCategory = category;
        this.AbilityType     = type;
        this.SpecificAbility = specific;

        this.CommonSpecializations = specializations ?? new List<string>();
        this.BaseXpCost            = IsAccelerated ? 1m : 5m;
        this.CannotUseUnskilled    = cannotUseUnskilled;
        this.IsAccelerated         = isAccelerated;
    }

    static AbilityArchetypeWildcard()
    {
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { AreaLoreOwnCovenant, AreaLoreCovDurenmar,  AreaLoreRhineTribunal  } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { OrgLoreTheChurch,    OrgLoreOrderOfHermes, OrgLoreSomeKnightOrder,           OrgLoreSomeNobleCourt, OrgLoreSomeCraftGuild } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { CraftCarpenter,      CraftBlacksmith,      CraftTanner,  CraftLeatherworker, CraftCarpenter,        CraftWoodcarver, CraftBlacksmith, CraftJeweler, CraftWhitesmith } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { ProfScribe,          ProfApothecary,       ProfJongleur, ProfReeve,          ProfSailor,            ProfSteward,     ProfTeamster,    ProfWasherwoman } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LangEnglish,         LangGerman,           LangItalian,  LangKoineGreek,     LangArabic } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LangLatin,           LangClassicalGreek,   LangHebrew,   LangGothic } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { LawCivilAndCanon,    LawCodeOfHermes,      LawIslamic } );
        AllAbilityArchetypeWildcards.AddRange( new List<AbilityArchetypeWildcard>() { TheologyChristian,   TheologyJudaic,       TheologyIslamic, TheologyPagan } );
    }
    #endregion

    #region Public methods
    public override string ToString ()
    {
        string str = string.Format("Name='{0}', WCGroup='{1}', Category='{2}', Type='{3}'",
            this.Name, this.IsWithinGroup.Name, this.AbilityCategory, this.AbilityType.Name
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
    public static AbilityArchetypeWildcard AreaLoreOwnCovenant   = new AbilityArchetypeWildcard(AbilityWildcardGroup.AreaLore, "(Your own Covenant)", AbilityCategory.AreaLores,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard AreaLoreCovDurenmar   = new AbilityArchetypeWildcard(AbilityWildcardGroup.AreaLore, "Durenmar",            AbilityCategory.AreaLores,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard AreaLoreRhineTribunal = new AbilityArchetypeWildcard(AbilityWildcardGroup.AreaLore, "Rhine Tribunal",      AbilityCategory.AreaLores,   AbilityType.General, emptyListOfSpecialties );

    // Org Lores
    public static AbilityArchetypeWildcard OrgLoreTheChurch       = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "The Church",         AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties);
    public static AbilityArchetypeWildcard OrgLoreOrderOfHermes   = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "Order of Hermes",    AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard OrgLoreSomeKnightOrder = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "SomeKnightOrder",    AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard OrgLoreSomeNobleCourt  = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "SomeNobleCourt",     AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard OrgLoreSomeCraftGuild  = new AbilityArchetypeWildcard(AbilityWildcardGroup.OrgLore, "SomeCraftGuild",     AbilityCategory.OrgLores,    AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );

    // Crafts

    public static AbilityArchetypeWildcard CraftTanner          = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Tanner",                 AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftLeatherworker   = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Leatherworker",          AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftCarpenter       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Carpentry",              AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftWoodcarver      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Woodcarver",             AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftBlacksmith      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Blacksmith",             AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard CraftJeweler         = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Jeweler",                AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard CraftWhitesmith      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Craft, "Whitesmith",             AbilityCategory.Crafts,      AbilityType.General, emptyListOfSpecialties );

    // Professions
    public static AbilityArchetypeWildcard ProfScribe           = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Scribe",            AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard ProfApothecary       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Apothecary",        AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard ProfJongleur         = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Jongleur",          AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard ProfReeve            = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Reeve",             AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard ProfSailor           = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Sailor",            AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard ProfSteward          = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Steward",           AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard ProfTeamster         = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Teamster",          AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );
    public static AbilityArchetypeWildcard ProfWasherwoman      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Profession, "Washerwoman",       AbilityCategory.Professions, AbilityType.General, emptyListOfSpecialties );

    // Living Languages
    public static AbilityArchetypeWildcard LangEnglish          = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "English",       AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangGerman           = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "German",        AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangItalian          = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "Italian",       AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard LangKoineGreek       = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "Koine Greek",   AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard LangArabic           = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageLiving, "Arabic",        AbilityCategory.Languages,   AbilityType.General, emptyListOfSpecialties, NO_UNSKILLED );

    // Dead Languages
    public static AbilityArchetypeWildcard LangLatin            = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Latin",           AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangClassicalGreek   = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Classical Greek", AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LangHebrew           = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Hebrew",          AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard LangGothic           = new AbilityArchetypeWildcard(AbilityWildcardGroup.LanguageDead, "Gothic",          AbilityCategory.Languages,   AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED );

    // Law
    public static AbilityArchetypeWildcard LawCivilAndCanon     = new AbilityArchetypeWildcard(AbilityWildcardGroup.Law, "Civil and Canon",          AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LawCodeOfHermes      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Law, "Code of Hermes",           AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard LawIslamic           = new AbilityArchetypeWildcard(AbilityWildcardGroup.Law, "Islamic",                  AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED );

    // Theology
    public static AbilityArchetypeWildcard TheologyChristian    = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Christian",           AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard TheologyJudaic       = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Judaic",              AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED);
    public static AbilityArchetypeWildcard TheologyIslamic      = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Islamic",             AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetypeWildcard TheologyPagan        = new AbilityArchetypeWildcard(AbilityWildcardGroup.Theology, "Pagan",               AbilityCategory.Academic,    AbilityType.Academic, emptyListOfSpecialties, NO_UNSKILLED );

    #endregion
}
