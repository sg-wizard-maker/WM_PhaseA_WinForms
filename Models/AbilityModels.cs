using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
//using System.Text.Json.Serialization;  // JsonConverterAttribute
using System.Xml.Serialization;

namespace WizardMakerPrototype.Models;

/// <summary>
/// These are groupings of related Abilities for display, such as:<br/>
///  - Martial, Physical, Social, Languages, Crafts, Professions, <br/>
///  - Academic, Supernatural, Arcane, Area Lores, Organization Lores<br/>
/// <br/>
/// Note that the names of some of these groupings are identical to 
/// the names of AbilityTypes, such as:<br/>
/// - (Martial, Academic, Arcane, Supernatural)<br/>
/// <br/>
/// AbilityCategories are not defined as such in the ArM5 rules,
/// but rather are used for convenience of display.<br/>
/// - Some people prefer a single alphabetized list, which has advantages for a very short list of Abilities.<br/>
/// - Others prefer a number of AbilityCategories (possibly alphabetized within each).<br/>
/// </summary>
public static class AbilityCategory
{
    public static List<String> Categories = new List<string>();

    //public static string Martial      = "Martial Abilities";
    //public static string Physical     = "Physical Abilities";
    //public static string Social       = "Social Abilities";
    //public static string Languages    = "Languages";
    //public static string Crafts       = "Crafts";
    //public static string Professions  = "Professions";
    //public static string Academic     = "Academic Abilities";
    //public static string Supernatural = "Supernatural Abilities";
    //public static string Arcane       = "Arcane Abilities";
    //public static string RealmLores   = "Supernatural Realms";
    //public static string AreaLores    = "Area Lores";
    //public static string OrgLores     = "Organization Lores";

    public static string Martial      = "Martial";
    public static string Physical     = "Physical";
    public static string Social       = "Social";
    public static string Languages    = "Language";
    public static string Crafts       = "Crafts";
    public static string Professions  = "Professions";
    public static string Academic     = "Academic";
    public static string Supernatural = "Supernatural";
    public static string Arcane       = "Arcane";
    public static string RealmLores   = "Realm Lores";
    public static string AreaLores    = "Area Lores";
    public static string OrgLores     = "Org Lores";


    static AbilityCategory ()
    {
        Categories.Add( Martial      );
        Categories.Add( Physical     );
        Categories.Add( Social       );
        Categories.Add( Languages    );
        Categories.Add( Crafts       );
        Categories.Add( Professions  );
        Categories.Add( Academic     );
        Categories.Add( Supernatural );
        Categories.Add( Arcane       );
        Categories.Add( RealmLores   );
        Categories.Add( AreaLores    );
        Categories.Add( OrgLores     );
    }
}

/// <summary>
/// These are types of Abilities as defined in the ArM5 rules.<br/>
/// Characters have access to learn Abilities based upon their type,
/// and whether the character has an associated Virtue or Flaw granting access.<br/>
/// <br/>
/// The types are:<br/>
/// - [Gc]   General (child) - The initial 120 XP for "Early Life" can be spent on these<br/>
/// - [G]    General         - Any character can spend XP on these<br/>
/// - [M]    Martial         - Requires a Virtue/Flaw to spend XP on these during chargen<br/>
/// - [Acad] Academic        - Requires literacy, granted by certain Virtues<br/>
/// - [Arc]  Arcane          - Requires certain Virtues/Flaws<br/>
/// - [Sup]  Supernatural    - Each individual Ability in this grouping requires a specific Virtue (or Gifted tradition)<br/>
/// </summary>
public class AbilityType
{
    public static List<AbilityType> Types = new List<AbilityType>();

    public string Abbreviation { get; private set; }
    public string Name         { get; private set; }
    // TODO:
    // Need some additional properties defining the relations to
    // requirements/associations with certain Virtues/Flaws, age categories, etc...

    private AbilityType(string abbrev, string name)
    {
        this.Abbreviation = abbrev;
        this.Name = name;
    }

