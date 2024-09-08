using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Controls;

public class AbilitiesFlowLayoutPanel : FlowLayoutPanel
{
    #region Constants related to DataGridView columns
    const int COL_AbilityCategory = 0;
    const int COL_AbilityType     = 1;
    const int COL_AbilityName     = 2;
    const int COL_XP              = 3;
    const int COL_Score           = 4;
    const int COL_Bonus           = 5;
    const int COL_Specialty       = 6;
    const int COL_HasAffinity     = 7;
    const int COL_HasPuissant     = 8;

    const int NUM_COLUMNS = 9;
    #endregion

    public enum AbilityGroupingsDisplayMode
    {
        SingleList,
        ByAbilityType,
        ByAbilityCategory
    };
    public AbilityGroupingsDisplayMode GroupingDisplayMode { get; private set; }

    #region Constructors
    public AbilitiesFlowLayoutPanel()
    {
        this.Dock          = DockStyle.Fill;
        this.FlowDirection = FlowDirection.TopDown;

        //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // No benefit seen when applied only here

        this.BackColor = Color.Yellow;  // Useful for debug, not needed otherwise

        // Note:
        // With FlowDirection.TopDown, starting with an empty Controls collection,
        // the first control is placed upon the top-left,
        // and the first column goes downwards.
        // 
        // After the available height is filled, 
        // each subsequent column is rightwards of the previous,
        // and each column grows downwards.
        // 
        // Supposedly, on systems configured with a Locale with Right-to-Left text flow,
        // this would start on the top-right and subsequent columns would be leftwards of the previous.
        // 
        // Supposedly, on systems configured with a Locale with Top-to-Down text flow,
        // some analogous change in "flow" would be observed.

        // Note:
        // Since the contents of this panel are of variable number,
        // they are added in at a later time, either by a DoDataSetupFor*() method,
        // or some similar method (not yet implemented) which does
        // such setup based upon the abilities data in a particular Character.
    }
    #endregion

    /// <summary>
    /// Sanity checks that all of the contained DataGridView controls 
    /// have the same number of columns.
    /// If a mismatch is found, throws an exception regarding the bad setup.
    /// If the number found does not match the (currently hard-coded) number expected, throws an exception regarding this.
    /// Otherwise, returns that number.
    /// </summary>
    /// <returns>The number of columns in common across all of the contained DataGridView controls</returns>
    /// <exception cref="Exception"></exception>
    public int SanityCheckNumberOfColumnsInEachGrid(IEnumerable<DataGridView> dataGridsInThisPanel)
    {
        if (dataGridsInThisPanel.Count() == 0)
        {
            var label = new Label();
            label.Font    = new Font("Consolas", 16, FontStyle.Bold);
            label.Text    = "Zero groupings of Abilities to display!";
            label.Width   = 600;
            label.Height += 2;  // Fudge factor, to avoid vertical cut-off
            this.Controls.Add(label);
            return 0;
        }
        // Alternate approach:
        // { throw new Exception("Got zero Grids for FlowLayoutPanel"); }

        var minColumns = (from gg in dataGridsInThisPanel select gg.ColumnCount).Min();
        var maxColumns = (from gg in dataGridsInThisPanel select gg.ColumnCount).Max();
        if (minColumns != maxColumns) { throw new Exception("Got panel with AbilityGridControls with divergent column counts!"); }
        int numColumns = minColumns;
        if (numColumns != NUM_COLUMNS) { throw new Exception("Got panel with AblityGridControls with unexpected column count!"); }
        return numColumns;
    }

    #region Methods to adjust Width and Height of contained Grids
    public int AdjustEachContainedGridToIndividualNeededWidth(IEnumerable<DataGridView> dataGridsInThisPanel)
    {
        // Note:
        // This gives each grid the width it needs individually,
        // but the overall need seems like
        // 
        // Make all grids have:
        //    - the same width         (equals widest needed for any grid), and
        //    - the same column widths (each equals widest needed for that column for any grid)
        // 
        // Replaced this with logic in DoColumnsSetup() using commonMaxWidthsForEachColumn
        int widestSumOfColumnWidths = 0;
        foreach (AbilityGrid gg in dataGridsInThisPanel)
        {
            int sumOfNeededColumnWidths = 0;
            for (int ii = 0; ii < gg.ColumnCount; ii++)
            {
                var thisColumn      = gg.Columns[ii];
                int thisColumnWidth = thisColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                if (thisColumn.Visible)
                {
                    // Don't count not-Visible columns for this
                    sumOfNeededColumnWidths += thisColumnWidth;
                }
                if (widestSumOfColumnWidths < sumOfNeededColumnWidths) { widestSumOfColumnWidths = sumOfNeededColumnWidths; }
            }
            gg.Width = sumOfNeededColumnWidths;
        }
        return widestSumOfColumnWidths;
    }

