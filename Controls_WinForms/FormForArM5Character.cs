using System.Drawing;
using System.Windows.Forms;

using WizardMakerPrototype.Models;
using WizardMakerPrototype.Controls;
using WizardMakerPrototype.Controls_WinForms;
using System;
using System.Runtime.CompilerServices;

namespace WizardMakerPrototype;

public class FormForArM5Character : Form
{
    #region Public members
    public Character TheCharacter { get; private set; }

    //public AbilitiesFlowLayoutPanel PanelForAbilityGroupings { get; private set; }
    public CharacterTabControl      TheTabControl            { get; private set; }
    #endregion

    #region Constructors
    public FormForArM5Character(Character character)
    {
        this.TheCharacter = character;

        this.Name = "TEMP MainWindow";
        this.Text = "Wizard Maker - TEMP Main Window";
        //this.Font = new Font("Microsoft Sans Serif", 8.25f);  // This is the default font used, when .Font is not specified
        //this.Font = new Font("Verdana", 7.0f);                // This font is used in the .ODT character sheet; looks different than in a PDF, why?  Change in font would imply various changes in width/height/padding and "fudge" factor adjustments...
        this.AutoScaleDimensions = new SizeF( 6F, 13F );
        this.AutoScaleMode       = AutoScaleMode.Font;
        this.ClientSize          = new Size( 1024, 600 );
        this.BackColor           = Color.Green;  // Useful for debugging, probably remove later
        //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        this.TheTabControl = new CharacterTabControl();
        this.TheTabControl.Dock = DockStyle.Fill;

        this.Shown += OnFormShown;

        #region GUI Layout: SuspendLayout()/add controls/ResumeLayout()/PerformLayout()
        this.SuspendLayout();

        // TODO:
        // Why is resizing the Form MUCH slower when using TabControl, rather than PanelForAbilityGroupings (a AbilitiesFlowLayoutPanel used directly) ?
        // - Found that SuspendLayout()/ResumeLayout() did not affect performance -- may be useful only "when adding several controls"
        // - Found that setting ControlStyles.OptimizedDoubleBuffer improved performance considerably, for (AbilityGrid, AbilityLabelAndGridPanel, CharacterTabControl)
        //   but not for (AbilitiesFlowLayoutPanel, AbilitiesTabPage)
        this.Controls.Add(this.TheTabControl);
        //this.Controls.Add(this.PanelForAbilityGroupings);

        this.ResumeLayout( false );
        this.PerformLayout();
        #endregion

        #region DEBUG for Character data setup
        this.TheCharacter.DEBUG_DataSetupOfAbilityInstances();

        this.TheTabControl.DoDataSetup();
        #endregion

        #region DEBUG variables for inspection via breakpoint
        var debugSaga                      = Saga.CurrentSaga;
        var debugWildCardGroups            = AbilityWildcardGroup.AllAbilityWildcardGroups;
        var debugAbilityArchetypes         = AbilityArchetype.AllCommonAbilities;
        var debugAbilityArchetypeWildcards = AbilityArchetypeWildcard.AllAbilityArchetypeWildcards;
        #endregion
    }
    #endregion

    #region Event Handlers
    public void OnFormShown(object sender, EventArgs e)
    {
        // HACK: The behavior of SplitContainer is odd;
        // attempts to programatically set the SplitterDistance 
        // prior to form.Shown seem to be largely ignored.
        // 
        // This may be due to various contained control dimensions not being set up yet?

        // As a result, possibly some other control dimension adjustments will end up here.  Blah.

        this.TheTabControl.TheAbilitiesPage.TheSplitContainer.SplitterDistance = 125;
    }
    #endregion


} // class