    public static AbilityType GenChild     = new AbilityType("GC",   "General+Child" );
    public static AbilityType General      = new AbilityType("Gen",  "General"       );
    public static AbilityType Martial      = new AbilityType("Mar",  "Martial"       );
    public static AbilityType Academic     = new AbilityType("Acad", "Academic"      );
    public static AbilityType Arcane       = new AbilityType("Arc",  "Arcane"        );
    public static AbilityType Supernatural = new AbilityType("Sup",  "Supernatural"  );
    public static AbilityType Secret       = new AbilityType("Sec",  "Secret"        );  // Not specified in (ArM5 core book), but useful

    static AbilityType()
    {
        Types.Add( GenChild     );
        Types.Add( General      );
        Types.Add( Martial      );
        Types.Add( Academic     );
        Types.Add( Arcane       );
        Types.Add( Supernatural );
        Types.Add( Secret       );  // Not specified in (ArM5 core book), but useful
    }
}

/// <summary>
/// These are objects which describe a particular Ability in the abstract,
/// as opposed to what degree of skill a particular character has attained.<br/>
/// <br/>
/// Therefore, a character may have an AbilityInstance which <br/>
/// - refers to the AbilityArchetype for "Athletics", <br/>
/// - specifies (how many XP they have, what Specialization).<br/>
/// <br/>
/// Thus for example, the Ability "Athletics" is defined as <br/>
/// - being named "Athletics", <br/>
/// - of Category "Physical", <br/>
/// - of AbilityType "General", <br/>
/// - usable unskilled, <br/>
/// - not "accelerated", <br/>
/// - needing 5 XP to reach a Score of 1, <br/>
/// - and with certain common Specializations.
/// </summary>
public class AbilityArchetype
{
    public string       Category              { get; private set; }
    public AbilityType  Type                  { get; private set; }
    public string       Name                  { get; private set; }
    public List<string> CommonSpecializations { get; private set; } = new List<string>();
    public bool         CannotUseUnskilled    { get; private set; } = false;
    public bool         IsAccelerated         { get; private set; } = false;
    public decimal      BaseXpCost            { get; private set; }

    public AbilityArchetype ( string category, AbilityType type, string name, List<string> specializations, 
        bool cannotUseUnskilled = false, bool isAccelerated = false )
    {
        this.Category = category;
        this.Type = type;
        this.Name = name;
        this.CommonSpecializations = specializations ?? new List<string>();
        this.BaseXpCost            = isAccelerated ? 1m : 5m;
        this.CannotUseUnskilled    = cannotUseUnskilled;
        this.IsAccelerated         = isAccelerated;
    }

    public static List<AbilityArchetype> AllCommonAbilities = new List<AbilityArchetype>();

    private const bool  NO_UNSKILLED = true;
    private const bool YES_UNSKILLED = false;

    #region Static data for Common Specializations
    private static List<string> emptyListOfSpecialties = new List<string>();

    private static List<string> brawlSpecializations        = new List<string>() { "Dodging", "Punches", "Kicks", "Grapples", "Knives", "Bludgeon" };
    private static List<string> singleWeaponSpecializations = new List<string>() { "Axe/Hatchet", "Club/Mace", "Mace and Chain", "Short Spear", "Short Sword", "Long Sword", "Shields" };
    private static List<string> greatWeaponSpecializations  = new List<string>() { "Cudgel", "Farm implement", "Flail", "Pole Arm", "Pole Axe", "Long Spear", "Great Sword", "Staff", "Warhammer" };
    private static List<string> bowsSpecializations         = new List<string>() { "Short bow", "Long bow", "Crossbow" };
    private static List<string> thrownSpecializations       = new List<string>() { "Throwing axe", "Javelin", "Thrown knife", "Sling", "Thrown stone" };

    // ...
    #endregion

    #region Setup static data for Common Abilities
    // TODO:
    // In future, rather than a hard-coded list,
    // the program will offer the means to declare a new Ability at runtime.
    // 
    // This will be especially needful, when considering "wildcard" Abilities which describe a list of many Abilities, such as:
    //     (Language, Dead Language, Craft, Profession, Area Lore, Organization Lore)
    // 
    // This will require that this data
    // is loaded from JSON files
    //     (one for CommonAbilities, at least one for additional / user-defined)
    // upon startup,
    // and saved to JSON files
    //     (at least for additional / user-defined)
    // upon (when XXX is saved, possibly also at shutdown)

