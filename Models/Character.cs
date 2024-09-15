using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models;

public class Character
{
    private static int _MaxTagInteger = 0;
    private static int MaxTagInteger
    {
        get { _MaxTagInteger++;  return _MaxTagInteger; }
    }

    #region Public Members
    public string Tag { get; private set; }

    public string        Name             { get; private set; }
    public CharacterType CharacterType    { get; private set; }
    public Covenant?     MemberOfCovenant { get; private set; }

    public List<AbilityInstance> Abilities { get; private set; } = new List<AbilityInstance>();
    // Will need additional members to represent access/denial to particular Abilities and groups thereof...
    #endregion

    #region Constructors
    public Character(string name, CharacterType characterType, Covenant? covenant = null)
    {
        this.Tag              = "char_" + MaxTagInteger;
        this.Name             = name;
        this.CharacterType    = characterType;
        this.MemberOfCovenant = covenant;
        if (covenant != null) { covenant.AddCharacter(this); }

        Saga.CurrentSaga.AddCharacter(this);
    }
    #endregion

    #region Public Methods
    public List<AbilityInstance> AbilitiesOfType(AbilityType type) 
    {
        var query = (from a in this.Abilities
                     where a.Archetype.AbilityType == type
                     select a);
        var result = query.ToList();
        return result;
    }

    //public List<AbilityInstance> AbilitiesOfCategory(AbilityCategory cat)
    //{
    //    var query = (from a in this.Abilities
    //                 where a.Archetype.Category == cat
    //                 select a);
    //    var result = query.ToList();
    //    return result;
    //}

    public override string ToString()
    {
        string str = string.Format("Character: Tag='{0}', Name='{1}', Type={2}, Cov='{3}'", 
            this.Tag, this.Name, this.CharacterType, this.MemberOfCovenant?.Name
        );
        return str;
    }
    #endregion
}

public enum CharacterType
{
    Unknown         = 0,
    Grog            = 1,
    Companion       = 2,
    MythicCompanion = 3,
    Magus           = 4,
}
