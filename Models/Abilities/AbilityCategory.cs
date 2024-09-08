using System;
using System.Collections.Generic;

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
public class AbilityCategory
{
    // Note that many AbilityCategory map directly to
    // an AbilityWildcardGroup (excepting Law: <specify>, and Theology: <specify>),
    // however they serve different purposes.

    // TODO: Once serialization is integrated, this sort of static data will be loaded from JSON...
    public static List<String> Categories = new List<string>();

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
