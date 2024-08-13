using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models;

public class Covenant
{
    private static int _MaxTagInteger = 0;
    private static int MaxTagInteger
    {
        get { _MaxTagInteger++;  return _MaxTagInteger; }
    }

    #region Public Members
    public string Tag { get; private set; }

    public string Name { get; set; }

    public Dictionary<string, Character> Characters = new Dictionary<string, Character>();
    #endregion

    #region Constructors
    public Covenant(string name) 
    {
        this.Tag = "cov_" + MaxTagInteger;
        this.Name = name;

        Saga.CurrentSaga.AddCovenant(this);
    }
    #endregion

    #region Public Methods
    public void AddCharacter(Character character)
    {
        this.Characters.Add(character.Tag, character);
    }

    public void AddRangeCharacters(IEnumerable<Character> characters)
    {
        foreach (var ch in characters)
        {
            this.Characters.Add(ch.Tag, ch);
        }
    }

    public override string ToString()
    {
        string str = string.Format("Covenant: Tag='{0}', Name='{1}', #Char={2}", 
            this.Tag, this.Name, this.Characters.Keys.Count
        );
        return str;
    }
    #endregion

}
