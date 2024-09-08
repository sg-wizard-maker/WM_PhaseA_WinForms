using System;
using System.Collections.Generic;

namespace WizardMakerPrototype.Models;

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
/// - and with certain common Specializations.<br/>
/// <br/>
/// Note that some Abilities are members of a "wildcard" grouping 
/// of possibly-many Abilities, such as "Craft: (specify)".<br/>
/// For that kind of Ability, the class <see cref="AbilityArchetypeWildcard">AbilityArchetypeWildcard</see> <br/>
/// is analogous to <see cref="AbilityArchetype">AbilityArchetype</see>, serving the same purpose.
/// </summary>
public class AbilityArchetype : AbilityArchetypeBase, IOrdinaryArchetypeOrWildcardGroup
{
    #region Properties possibly useful for LINQ queries
    public bool IsSingle   { get { return true;  } }
    public bool IsGroup    { get { return false; } }
    public bool IsWildcard { get { return false; } }
    #endregion

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

    // TODO:
    // Change the static data setup to use AbilityArchetypeWildcard + AbilityWildCardGroup,
    // for (Living Languages, Dead Languages, Craft, Profession, Area Lore, Org Lore) etc.
    //
    // This, coupled with altering the data setup in AbilitiesTabPage.cs ...

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
