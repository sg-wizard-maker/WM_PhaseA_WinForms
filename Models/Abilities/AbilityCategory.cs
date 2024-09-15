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
    #region Public members
    public string Name { get; }
    #endregion

    #region Constructors
    public AbilityCategory(string name)
    {
        this.Name = name;
    }

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
    #endregion

    #region Static data
    // TODO: Once serialization is integrated, this sort of static data will be loaded from JSON...
    public static List<AbilityCategory> Categories = new List<AbilityCategory>();

    public static AbilityCategory Martial      = new AbilityCategory("Martial");
    public static AbilityCategory Physical     = new AbilityCategory("Physical");
    public static AbilityCategory Social       = new AbilityCategory("Social");
    public static AbilityCategory Languages    = new AbilityCategory("Language");
    public static AbilityCategory Crafts       = new AbilityCategory("Crafts");
    public static AbilityCategory Professions  = new AbilityCategory("Professions");
    public static AbilityCategory Academic     = new AbilityCategory("Academic");
    public static AbilityCategory Supernatural = new AbilityCategory("Supernatural");
    public static AbilityCategory Arcane       = new AbilityCategory("Arcane");
    public static AbilityCategory RealmLores   = new AbilityCategory("Realm Lores");
    public static AbilityCategory AreaLores    = new AbilityCategory("Area Lores");
    public static AbilityCategory OrgLores     = new AbilityCategory("Org Lores");
    #endregion

}
