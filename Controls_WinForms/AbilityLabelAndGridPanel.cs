using System.Drawing;
using System.Windows.Forms;

namespace WizardMakerPrototype.Controls;

public class AbilityLabelAndGridPanel : TableLayoutPanel
{
    #region Public Members
    public Label       TheLabel { get; private set; }
    public AbilityGrid TheGrid  { get; private set; }
    #endregion

    #region Constructors
    public AbilityLabelAndGridPanel(string labelText)
    {
        //this.BackColor = Color.CadetBlue;  // Helpful for debugging, not needed otherwise
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // Some benefit seen here...

        this.TheLabel = new Label()
        {
            BackColor = Color.LightSteelBlue,  //Control.DefaultBackColor,
            Font      = new Font( this.Font, FontStyle.Bold ),
            Text      = labelText,
        };
        var size = TextRenderer.MeasureText(labelText, this.TheLabel.Font);
        this.TheLabel.Width  = size.Width;
        this.TheLabel.Height = size.Height;

        this.TheGrid = new AbilityGrid();

        this.Controls.Add(this.TheLabel);
        this.Controls.Add(this.TheGrid);
    }
    #endregion

} // class
