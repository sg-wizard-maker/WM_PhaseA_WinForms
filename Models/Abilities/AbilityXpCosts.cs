using System;

namespace WizardMakerPrototype.Models;

/// <summary>
/// A utility class encoding ArM5 RPG rules <br/>
/// (purposefully varying from the "Pen and Paper" Rules as Written, in aid of producing an "auditable" result)<br/>
/// which determine the XP needed to achieve a certain Score, 
/// given a Base Cost and perhaps modified by Affinity.
/// </summary>
public static class AbilityXpCosts
{
    // TODO:
    // Need to support such as
    //   Virtue:Linguist                 (sets Base XP Cost to 4.0m), and
    //   Virtue:Flawless Formulaic Magic (which acts to double XP gain on Spell Mastery abilities,
    //                                    and grants an initial 5 XP (unmodified) in each Spell Mastery)
    // The latter might be represented as having Base XP cost 2.5m, and granting an initial 2.5m ?

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

    #region Base mathematical methods
    public static decimal BaseXpCostWithAffinity(decimal baseXpCost)
    {
        ValidateBaseXpCostValue(baseXpCost);

        decimal adjustedForAffinity = baseXpCost * (2.0m / 3.0m);
        return adjustedForAffinity;
    }

    public static decimal ArithmeticSequence( int score )
    {
        // TODO: 
        // Replace the brute-force-and-stupidity "loop until the Score is reached" logic 
        // with some proper means of calculating the arithmetic sequence 
        // (or table-lookup up to some N, if that proves to be an optimization).
        // 
        // https://www.cuemath.com/arithmetic-sequence-formula/
        // https://www.mometrix.com/academy/writing-formulas-for-arithmetic-sequences/

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
    #endregion

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
