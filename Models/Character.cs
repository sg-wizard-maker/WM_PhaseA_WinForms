using System;
using System.Collections.Generic;
using System.Linq;

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
    public Covenant      MemberOfCovenant { get; private set; }  // Likely nullable in the near future... Annoying warnings about that...

    public List<AbilityInstance> Abilities { get; private set; } = new List<AbilityInstance>();
    // Will need additional members to represent access/denial to particular Abilities and groups thereof...
    #endregion

    #region Constructors
    public Character(string name, CharacterType characterType, Covenant covenant = null)
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

    #region Methods returning info about AbilityType / AbilityCategory / AbilityWildcardGroup
    public List<AbilityType> ContainedAbilityTypes()
    {
        var query = (from a in this.Abilities
                     select a.Archetype.AbilityType
                     ).Distinct();
        var result = query.ToList();
        return result;
    }

    public List<AbilityCategory> ContainedAbilityCategories()
    {
        var query = (from a in this.Abilities
                     select a.Archetype.AbilityCategory
                     ).Distinct();
        var result = query.ToList();
        return result;
    }

    public List<AbilityWildcardGroup> ContainedAbilityWildcardGroups()
    {
        var query = (from a in this.Abilities
                     select (a.Archetype as AbilityArchetypeWildcard).IsWithinGroup
                     ).Distinct();
        var result = query.ToList();
        return result;
    }
    #endregion

    #region Methods returning subsets of AbilityInstance
    public List<AbilityInstance> AbilitiesOfType(AbilityType type) 
    {
        if (type     is null) { throw new ArgumentException("AbilitiesOfType: Got null AbilityType"); }

        var query = (from a in this.Abilities
                     where a.Archetype.AbilityType == type
                     select a);
        var result = query.ToList();
        return result;
    }

    public List<AbilityInstance> AbilitiesOfCategory(AbilityCategory category)
    {
        if (category is null) { throw new ArgumentException("AbilitiesOfCategory: Got null AbilityCategory"); }

        var query = (from a in this.Abilities
                     where a.Archetype.AbilityCategory == category
                     select a);
        var result = query.ToList();
        return result;
    }

    /// <summary>
    /// Given an AbilityType and an AbilityCategory (both must not be null), returns a collection of AbilityInstance which match.
    /// </summary>
    /// <param name="type">The AbilityType to match, must not be null.</param>
    /// <param name="category">The AbilityCategory to match, must not be null.</param>
    /// <returns>List of AbilityInstance matching both criteria</returns>
    /// <exception cref="ArgumentException"></exception>
    public List<AbilityInstance> AbilitiesOfTypeAndCategory(AbilityType type, AbilityCategory category)
    {
        if (type is null && category is null) { throw new ArgumentException("AbilitiesOfTypeAndCategory: Got null AbilityType AND null AbilityCategory"); }
        if (type     is null) { throw new ArgumentException("AbilitiesOfTypeAndCategory: Got null AbilityType"); }
        if (category is null) { throw new ArgumentException("AbilitiesOfTypeAndCategory: Got null AbilityCategory"); }

        var query = (from a in this.Abilities
                     where
                            a.Archetype.AbilityType     == type
                         && a.Archetype.AbilityCategory == category
                     select a);
        var result = query.ToList();
        return result;
    }

    public List<AbilityInstance> AbilitiesOfNonWildcardKind()
    {
        var query = (from a in this.Abilities
                     where a.Archetype is AbilityArchetype  // could also test a.Archetype.IsWildcard
                     select a);
        var result = query.ToList();
        return result;
    }

    public List<AbilityInstance> AbilitiesOfWildcardKind()
    {
        var query = (from a in this.Abilities
                     where a.Archetype is AbilityArchetypeWildcard  // could also test a.Archetype.IsWildcard
                     select a);
        var result = query.ToList();
        return result;
    }
    #endregion

    #region Methods for sanity-check of the Abilities list
    public bool AbilitiesSanityCheckNoDuplicateArchetypes()
    {
        var query = (from a in this.Abilities
                     select a.Archetype);
        var queryDistinct = query.Distinct();
        var listArchetypes = query.ToList();
        var listArchetypesDistinct = queryDistinct.ToList();
        bool result = (listArchetypes.Count == listArchetypesDistinct.Count);
        return result;
    }
    #endregion

    public override string ToString()
    {
        string str = string.Format("Character: Tag='{0}', Name='{1}', Type={2}, Cov='{3}'", 
            this.Tag, this.Name, this.CharacterType, this.MemberOfCovenant?.Name
        );
        return str;
    }
    #endregion

    #region Methods to setup static data
    public void DEBUG_DataSetupOfAbilityInstances()
    {
        Random rand = new Random();
        string[] randomSpecialties = new string[] 
        {
            "---",
            "daytime",
            "at night",
            "when on fire",
            "routine matters",
            "dangerous situations",
            "France and the French",
        };
        int numSpecialties = randomSpecialties.Count() - 1;

        List<IAbilityArchetype> combinedList = new List<IAbilityArchetype>();
        combinedList.AddRange(AbilityArchetype.AllCommonAbilities);
        combinedList.AddRange(AbilityArchetypeWildcard.AllAbilityArchetypeWildcards);
        foreach (var arch in combinedList)
        {
            int randomXP = Utility.RandomInteger(rand, 0, 150);
            string randomSpecialty = randomSpecialties[Utility.RandomInteger(rand, 0, numSpecialties)];
            bool hasPuissant = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            bool hasAffinity = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            var abilityInstance = new AbilityInstance( arch, randomXP, randomSpecialty, hasPuissant, hasAffinity);

            // For layout debug purposes:
            if (arch == AbilityArchetype.AllCommonAbilities.First() )
            {
                abilityInstance = new AbilityInstance(arch, 9999, "widest specialty name", true, true, 99);
            }

            this.Abilities.Add(abilityInstance);
        }
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