    public void AdjustEachContainedGridToIndividualNeededHeight(IEnumerable<DataGridView> dataGridsInThisPanel)
    {
        foreach (AbilityGrid gg in dataGridsInThisPanel)
        {
            int resultingHeight = gg.ResizeGridToNeededHeight();
            gg.Parent.Height = gg.Parent.PreferredSize.Height;  // Adjust the AbilityGroupingPanel
        }
    }
    #endregion

    #region Methods to do setup for (columns, etc) in contained Grids
    public void DoColumnsSetup(IEnumerable<DataGridView> dataGridsInThisPanel)
    {
        // TODO:
        // This panel control should have a notion of what kind of "grouping" is used (by Type, by Category, single list, ...)
        // and do column setup to correspond -- probably meaning
        // - hide AbilityType for 'by Type'
        // - hide AbilityCategory for 'by Category'
        // 
        // This notion is now contained in:
        //     this.AbilityGroupingsDisplayMode

        int numColumns = this.SanityCheckNumberOfColumnsInEachGrid(dataGridsInThisPanel);

        foreach (AbilityGrid gg in dataGridsInThisPanel)
        {
            // TODO: 
            // Want to supporting by various columns, but the basic DataBinding does not do that.
            // Setup of something more is needed...
            // 
            //gg.Columns[COL_AbilityName].SortMode = DataGridViewColumnSortMode.Automatic;

            #region Setup particular columns to be not-Visible
            if (this.GroupingDisplayMode == AbilityGroupingsDisplayMode.SingleList) 
            {
                // No columns to hide, for this mode. Note that total width will be somewhat greater.
            }
            if (this.GroupingDisplayMode == AbilityGroupingsDisplayMode.ByAbilityType) 
            {
                gg.Columns[COL_AbilityType].Visible = false;
            }
            if (this.GroupingDisplayMode == AbilityGroupingsDisplayMode.ByAbilityCategory) 
            {
                gg.Columns[COL_AbilityCategory].Visible = false;
            }
            #endregion

            #region Setup particular columns to be resizeable
            gg.Columns[COL_AbilityName].Resizable = DataGridViewTriState.True;  // Ability Name
            gg.Columns[COL_Specialty  ].Resizable = DataGridViewTriState.True;  // Specialty
            #endregion

            #region Manually specify which columns should be aligned to the right:
            gg.Columns[COL_XP   ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gg.Columns[COL_Score].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            #endregion

            #region Manually setup ToolTipText for columns headers
            // TODO:
            // Look into a custom ToolTip class, to allow formatting.
            // A starting point might be:
            //     https://andrusdevelopment.blogspot.com/2007/10/implementing-custom-tooltip-in-c.html
            gg.Columns[COL_AbilityCategory].ToolTipText = 
                "Category:\nCategory of the Ability, such as:\n" +
                "- Martial Abilities\n" +
                "- Physical Abilities\n" +
                "- Social Abilities\n" +
                "- Languages\n" +
                "- Crafts\n" +
                "- Professions\n" +
                "- Academic Abilities\n" +
                "- Supernatural Abilities\n" +
                "- Arcane Abilities\n" +
                "- Supernatural Realms\n" +
                "- Area Lores\n" +
                "- Organization Lores\n";

            gg.Columns[COL_AbilityType].ToolTipText = 
                "Ability Type:\nType of the Ability per (ArM5 36-40), such as:\n" +
                "- [Gen]  General\n"+ 
                "- [GC]   General+Child\n" +
                "- [Mar]  Martial\n" +
                "- [Acad] Academic\n" +
                "- [Arc]  Arcane\n" +
                "- [Sup]  Supernatural\n" +
                "\n" +
                "- [Sec]  Secret Ability (requires special background)";
            gg.Columns[COL_AbilityName].ToolTipText = "Name:\nName of the Ability\nAn asterix (*) indicates the Ability cannot be used unskilled.";
            gg.Columns[COL_XP         ].ToolTipText = "XP:\nExperience Points (XP) in the Ability";
            gg.Columns[COL_Score      ].ToolTipText = "Score:\nScore in the Ability based upon XP";
            gg.Columns[COL_Bonus      ].ToolTipText = "Bonus:\nBonus to Score from Puissant";
            gg.Columns[COL_Specialty  ].ToolTipText = "Specialty:\nSpecialty for the Ability (+1 effective Score)";
            gg.Columns[COL_HasAffinity].ToolTipText = "Aff:\nDoes this Ability have Affinity?";
            gg.Columns[COL_HasPuissant].ToolTipText = "Pui:\nDoes this Ability have Puissance?";
            #endregion
        }

        #region Determine: for each column, across all grids, the max width needed for that column, and stash it
        var commonMaxWidthsForEachColumn = new List<int>();
        int sumOfVisibleColumnWidths = 0;
        for (int ii = 0; ii < numColumns; ii++)
        {
            int maxWidthFoundThusFar = 0;
            bool columnIsVisible = false;
            foreach (AbilityGrid gg in dataGridsInThisPanel)
            {
                var thisColumn = gg.Columns[ii];
                int neededWidth = thisColumn.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
                if (neededWidth > maxWidthFoundThusFar) 
                {
                    maxWidthFoundThusFar = neededWidth; 
                }
                columnIsVisible = thisColumn.Visible;
            }
            commonMaxWidthsForEachColumn.Add(maxWidthFoundThusFar);
            if (columnIsVisible) { sumOfVisibleColumnWidths += maxWidthFoundThusFar; }
        }
        #endregion

        #region Set: for each column, across all grids, the column width to the previously determined common maximum
        for (int ii = 0; ii < numColumns; ii++)
        {
            foreach (AbilityGrid gg in dataGridsInThisPanel)
            {
                var thisColumnInThisGrid = gg.Columns[ii];
                thisColumnInThisGrid.Width = commonMaxWidthsForEachColumn[ii];
            }
        }
        foreach (AbilityGrid gg in dataGridsInThisPanel)
        {
            int fudge = 0;
            if (this.GroupingDisplayMode != AbilityGroupingsDisplayMode.ByAbilityType) 
            {
                // COL_AbilityType is hidden
                gg.Columns[COL_AbilityType    ].Width -= 20;  // Tighten it up somewhat
                fudge -= 20;
            }
            if (this.GroupingDisplayMode != AbilityGroupingsDisplayMode.ByAbilityCategory) 
            {
                // COL_AbilityCategory is hidden
                gg.Columns[COL_AbilityCategory].Width -= 10;  // Tighten it up somewhat
                fudge -= 10;
            }
            //gg.Columns[COL_AbilityName    ].Width +=  0;  // 
            gg.Columns[COL_XP               ].Width -= 15;  // Tighten it up somewhat
            gg.Columns[COL_Score            ].Width -= 25;  // Tighten it up somewhat
            //gg.Columns[COL_Bonus          ].Width +=  0;  // 
            //gg.Columns[COL_Specialty      ].Width +=  0;  // 
            //gg.Columns[COL_HasAffinity    ].Width +=  0;  // 
            //gg.Columns[COL_HasPuissant    ].Width +=  0;  // 
            fudge -= 15;
            fudge -= 25;

            gg.Width        = sumOfVisibleColumnWidths + fudge;  // Fudge factor, varies by hidden columns (without: grid less wide than containing panel, due to net-negative adjustments above)
            gg.Parent.Width = sumOfVisibleColumnWidths + fudge;  // Fudge factor, varies by hidden columns (without: flow column too wide)
        }
        #endregion
    }
    #endregion

    #region Methods to insert data for the contained Grids
    /// <summary>
    /// Setup data for N grids in the flowlayoutpanel (N == number of Ability Categories), 
    /// with Abilities of one AbilityCategory per grid.<br/>
    /// Note: ONE of the DoDataSetupForAbilityXXX() methods can be executed; they will interfere with one another!
    /// </summary>
    public void DoDataSetupForAbilityCategories()
    {
        this.GroupingDisplayMode = AbilityGroupingsDisplayMode.ByAbilityCategory;
        var bindingListsByAbilityCategory = new Dictionary<string, BindingList<AbilityInstance>>()
        {
            { AbilityCategory.Martial,      new BindingList<AbilityInstance>() },
            { AbilityCategory.Physical,     new BindingList<AbilityInstance>() },
            { AbilityCategory.Social,       new BindingList<AbilityInstance>() },
            { AbilityCategory.Languages,    new BindingList<AbilityInstance>() },
            { AbilityCategory.Crafts,       new BindingList<AbilityInstance>() },
            { AbilityCategory.Professions,  new BindingList<AbilityInstance>() },
            { AbilityCategory.Academic,     new BindingList<AbilityInstance>() },
            { AbilityCategory.Supernatural, new BindingList<AbilityInstance>() },
            { AbilityCategory.Arcane,       new BindingList<AbilityInstance>() },
            { AbilityCategory.RealmLores,   new BindingList<AbilityInstance>() },
            { AbilityCategory.AreaLores,    new BindingList<AbilityInstance>() },
            { AbilityCategory.OrgLores,     new BindingList<AbilityInstance>() },
        };
        // Note: 
        // For a BindingList that is sortable, consider:
        // https://martinwilley.com/net/code/forms/sortablebindinglist.html
        // https://stackoverflow.com/questions/43613305/sorting-with-sortablebindinglist-and-datagridview-c

        int numCategories = bindingListsByAbilityCategory.Keys.Count;

        var abilityGroupingPanelsList = new List<AbilityLabelAndGridPanel>();
        foreach (var grouping in bindingListsByAbilityCategory)
        {
            var groupingName = grouping.Key;
            var panel = new AbilityLabelAndGridPanel(groupingName);
            abilityGroupingPanelsList.Add(panel);
            this.Controls.Add(panel);
        }

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

        // TODO:
        // Need to add from AbilityArchetypeWildcard.AllAbilityArchetypeWildcards as well as AbilityArchetype.AllCommonAbilities ...
        // ...Seems to be working thus far, need more testing and some review to deal with a couple of awkward bits around .Name

        List<AbilityArchetypeBase> combinedList = new List<AbilityArchetypeBase>();
        combinedList.AddRange(AbilityArchetype.AllCommonAbilities);
        combinedList.AddRange(AbilityArchetypeWildcard.AllAbilityArchetypeWildcards);
        //foreach (var arch in AbilityArchetype.AllCommonAbilities)
        foreach (var arch in combinedList)
        {
            int randomXP = Utility.RandomInteger(rand, 0, 150);
            string randomsSpecialty = randomSpecialties[Utility.RandomInteger(rand, 0, numSpecialties)];
            bool hasPuissant = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            bool hasAffinity = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            var abilityInstance = new AbilityInstance( arch, randomXP, randomsSpecialty, hasPuissant, hasAffinity);

            // For layout debug purposes:
            if (arch == AbilityArchetype.AllCommonAbilities.First() )
            {
                abilityInstance = new AbilityInstance(arch, 9999, "widest specialty name", true, true, 99);
            }

            bindingListsByAbilityCategory[abilityInstance.Archetype.Category].Add(abilityInstance);
        }
        int jj = 0;
        foreach (var grouping in AbilityCategory.Categories)
        {
            var bindingList = bindingListsByAbilityCategory[grouping];
            var theGroupingPanel = abilityGroupingPanelsList[jj];
            theGroupingPanel.TheGrid.BindingContext = new BindingContext();  // TODO: Find out why this is needed when AbilitiesFlowLayoutPanel is contained in a TabPage, but not when it is contained in a Form directly...
            theGroupingPanel.TheGrid.DataSource = bindingList;
            jj++;
        }
    }

    /// <summary>
    /// Setup data for N grids in the flowlayoutpanel (N == number of Ability Types), 
    /// with Abilities of one AbilityType per grid.<br/>
    /// Note: ONE of the DoDataSetupForAbilityXXX() methods can be executed; they will interfere with one another!
    /// </summary>
    public void DoDataSetupForAbilityTypes()
    {
        this.GroupingDisplayMode = AbilityGroupingsDisplayMode.ByAbilityType;
        var bindingListsByAbilityType = new Dictionary<string, BindingList<AbilityInstance>>()
        {
            { AbilityType.GenChild.Name,     new BindingList<AbilityInstance>() },
            { AbilityType.General.Name,      new BindingList<AbilityInstance>() },
            { AbilityType.Martial.Name,      new BindingList<AbilityInstance>() },
            { AbilityType.Academic.Name,     new BindingList<AbilityInstance>() },
            { AbilityType.Arcane.Name,       new BindingList<AbilityInstance>() },
            { AbilityType.Supernatural.Name, new BindingList<AbilityInstance>() },
            { AbilityType.Secret.Name,       new BindingList<AbilityInstance>() },  // Not specified in (ArM5 core book), but useful
        };
        int numAbilityTypes = bindingListsByAbilityType.Keys.Count;

        var abilityGroupingPanelsList = new List<AbilityLabelAndGridPanel>();
        foreach (var grouping in bindingListsByAbilityType)
        {
            var groupingName = grouping.Key;
            var panel = new AbilityLabelAndGridPanel(groupingName);
            abilityGroupingPanelsList.Add(panel);
            this.Controls.Add(panel);
        }

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

        foreach (var arch in AbilityArchetype.AllCommonAbilities)
        {
            int randomXP = Utility.RandomInteger(rand, 0, 150);
            string randomsSpecialty = randomSpecialties[Utility.RandomInteger(rand, 0, numSpecialties)];
            bool hasPuissant = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            bool hasAffinity = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            var abilityInstance = new AbilityInstance( arch, randomXP, randomsSpecialty, hasPuissant, hasAffinity);

            // For layout debug purposes:
            if (arch == AbilityArchetype.AllCommonAbilities.First() )
            {
                abilityInstance = new AbilityInstance(arch, 9999, "widest specialty name", true, true, 99);
            }

            bindingListsByAbilityType[abilityInstance.Archetype.Type.Name].Add(abilityInstance);
        }
        int jj = 0;
        foreach (var grouping in AbilityType.Types)
        {
            var bindingList = bindingListsByAbilityType[grouping.Name];
            var theGroupingPanel = abilityGroupingPanelsList[jj];
            theGroupingPanel.TheGrid.BindingContext = new BindingContext();  // TODO: Find out why this is needed when AbilitiesFlowLayoutPanel is contained in a TabPage, but not when it is contained in a Form directly...
            theGroupingPanel.TheGrid.DataSource = bindingList;
            jj++;
        }
    }

    public void DoDataSetupForOneGiantList()
    {
        // TODO: 
        // Consider what is needed, for this mode to 
        // - Determine how many entries can fit, in the vertical space
        //   for one Grid (possibly with AbilityGroupingPanel header)
        // - Put that many into a first grid,
        //   and the rest into one or more grids
        // 
        // Hmmm, that sounds like some of it may belong in the AbilityGroupingPanel level...
        // 
        this.GroupingDisplayMode = AbilityGroupingsDisplayMode.SingleList;
        var bindingListsByKey = new Dictionary<string, BindingList<AbilityInstance>>()
        {
            { "Abilities", new BindingList<AbilityInstance>() }
        };
        int numAbilityLists = bindingListsByKey.Keys.Count;

        var abilityGroupingPanelsList = new List<AbilityLabelAndGridPanel>();
        foreach (var grouping in bindingListsByKey)
        {
            var groupingName = grouping.Key;
            var panel = new AbilityLabelAndGridPanel(groupingName);
            abilityGroupingPanelsList.Add(panel);
            this.Controls.Add(panel);
        }

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

        List<AbilityArchetype> AllAbilitiesSortedByAlpha =
        (
            from aa in AbilityArchetype.AllCommonAbilities
            select aa
        )
            .OrderBy(xx => xx.Name)
            .ToList();
        foreach (var arch in AllAbilitiesSortedByAlpha)
        {
            int randomXP = Utility.RandomInteger(rand, 0, 150);
            string randomsSpecialty = randomSpecialties[Utility.RandomInteger(rand, 0, numSpecialties)];
            bool hasPuissant = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            bool hasAffinity = Utility.RandomInteger(rand, 0, 7) == 0;  // 1 in 8
            var abilityInstance = new AbilityInstance( arch, randomXP, randomsSpecialty, hasPuissant, hasAffinity);

            // For layout debug purposes:
            if (arch == AllAbilitiesSortedByAlpha.First() )
            {
                abilityInstance = new AbilityInstance(arch, 9999, "widest specialty name", true, true, 99);
            }

            bindingListsByKey["Abilities"].Add(abilityInstance);
        }
        int jj = 0;
        foreach (var cat in AbilityCategory.Categories)  // TODO: Loop is needless for single list.  Planning to split into one per column, though...
        {
            var bindingList = bindingListsByKey["Abilities"];
            var theGroupingPanel = abilityGroupingPanelsList[0];
            theGroupingPanel.TheGrid.BindingContext = new BindingContext();  // TODO: Find out why this is needed when AbilitiesFlowLayoutPanel is contained in a TabPage, but not when it is contained in a Form directly...
            theGroupingPanel.TheGrid.DataSource = bindingList;
            jj++;
        }
    }
    #endregion

} // class
