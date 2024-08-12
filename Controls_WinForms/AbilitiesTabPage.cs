using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using WizardMakerPrototype.Models;
using System.Threading;

namespace WizardMakerPrototype.Controls;

public class AbilitiesTabPage : TabPage
{
    #region Public members
    public SplitContainer           TheSplitContainer   { get; private set; }
    public SplitterPanel            SplitPanel1         { get; private set; }
    public SplitterPanel            SplitPanel2         { get; private set; }

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
        this.SplitPanel1 = new SplitterPanel(this.TheSplitContainer);
        this.SplitPanel2 = new SplitterPanel(this.TheSplitContainer);

        this.PlaceHolderLabel1   = new Label() { Text = "Placeholder: Characteristics 1", Width = 1024, BackColor = Color.Coral };
        this.PlaceHolderLabel2   = new Label() { Text = "Placeholder: Characteristics 2", Width = 1024, BackColor = Color.Coral, Location = new Point(0, 24) };
        this.PlaceHolderLabel3   = new Label() { Text = "Placeholder: Characteristics 3", Width = 1024, BackColor = Color.Coral, Location = new Point(0, 48) };
        this.TheFlowLayoutPanel = new AbilitiesFlowLayoutPanel() 
        {
            AutoSize     = true, 
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
        };

        this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel1);
        this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel2);
        this.TheSplitContainer.Panel1.Controls.Add(this.PlaceHolderLabel3);
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

        #region Setup various things in the AbilityGridControls
        // Note: Call exactly ONE of the .DoDataSetupForAbilityXXX() methods, calling additional will add duplicate Abilities data.
        this.TheFlowLayoutPanel.DoDataSetupForAbilityCategories();
        //this.TheFlowLayoutPanel.DoDataSetupForAbilityTypes();
        //this.TheFlowLayoutPanel.DoDataSetupForOneGiantList();
        //// TODO: Maybe add another kind, "ByAbilityTypeButWithGeneralSplitUp" ...
        
        var dataGridsInThisPanel = ControlExtensions.DataGridsInAbilitiesFlowLayoutPanel(this.TheFlowLayoutPanel);
        
        this.TheFlowLayoutPanel.DoColumnsSetup(dataGridsInThisPanel);
        this.TheFlowLayoutPanel.AdjustEachContainedGridToIndividualNeededHeight(dataGridsInThisPanel);
        #endregion

        
    }
    #endregion

} // class
