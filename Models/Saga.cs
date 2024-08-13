using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models;

public class Saga
{
    #region Static Members
    public static Saga CurrentSaga { get; private set; }

    #endregion

    #region Public Members
    public string Name { get; set; }

    public Dictionary<string, Covenant>  Covenants  = new Dictionary<string, Covenant>();
    public Dictionary<string, Character> Characters = new Dictionary<string, Character>();
    // TODO: Hmmm, maybe dictionary for each entity type, but also a property returning IEnumerable<some_entity_type> ...
    #endregion

    #region Constructors
    static Saga()
    {
        // Putting this in the static ctor for now, it may belong elsewhere, once implementation is more complete.
        Saga.CurrentSaga = Saga.ExampleSaga;

        Covenant  exampleCovenant = new Covenant("Example Covenant");
        Covenant  otherCovenant   = new Covenant("Some Other Covenant");
        Covenant  rivalCovenant   = new Covenant("The Rivalrous Covenant");

        Character exampleGrog    = new Character("Willem the Handy", CharacterType.Grog, exampleCovenant);
        Character anotherGrog    = new Character("Torbjoern",        CharacterType.Grog, exampleCovenant);
        Character yetAnotherGrog = new Character("Elena the Pious",  CharacterType.Grog, exampleCovenant);

        Character exampleCompanion    = new Character("Adrian the Militant", CharacterType.Companion, exampleCovenant);
        Character anotherCompanion    = new Character("Theodric the Bishop", CharacterType.Companion, exampleCovenant);
        Character yetAnotherCompanion = new Character("Hans the Beekeeper",  CharacterType.Companion, exampleCovenant);

        Character exampleMagus = new Character("Grimgroth",        CharacterType.Magus, exampleCovenant);
        Character anotherMagus = new Character("Pluribus Ex Misc", CharacterType.Magus, exampleCovenant);
        Character rivalMagus   = new Character("Acerbacaepus",     CharacterType.Magus, exampleCovenant);

        // ...
    }

    public Saga(string name)
    {
        this.Name = name;
    }
    #endregion

    #region Public Methods
    public void AddCharacter(Character character)
    {
        this.Characters.Add(character.Tag, character);
    }

    public void AddCharactersRange(IEnumerable<Character> characters)
    {
        foreach (var ch in characters)
        {
            this.Characters.Add(ch.Tag, ch);
        }
    }

    public void AddCovenant(Covenant covenant)
    {
        this.Covenants.Add(covenant.Tag, covenant);
    }

    public void AddRangeCovenants(IEnumerable<Covenant> covenants)
    {
        foreach (var cov in covenants)
        {
            this.Covenants.Add(cov.Tag, cov);
        }
    }

    public override string ToString()
    {
        string str = string.Format("Saga: Name='{0}', #Cov={1}, #Char={2}", 
            this.Name, this.Covenants.Keys.Count, this.Characters.Keys.Count
        );
        return str;
    }
    #endregion

    #region Class-scoped Static Data (for testing, etc)
    public static Saga ExampleSaga       = new Saga("The Saga of Examples");
    public static Saga AnotherSaga       = new Saga("Another Saga");
    public static Saga OlderInactiveSaga = new Saga("Older Saga, now inactive");
    #endregion

}
