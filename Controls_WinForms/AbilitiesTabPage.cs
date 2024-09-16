using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using WizardMakerPrototype.Models;
using System.Threading;
using WizardMakerPrototype.Controls_WinForms;

namespace WizardMakerPrototype.Controls;

public class AbilitiesTabPage : TabPage
{
    #region Public members
    public Character TheCharacter
    {
        get
        {
            var parent = ControlExtensions.FindTopmostForm(this);
            var form   = parent as FormForArM5Character;
            if (form is null) { throw new Exception("Strange, got topmost Form that was not FormForArM5Character"); }
            var result = form.TheCharacter;
            return result;
        }
    }

    public SplitContainer           TheSplitContainer   { get; private set; }
    //public SplitterPanel            SplitPanel1         { get; private set; }
    //public SplitterPanel            SplitPanel2         { get; private set; }

    public Label LabelAbilityType          { get; private set; }
    public Label LabelAbilityCategory      { get; private set; }
    public Label LabelAbilityWildcardGroup { get; private set; }
    public Label LabelAbility              { get; private set; }
    public Label LabelWildcardAbility      { get; private set; }

    public ComboBox Combo1 { get; private set; }
    public ComboBox Combo2 { get; private set; }
    public ComboBox Combo3 { get; private set; }
    public ComboBox Combo4 { get; private set; }
    public ComboBox Combo5 { get; private set; }
    //public ComboBox Combo6 { get; private set; }
    //public ComboBox Combo7 { get; private set; }

    public Label                    PlaceHolderLabel1   { get; private set; }
    public Label                    PlaceHolderLabel2   { get; private set; }
    public Label                    PlaceHolderLabel3   { get; private set; }

    public AbilitiesFlowLayoutPanel TheFlowLayoutPanel  { get; private set; }
    // Hmmm... Putting FlowLayoutPanel inside TableLayoutPanel did not work well (only one column in FlowLayoutPanel).
    #endregion

