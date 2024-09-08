using System.Drawing;
using System.Windows.Forms;

using WizardMakerPrototype.Models;
using WizardMakerPrototype.Controls;
using WizardMakerPrototype.Controls_WinForms;

namespace WizardMakerPrototype;

public class FormForArM5Character : Form
{
    // Soon, a CharacterTabControl, and this panel moved to there...
    #region Public members
    public AbilitiesFlowLayoutPanel PanelForAbilityGroupings { get; private set; }
    public CharacterTabControl      TheTabControl            { get; private set; }
    #endregion

    #region Constructors
    public FormForArM5Character()
    {
        this.Name = "TEMP MainWindow";
        this.Text = "Wizard Maker - TEMP Main Window";
        //this.Font = new Font("Microsoft Sans Serif", 8.25f);  // This is the default font used, when .Font is not specified
        //this.Font = new Font("Verdana", 7.0f);                // This font is used in the .ODT character sheet; looks different than in a PDF, why?  Change in font would imply various changes in width/height/padding and "fudge" factor adjustments...
        this.AutoScaleDimensions = new SizeF( 6F, 13F );
        this.AutoScaleMode       = AutoScaleMode.Font;
        this.ClientSize          = new Size( 1024, 600 );
        this.BackColor           = Color.Green;  // Useful for debugging, probably remove later
        //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        //this.PanelForAbilityGroupings = new AbilitiesFlowLayoutPanel();
        this.TheTabControl = new CharacterTabControl();
        this.TheTabControl.Dock = DockStyle.Fill;


        #region GUI Layout: SuspendLayout()/add controls/ResumeLayout()/PerformLayout()
        this.SuspendLayout();

        // TODO:
        // Why is resizing the Form MUCH slower when using TabControl, rather than PanelForAbilityGroupings ?
        // - Found that SuspendLayout()/ResumeLayout() did not affect performence -- may be useful only "when adding several controls"
        // - Found that setting ControlStyles.OptimizedDoubleBuffer improved performance considerably, for (AbilityGrid, AbilityLabelAndGridPanel, CharacterTabControl)
        //   but not for (AbilitiesFlowLayoutPanel, AbilitiesTabPage)
        this.Controls.Add(this.TheTabControl);
        //this.Controls.Add(this.PanelForAbilityGroupings);

        this.ResumeLayout( false );
        this.PerformLayout();
        #endregion

        var foo = Saga.CurrentSaga;  // for breakpoint
        var bar = AbilityWildcardGroup.AllAbilityWildcardGroups;  // for breakpoint
        var baz = AbilityArchetypeWildcard.AllAbilityArchetypeWildcards;  // for breakpoint
    }
    #endregion

} // class
