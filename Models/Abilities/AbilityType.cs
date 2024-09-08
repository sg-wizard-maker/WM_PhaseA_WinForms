using System.Collections.Generic;

namespace WizardMakerPrototype.Models;

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
    // TODO: Once serialization is integrated, this sort of static data will be loaded from JSON...
    public static List<AbilityType> Types = new List<AbilityType>();

    public string Abbreviation { get; private set; }
    public string Name         { get; private set; }
    // TODO:
    // Need some additional properties defining the 
    // requirements/associations which each AbilityType has
    // with certain Virtues/Flaws, age categories, etc...
    // 
    // (There are Virtues and Flaws which
    //   - grant access to certain AbilityTypes or specific Abilities,
    //   - REMOVE access to certain AbilityTypes or specific Abilities
    // and other considerations.)

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
    public static AbilityType Secret       = new AbilityType("Sec",  "Secret"        );  // Not specified in (ArM5 core), but useful for Saga-specific Abilities which are private to certain groups, and the like

    static AbilityType()
    {
        Types.Add( GenChild     );
        Types.Add( General      );
        Types.Add( Martial      );
        Types.Add( Academic     );
        Types.Add( Arcane       );
        Types.Add( Supernatural );
        Types.Add( Secret       );  // Not specified in (ArM5 core), but useful
    }

}
