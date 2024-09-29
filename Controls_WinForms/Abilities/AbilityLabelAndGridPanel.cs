using System;
using System.Drawing;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Controls;

public class AbilityLabelAndGridPanel : TableLayoutPanel
{
    #region Public Members
    //public Character TheCharacter
    //{
    //    get
    //    {
    //        var parent = ControlExtensions.FindTopmostForm(this);
    //        var form   = parent as FormForArM5Character;
    //        if (form is null) { throw new Exception("Strange, got topmost Form that was not FormForArM5Character"); }
    //        var result = form.TheCharacter;
    //        return result;
    //    }
    //}

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
        this.TheLabel.MouseClick += OnLabelClick;

        this.TheGrid = new AbilityGrid();

        this.Controls.Add(this.TheLabel);
        this.Controls.Add(this.TheGrid);
    }
    #endregion

    public void OnLabelClick(object sender, MouseEventArgs e)
    {
        Label theLabel = sender as Label;
        string whichButton;
        string labelText = theLabel?.Text;
        string msg;
        switch (e.Button)
        {
            case MouseButtons.Left:
                whichButton = "Left";
                break;

            case MouseButtons.Middle:
                whichButton = "Middle";
                break;

            case MouseButtons.Right:
                whichButton = "Right";
                break;

            default:
                whichButton = "(No button, impossible?)";
                break;
        }
        string caption = "AbilityLabelAndGridPanel.OnLabelClick()";
        msg = MessageForClick(whichButton, labelText);
        MessageBox.Show(msg, caption);
    }

    private string MessageForClick(string whichButton, string labelText)
    {
        string str = string.Format("Label: {0} click on {1}", whichButton, labelText);
        return str;
    }

} // class