    // Category: Martial
    public static AbilityArchetype Brawl        = new AbilityArchetype(AbilityCategory.Martial, AbilityType.GenChild, "Brawl",         brawlSpecializations );
    public static AbilityArchetype SingleWeapon = new AbilityArchetype(AbilityCategory.Martial, AbilityType.Martial,  "Single Weapon", singleWeaponSpecializations );
    public static AbilityArchetype GreatWeapon  = new AbilityArchetype(AbilityCategory.Martial, AbilityType.Martial,  "Great Weapon",  greatWeaponSpecializations  );
    public static AbilityArchetype Bows         = new AbilityArchetype(AbilityCategory.Martial, AbilityType.Martial,  "Bows",          bowsSpecializations   );
    public static AbilityArchetype Thrown       = new AbilityArchetype(AbilityCategory.Martial, AbilityType.Martial,  "Thrown",        thrownSpecializations );

    // Category: Physical
    public static AbilityArchetype AnimalHandling = new AbilityArchetype(AbilityCategory.Physical, AbilityType.General,  "Animal Handling", emptyListOfSpecialties );
    public static AbilityArchetype Athletics      = new AbilityArchetype(AbilityCategory.Physical, AbilityType.GenChild, "Athletics",       emptyListOfSpecialties );
    public static AbilityArchetype Awareness      = new AbilityArchetype(AbilityCategory.Physical, AbilityType.GenChild, "Awareness",       emptyListOfSpecialties );
    public static AbilityArchetype Hunt           = new AbilityArchetype(AbilityCategory.Physical, AbilityType.General,  "Hunt",            emptyListOfSpecialties );
    public static AbilityArchetype Legerdemain    = new AbilityArchetype(AbilityCategory.Physical, AbilityType.General,  "Legerdemain",     emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Ride           = new AbilityArchetype(AbilityCategory.Physical, AbilityType.General,  "Ride",            emptyListOfSpecialties );
    public static AbilityArchetype Stealth        = new AbilityArchetype(AbilityCategory.Physical, AbilityType.GenChild, "Stealth",         emptyListOfSpecialties );
    public static AbilityArchetype Survival       = new AbilityArchetype(AbilityCategory.Physical, AbilityType.GenChild, "Survival",        emptyListOfSpecialties );
    public static AbilityArchetype Swim           = new AbilityArchetype(AbilityCategory.Physical, AbilityType.GenChild, "Swim",            emptyListOfSpecialties );

    // Category: Social
    public static AbilityArchetype Bargain        = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Bargain",    emptyListOfSpecialties );
    public static AbilityArchetype Carouse        = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Carouse",    emptyListOfSpecialties );
    public static AbilityArchetype Charm          = new AbilityArchetype(AbilityCategory.Social, AbilityType.GenChild, "Charm",      emptyListOfSpecialties );
    public static AbilityArchetype Etiquette      = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Etiquette",  emptyListOfSpecialties );
    public static AbilityArchetype FolkKen        = new AbilityArchetype(AbilityCategory.Social, AbilityType.GenChild, "Folk Ken",   emptyListOfSpecialties );
    public static AbilityArchetype Guile          = new AbilityArchetype(AbilityCategory.Social, AbilityType.GenChild, "Guile",      emptyListOfSpecialties );
    public static AbilityArchetype Intrigue       = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Intrigue",   emptyListOfSpecialties );
    public static AbilityArchetype Music          = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Music",      emptyListOfSpecialties );
    public static AbilityArchetype Leadership     = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Leadership", emptyListOfSpecialties );
    public static AbilityArchetype Teaching       = new AbilityArchetype(AbilityCategory.Social, AbilityType.General,  "Teaching",   emptyListOfSpecialties );

    // Category: Languages - Living Languages (General)
    public static AbilityArchetype LangEnglish    = new AbilityArchetype(AbilityCategory.Languages, AbilityType.General, "Lang: English",     emptyListOfSpecialties );
    public static AbilityArchetype LangHighGerman = new AbilityArchetype(AbilityCategory.Languages, AbilityType.General, "Lang: High German", emptyListOfSpecialties );
    public static AbilityArchetype LangItalian    = new AbilityArchetype(AbilityCategory.Languages, AbilityType.General, "Lang: Italian",     emptyListOfSpecialties );
    public static AbilityArchetype LangKoineGreek = new AbilityArchetype(AbilityCategory.Languages, AbilityType.General, "Lang: Koine Greek", emptyListOfSpecialties );
    public static AbilityArchetype LangArabic     = new AbilityArchetype(AbilityCategory.Languages, AbilityType.General, "Lang: Arabic",      emptyListOfSpecialties );
    // ...

    // Category: Languages - Dead Languages (Academic)
    public static AbilityArchetype LangLatin        = new AbilityArchetype(AbilityCategory.Languages, AbilityType.Academic, "Dead: Latin",         emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LangAncientGreek = new AbilityArchetype(AbilityCategory.Languages, AbilityType.Academic, "Dead: Ancient Greek", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LangHebrew       = new AbilityArchetype(AbilityCategory.Languages, AbilityType.Academic, "Dead: Hebrew",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LangGothic       = new AbilityArchetype(AbilityCategory.Languages, AbilityType.Academic, "Dead: Gothic",        emptyListOfSpecialties, NO_UNSKILLED );
    // ...

    // Category: Crafts
    public static AbilityArchetype CraftTanner        = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Tanner",        emptyListOfSpecialties );
    public static AbilityArchetype CraftLeatherworker = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Leatherworker", emptyListOfSpecialties );
    public static AbilityArchetype CraftCarpenter     = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Carpenter",     emptyListOfSpecialties );
    public static AbilityArchetype CraftWoodcarver    = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Woodcarver",    emptyListOfSpecialties );
    public static AbilityArchetype CraftBlacksmith    = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Blacksmith",    emptyListOfSpecialties );
    public static AbilityArchetype CraftJeweler       = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Jeweler",       emptyListOfSpecialties );
    public static AbilityArchetype CraftWhitesmith    = new AbilityArchetype(AbilityCategory.Crafts, AbilityType.General,  "Craft: Whitesmith",    emptyListOfSpecialties );
    // ...

    // Category: Professions
    public static AbilityArchetype ProfScribe      = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Scribe",      emptyListOfSpecialties );
    public static AbilityArchetype ProfApothecary  = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Apothecary",  emptyListOfSpecialties );
    // ...
    public static AbilityArchetype ProfJongleur    = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Jongleur",    emptyListOfSpecialties );
    public static AbilityArchetype ProfReeve       = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Reeve",       emptyListOfSpecialties );
    public static AbilityArchetype ProfSailor      = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Sailor",      emptyListOfSpecialties );
    public static AbilityArchetype ProfSteward     = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Steward",     emptyListOfSpecialties );
    public static AbilityArchetype ProfTeamster    = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Teamster",    emptyListOfSpecialties );
    public static AbilityArchetype ProfWasherwoman = new AbilityArchetype(AbilityCategory.Professions, AbilityType.General,  "Prof: Washerwoman", emptyListOfSpecialties );
    // ...

    // Category: Academic
    public static AbilityArchetype ArtesLiberales    = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Artes Liberales", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype ArtOfMemory       = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Art of Memory",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Chirurgy          = new AbilityArchetype(AbilityCategory.Academic, AbilityType.General,   "Chirurgy",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Medicine          = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Medicine",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Philosophiae      = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Philosophiae",    emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    public static AbilityArchetype LawCodeOfHermes   = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Law: Code of Hermes", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LawCivilAndCanon  = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Law: Civil & Canon",  emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LawIslamic        = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Law: Islamic",        emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    public static AbilityArchetype TheologyChristian = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Christian", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype TheologyJudaic    = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Judaic",    emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype TheologyIslamic   = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Islamic",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype TheologyPagan     = new AbilityArchetype(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Pagan",     emptyListOfSpecialties, NO_UNSKILLED );
    // ...

    // Category: Supernatural
    public static AbilityArchetype AnimalKen        = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Animal Ken",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Dowsing          = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Dowsing",           emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype EnchantingMusic  = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Enchanting Music",  emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Entrancement     = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Entrancement",      emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype MagicSensitivity = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Magic Sensitivity", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Premonitions     = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Premonitions",      emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype SecondSight      = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Second Sight",      emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype SenseHolyUnholy  = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Sense Holy/Unholy", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Shapeshifter     = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Shapeshifter",      emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype WildernessSense  = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Wilderness Sense",  emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    public static AbilityArchetype TheEnigma     = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "The Enigma",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype HeartBeast    = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "HeartBeast",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype FaerieMagic   = new AbilityArchetype(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Faerie Magic", emptyListOfSpecialties, NO_UNSKILLED );

    // Category: Arcane
    public static AbilityArchetype HermeticMagicTheory  = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Hermetic Magic Theory",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype FolkWitchMagicTheory = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Folk Witch Magic Theory", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype ParmaMagica          = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Parma Magica",            emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype Certamen             = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Certamen",                emptyListOfSpecialties );
    public static AbilityArchetype Concentration        = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.General, "Concentration",           emptyListOfSpecialties );
    public static AbilityArchetype Finesse              = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Finesse",                 emptyListOfSpecialties );
    public static AbilityArchetype Penetration          = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Penetration",             emptyListOfSpecialties );
    public static AbilityArchetype Recuperation         = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Recuperation",            emptyListOfSpecialties );
    public static AbilityArchetype Withstanding         = new AbilityArchetype(AbilityCategory.Arcane, AbilityType.Arcane,  "Withstanding",            emptyListOfSpecialties );

    // Category: Supernatural Realm Lores
    public static AbilityArchetype MagicLore       = new AbilityArchetype(AbilityCategory.RealmLores, AbilityType.Arcane,  "Magic Lore",    emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype FaerieLore      = new AbilityArchetype(AbilityCategory.RealmLores, AbilityType.Arcane,  "Faerie Lore",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype DominionLore    = new AbilityArchetype(AbilityCategory.RealmLores, AbilityType.Arcane,  "Dominion Lore", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype InfernalLore    = new AbilityArchetype(AbilityCategory.RealmLores, AbilityType.Arcane,  "Infernal Lore", emptyListOfSpecialties, NO_UNSKILLED );

    // Category: Area Lores
    public static AbilityArchetype AreaLoreVeryBasic     = new AbilityArchetype(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: VeryBasic",     emptyListOfSpecialties );
    public static AbilityArchetype AreaLoreSomeCountry   = new AbilityArchetype(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: SomeCountry",   emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype AreaLoreSomeTribunal  = new AbilityArchetype(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: SomeTribunal",  emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    public static AbilityArchetype AreaLoreSomeCovenant  = new AbilityArchetype(AbilityCategory.AreaLores, AbilityType.Arcane,  "Area Lore: SomeCovenant",  emptyListOfSpecialties, NO_UNSKILLED );
    // ...

    // Category: Secret Abilities
    public static AbilityArchetype MartialSecret         = new AbilityArchetype(AbilityCategory.Martial,     AbilityType.Secret,  "Secret Martial",         emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype PhysicalSecret        = new AbilityArchetype(AbilityCategory.Physical,    AbilityType.Secret,  "Secret Physical",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype SocialSecret          = new AbilityArchetype(AbilityCategory.Social,      AbilityType.Secret,  "Secret Social",          emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype LangSecret            = new AbilityArchetype(AbilityCategory.Languages,   AbilityType.Secret,  "Lang: Secret",           emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype CraftSecret           = new AbilityArchetype(AbilityCategory.Crafts,      AbilityType.Secret,  "Craft: Secret",          emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype ProfSecret            = new AbilityArchetype(AbilityCategory.Professions, AbilityType.Secret,  "Prof: Secret",           emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype AcadSecret            = new AbilityArchetype(AbilityCategory.Academic,    AbilityType.Secret,  "Secret Academic",        emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype AreaLoreSecret        = new AbilityArchetype(AbilityCategory.AreaLores,   AbilityType.Secret,  "Area Lore: Secret Area", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype OrgLoreSecret         = new AbilityArchetype(AbilityCategory.OrgLores,    AbilityType.Secret,  "Org Lore: Secret Org",   emptyListOfSpecialties, NO_UNSKILLED );
    // ...

    // Category: Organization Lores
    public static AbilityArchetype OrgLoreChurch          = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: The Church",      emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype OrgLoreOrderOfHermes   = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.Arcane,  "Org Lore: Order of Hermes", emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    public static AbilityArchetype OrgLoreVeryBasic       = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: VeryBasic",       emptyListOfSpecialties );
    public static AbilityArchetype OrgLoreSomeKnightOrder = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeKnightOrder", emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype OrgLoreSomeNobleCourt  = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeNobleCourt",  emptyListOfSpecialties, NO_UNSKILLED );
    public static AbilityArchetype OrgLoreSomeCraftGuild  = new AbilityArchetype(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeCraftGuild",  emptyListOfSpecialties, NO_UNSKILLED );
    // ...
    #endregion

    static AbilityArchetype ()
    {
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { Brawl, SingleWeapon, GreatWeapon, Bows, Thrown } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { AnimalHandling, Athletics, Awareness, Hunt, Legerdemain, Ride, Stealth, Survival, Swim } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { Bargain, Carouse, Charm, Etiquette, FolkKen, Guile, Intrigue, Music, Leadership, Teaching } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { LangEnglish, LangHighGerman, LangItalian, LangKoineGreek, LangArabic } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { LangLatin, LangAncientGreek, LangHebrew, LangGothic } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { CraftTanner, CraftLeatherworker, CraftCarpenter, CraftWoodcarver, CraftBlacksmith, CraftJeweler, CraftWhitesmith } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { ProfScribe, ProfApothecary, ProfJongleur, ProfReeve, ProfSailor, ProfSteward, ProfTeamster, ProfWasherwoman } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { ArtesLiberales, ArtOfMemory, Chirurgy, Medicine, Philosophiae } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { LawCodeOfHermes, LawCivilAndCanon, LawIslamic } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { TheologyChristian, TheologyJudaic, TheologyIslamic, TheologyPagan } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { AnimalKen, Dowsing, EnchantingMusic, Entrancement, MagicSensitivity, Premonitions, SecondSight, SenseHolyUnholy, Shapeshifter, WildernessSense } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { TheEnigma, HeartBeast, FaerieMagic } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { HermeticMagicTheory, FolkWitchMagicTheory, ParmaMagica, Certamen, Concentration, Finesse, Penetration, Recuperation, Withstanding } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { MagicLore, FaerieLore, DominionLore, InfernalLore } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { AreaLoreVeryBasic, AreaLoreSomeCountry, AreaLoreSomeTribunal, AreaLoreSomeCovenant } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { OrgLoreChurch, OrgLoreOrderOfHermes, OrgLoreVeryBasic, OrgLoreSomeKnightOrder, OrgLoreSomeNobleCourt, OrgLoreSomeCraftGuild } );
        AllCommonAbilities.AddRange( new List<AbilityArchetype>() { MartialSecret, PhysicalSecret, SocialSecret, LangSecret, CraftSecret, ProfSecret, AcadSecret, OrgLoreSecret, AreaLoreSecret } );
    }
}

/// <summary>
/// These are objects which refer to a particular AbilityArchetype,
/// and which describe the details of an Ability possessed by a character.<br/>
/// <br/>
/// Therefore, a character may have an AbilityInstance which <br/>
/// - refers to the AbilityArchetype for "Athletics", and<br/>
/// - specifies (how many XP they have, what Specialization).<br/>
/// <br/>
/// Thus for example, such an AbilityInstance may describe that
/// in (the Ability described by the AbilityArchetype) the character<br/>
/// - has nnn XP<br/>
/// - has "Affinity"  in that Ability, needing less XP to increase Score<br/>
/// - has "Puissance" in that Ability, adding some value (default +2) to the Score<br/>
/// - has a particular Specialization in that Ability<br/>
/// </summary>
public class AbilityInstance
{
    // To make a public property NOT present in the DataGridView, apply an attribute such as:
    //     [System.ComponentModel.Browsable(false)]
    // 
    //[Browsable(false)]
    //public string PropertyNotAppearingInThisDataGridView { get; set; }
    // 
    // To make a public property present in the DataGridView (and thus accessible to business logic), but NOT displayed:
    //     dataGridView1.Columns[0].Visible = false;

    [Browsable(false)]
    public AbilityArchetype Archetype { get; private set; }

    [Browsable(false)]
    public decimal     BaseXPCost { get { return Archetype.BaseXpCost; } }

    [DisplayName("Category")]
    public string Category   { get { return this.Archetype.Category;          } }

    [Browsable(false)]
    public string Type       { get { return this.Archetype.Type.Name;         } }

    [DisplayName("Type")]
    public string TypeAbbrev { get { return this.Archetype.Type.Abbreviation; } }

    public string Name       { get { return this.Archetype.Name + (this.Archetype.CannotUseUnskilled ? "*" : ""); } }
    public int    XP         { get; set; }

    public int    Score      
    { 
        get 
        { 
            // TODO: Why is checking/unchecking "Aff" in the AbilityGrid (DataGridView) not affecting the Score ?
            var result = AbilityXpCosts.ScoreForXP( this.XP, (this.HasAffinity ? AbilityXpCosts.BaseXpCostWithAffinity(this.BaseXPCost) : this.BaseXPCost) );
            return result;
        }
    }

    // TODO: Why is checking/unchecking "Pui" in the AbilityGrid (DataGridView) not affecting the displayed (AddToScore) value?
    [DisplayName(" ")]
    public string AddToScore { get { return this.HasPuissance ? ("+" + this.PuissantBonus.ToString()) : null; } }

    public string Specialty  { get; set; }

    public List<string> CommonSpecializations { get { return this.Archetype.CommonSpecializations; } }

    [Browsable(false)]
    public bool HasThisAbility { get; set; } = false;  // TODO: If false, display a blank value for XP / Score, rather than 0 / 0

    // TODO: Why is checking/unchecking "Aff" in the AbilityGrid (DataGridView) not affecting the Score ?
    [DisplayName("Aff")]
    public bool HasAffinity    { get; set; } = false;

    // TODO: Why is checking/unchecking "Pui" in the AbilityGrid (DataGridView) not affecting the displayed (AddToScore) value?
    [DisplayName("Pui")]
    public bool HasPuissance   { get; set; } = false;

    [Browsable(false)]
    public int  PuissantBonus  { get; private set; } = 2;
    // TODO:
    // Something more will be needed to represent how Languages
    // get a Puissant-like bonus from a related Language with a higher Score...

    public AbilityInstance ( AbilityArchetype ability, int xp = 0, string specialty = "", 
        bool hasAffinity = false, bool hasPuissance = false, int puissantBonus = 2)
    {
        decimal xpCost = hasAffinity ? AbilityXpCosts.BaseXpCostWithAffinity(ability.BaseXpCost) : ability.BaseXpCost;

        this.Archetype     = ability;
        this.XP            = xp;
        this.Specialty     = specialty ?? "";
        this.HasAffinity   = hasAffinity;
        this.HasPuissance  = hasPuissance;
        this.PuissantBonus = puissantBonus;
    }

    public override string ToString ()
    {
        string str = string.Format("{0} '{1}' XP={2} / Score={3} Specialty='{4}' {5} {6}",
            this.TypeAbbrev, this.Name, this.XP, this.Score, this.Specialty, 
            HasAffinity  ? "Affinity" : "", 
            HasPuissance ? "Puissant" : "");
        return str;
    }
}



////////////////////////////////////////////



/// <summary>
/// A utility class encoding rules which determine the XP needed to achieve a certain Score, 
/// given a Base Cost and perhaps modified by Affinity.
/// </summary>
public static class AbilityXpCosts
{
    // TODO:
    // Need to support such as Linguist (sets Base XP Cost to 4.0m),
    // and Flawless Formulaic Magic (which acts to double XP gain on Spell Mastery abilities)

    // TODO: 
    // Replace the brute-force-and-stupidity "loop until the Score is reached" logic 
    // with some proper means of calculating the arithmetic sequence 
    // (or table-lookup up to some N, if that proves to be an optimization).

    #region List methods
    //public static List<int> ListXpRequiredForScore(int maxScore, int baseXpCost)
    //{
    //    ValidateAbilityScoreValue( maxScore );
    //    ValidateBaseXpCostValue(baseXpCost);
    //
    //    var results = new List<int>();
    //    for (int score = 0; score <= maxScore; score++ )
    //    {
    //        int nn = XPRequiredForScore(score, baseXpCost);
    //        results.Add( nn );
    //    }
    //    return results;
    //}
    #endregion

    #region XP/Score methods
    public static int XPRequiredForScore( int score, decimal baseXpCost )
    {
        var unroundedValue = ArithmeticSequence(score) * baseXpCost;
        var xp = RoundAsInt( unroundedValue );  
        // Note:
        // Results differ in some cases from my previously printed table,
        // such as (10 * (2/3*5)) resulting in 33 rather than 34.
        // It seems like that table was rounding UP sometimes, for .3333 ?
        // As I recall, that printed table used numbers printed by 
        // a Perl script, so uncertain what rounding mode was used...
        return xp;
    }

    public static int ScoreForXP( int currentXp, decimal baseXpCost )
    {
        ValidateXpValue( currentXp );
        ValidateBaseXpCostValue(baseXpCost);

        int result = 0;
        for (int score = 0; score <= 99; score++ )
        {
            int nn = XPRequiredForScore( score, baseXpCost );
            if (nn > currentXp)
            {
                result = score - 1;
                break;
            }
            if (nn == currentXp)
            {
                result = score;
                break;
            }
            // Otherwise, if (nn < currentXp) keep on looking...
        }
        return result;
    }

    //public static int RemainingXpUntilAbilityScoreIncrease(int currentXp, int baseXpCost)
    //{
    //    // TODO: Test this properly...
    //    ValidateXpValue( currentXp );
    //    ValidateBaseXpCostValue(baseXpCost);
    //
    //    int score        = ScoreForXP(currentXp, baseXpCost);
    //    int required     = XPRequiredForScore(score,     baseXpCost);
    //    int requiredNext = XPRequiredForScore(score + 1, baseXpCost);
    //    int nn           = (requiredNext - currentXp);
    //
    //    return nn;
    //}
    #endregion

    public static decimal BaseXpCostWithAffinity(decimal baseXpCost)
    {
        ValidateBaseXpCostValue(baseXpCost);

        decimal adjustedForAffinity = baseXpCost * (2.0m / 3.0m);
        return adjustedForAffinity;
    }

    public static decimal ArithmeticSequence( int score )
    {
        ValidateAbilityScoreValue( score );

        decimal value = 0.0m;
        for ( int ii = 0; ii <= score; ii++ )
        {
            value += ii;
        }
        return value;
    }

    public static int RoundAsInt ( decimal value )
    {
        decimal rounded =  Math.Round(value, MidpointRounding.AwayFromZero);
        int result = (int)rounded;
        return result;
    }

    public static int CeilingAsInt( decimal value )
    {
        decimal roundedUp = Math.Ceiling(value);
        int result = (int)roundedUp;
        return result;
    }

    #region Argument Validation methods
    public static void ValidateAbilityScoreValue(int score)
    {
        if (score <  0) { throw new ArgumentException("score < 0");  }
        if (score > 99) { throw new ArgumentException("score > 99"); }
        // Otherwise, OK
    }

    public static void ValidateXpValue(int xp)
    {
        if (xp <    0) { throw new ArgumentException("xp < 0");    }
        if (xp > 9999) { throw new ArgumentException("xp > 9999"); }
        // Otherwise, OK
    }

    public static void ValidateBaseXpCostValue(decimal baseXpCost)
    {
        if (baseXpCost <=  0.0m) { throw new ArgumentException("baseXpCost <= 0.0"); }
        if (baseXpCost  > 60.0m) { throw new ArgumentException("baseXpCost > 60.0"); }
        // Otherwise, OK
    }
    #endregion
}
