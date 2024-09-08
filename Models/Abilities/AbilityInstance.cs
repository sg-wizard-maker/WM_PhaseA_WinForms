using System.Collections.Generic;
using System.ComponentModel;  // for [Browsable] attribute

namespace WizardMakerPrototype.Models;

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

    #region Public Properties

    // TODO:
    // AbilityInstance needs to be able to use (AbilityArchetype, AbilityArchetypeWildcard)
    // This presumably means that AbilityInstance.Archetype needs to be of type AbilityArchetypeBase ...

    [Browsable(false)]
    //public AbilityArchetype Archetype { get; private set; }
    public AbilityArchetypeBase Archetype { get; private set; }

    [Browsable(false)]
    public decimal     BaseXPCost { get { return Archetype.BaseXpCost; } }

    [DisplayName("Category")]
    public string Category   { get { return this.Archetype.Category;          } }

    [Browsable(false)]
    public string Type       { get { return this.Archetype.Type.Name;         } }

    [DisplayName("Type")]
    public string TypeAbbrev { get { return this.Archetype.Type.Abbreviation; } }

    public string Name       
    { 
        // Hmmm, this is a bit awkward.  Can likely improve on it, by massaging the two classes involved...
        get 
        {
            var arch = (this.Archetype as AbilityArchetype);
            var wc   = (this.Archetype as AbilityArchetypeWildcard);

            if (arch is not null)
            {
                string str = arch.Name + (arch.CannotUseUnskilled ? "*" : "");
                return str;
            }
            if (wc is not null)
            {
                string str = wc.Name + (wc.CannotUseUnskilled ? "*" : "");
                return str;
            }
            throw new System.Exception("Impossible: AbilityInstance got strange archetype, neither AbilityArchetype nor AbilityArchetypeWildcard");

            //return this.Archetype.Name + (this.Archetype.CannotUseUnskilled ? "*" : ""); 
        } 
    }
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
    #endregion

    #region Constructors
    //public AbilityInstance ( AbilityArchetype ability, int xp = 0, string specialty = "", 
    //    bool hasAffinity = false, bool hasPuissance = false, int puissantBonus = 2)
    public AbilityInstance ( AbilityArchetypeBase ability, int xp = 0, string specialty = "", 
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
    #endregion

    public override string ToString ()
    {
        string str = string.Format("{0} '{1}' XP={2} / Score={3} Specialty='{4}' {5} {6}",
            this.TypeAbbrev, this.Name, this.XP, this.Score, this.Specialty, 
            HasAffinity  ? "Affinity" : "", 
            HasPuissance ? "Puissant" : "");
        return str;
    }
}