    #region Constructors
    public AbilitiesTabPage()
    {
        this.BackColor = Color.DarkOliveGreen;  // Useful for debug, not needed later on
        //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // No benefit seen when applied only here

        this.TheSplitContainer = new SplitContainer()
        { 
            Parent           = this,
            Dock             = DockStyle.Fill,
            BackColor        = Color.Aquamarine,  // Useful for Debug
            Orientation      = Orientation.Horizontal,
            BorderStyle      = BorderStyle.FixedSingle,
            FixedPanel       = FixedPanel.Panel1,
        };
        //this.SplitPanel1 = new SplitterPanel(this.TheSplitContainer);
        //this.SplitPanel2 = new SplitterPanel(this.TheSplitContainer);

        this.LabelAbilityType          = new Label() { Text = "Ability Type: ",     Width=85, Location = new Point(0,  25) };
        this.LabelAbilityCategory      = new Label() { Text = "Ability Category: ", Width=85, Location = new Point(0,  50) };
        this.LabelAbility              = new Label() { Text = "Ability: ",          Width=85, Location = new Point(0,  75) };
        this.LabelAbilityWildcardGroup = new Label() { Text = "Wildcard Group: ",   Width=85, Location = new Point(0, 100) };
        this.LabelWildcardAbility      = new Label() { Text = "Wildcard Ability: ", Width=85, Location = new Point(0, 125) };
        
        this.Combo1 = new ComboBox()
        {
            Width = 180,
            Location = new Point(85, 25),
            DataSource = AbilityType.Types,
            DisplayMember = "Name",
        };
        this.Combo2 = new ComboBox()
        {
            Width = 180,
            Location = new Point(85, 50),
            DataSource = AbilityCategory.Categories,
            DisplayMember = "Name",
        };
        this.Combo3 = new ComboBox()
        {
            Width = 180,
            Location = new Point(85, 75),
            DataSource = AbilityArchetype.AllCommonAbilities,
            DisplayMember = "Name",
        };
        this.Combo4 = new ComboBox()
        {
            Width = 180,
            Location = new Point(85, 100),
            DataSource = AbilityWildcardGroup.AllAbilityWildcardGroups,
            DisplayMember = "DisplayName",
        };
        this.Combo5 = new ComboBox()
        {
            Width = 180,
            Location = new Point(85, 125),
            DataSource = AbilityArchetypeWildcard.AllAbilityArchetypeWildcards,
            DisplayMember = "Name",
        };

        //this.PlaceHolderLabel1   = new Label() { Text = "Placeholder: Characteristics 1", Width = 1024, BackColor = Color.Coral };
        //this.PlaceHolderLabel2   = new Label() { Text = "Placeholder: Characteristics 2", Width = 1024, BackColor = Color.Coral, Location = new Point(0, 24) };
        //this.PlaceHolderLabel3   = new Label() { Text = "Placeholder: Characteristics 3", Width = 1024, BackColor = Color.Coral, Location = new Point(0, 48) };
        this.TheFlowLayoutPanel = new AbilitiesFlowLayoutPanel() 
        {
            AutoSize     = true, 
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
        };



        //this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel1);
        //this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel2);
        //this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel3);

        this.TheSplitContainer.Panel1.Controls.Add(this.LabelAbilityType);
        this.TheSplitContainer.Panel1.Controls.Add(this.LabelAbilityCategory);
        this.TheSplitContainer.Panel1.Controls.Add(this.LabelAbility);
        this.TheSplitContainer.Panel1.Controls.Add(this.LabelAbilityWildcardGroup);
        this.TheSplitContainer.Panel1.Controls.Add(this.LabelWildcardAbility);

        this.TheSplitContainer.Panel1.Controls.Add(this.Combo1);
        this.TheSplitContainer.Panel1.Controls.Add(this.Combo2);
        this.TheSplitContainer.Panel1.Controls.Add(this.Combo3);
        this.TheSplitContainer.Panel1.Controls.Add(this.Combo4);
        this.TheSplitContainer.Panel1.Controls.Add(this.Combo5);

        this.TheSplitContainer.Panel2.Controls.Add(this.TheFlowLayoutPanel);

        this.Controls.Add(this.TheSplitContainer);
        //this.Controls.Add(this.TheFlowLayoutPanel);



        // TODO:
        // Why is resizing the Form MUCH slower when
        //     (main Form constains CharacterTabControl,
        //      which contains AbilitiesTabPage,
        //      which contains AbilitiesFlowLayoutPanel),
        //  rather than
        //     (main Form contains AbilitiesFlowLayoutPanel) ?
        // - Found that SuspendLayout()/ResumeLayout() did not affect performance -- may be useful only "when adding several controls"
        // - Found that setting ControlStyles.OptimizedDoubleBuffer improved performance considerably,
        //           for (AbilityGrid, AbilityLabelAndGridPanel, CharacterTabControl)
        //   but NOT for (AbilitiesFlowLayoutPanel, AbilitiesTabPage)
    }
    #endregion

    #region Public Methods
    public void DoDataSetup()
    {
        // Note: Call exactly ONE of the .DoDataSetupForAbilityXXX() methods, calling additional will add duplicate Abilities data.
        //this.TheFlowLayoutPanel.DoDataSetupForAbilityCategories();
        this.TheFlowLayoutPanel.DoDataSetupForAbilityTypes();
        //this.TheFlowLayoutPanel.DoDataSetupForOneGiantList();
        //// TODO: Maybe add another kind, "ByAbilityTypeButWithGeneralSplitUp" ...

        var dataGridsInThisPanel = ControlExtensions.DataGridsInAbilitiesFlowLayoutPanel(this.TheFlowLayoutPanel);
        
        this.TheFlowLayoutPanel.DoColumnsSetup(dataGridsInThisPanel);
        this.TheFlowLayoutPanel.AdjustEachContainedGridToIndividualNeededHeight(dataGridsInThisPanel);
    }
    #endregion

} // class
