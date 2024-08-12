using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

using WizardMakerPrototype.Models;
using WizardMakerPrototype.Controls;

namespace WizardMakerPrototype.Controls_WinForms;

public class CharacterTabControl : TabControl
{
    #region Public members
    public TabPage TheAbilitiesPage       { get; private set; }
    public TabPage TheControlsTestbedPage { get; private set; }
    public TabPage TheYetAnotherPage      { get; private set; }
    #endregion

    #region Constructors
    public CharacterTabControl()
    {
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // Some benefit seen here

        TheAbilitiesPage       = new AbilitiesTabPage() { Text = "Abilities Page" };
        TheControlsTestbedPage = new TabPage()          { Text = "Testbed Page",      BackColor = Color.Tan };
        TheYetAnotherPage      = new TabPage()          { Text = "Yet Another Page",  BackColor = Color.Turquoise };
        TheControlsTestbedPage.Controls.Add(new Label() { Text = "Placeholder Label", BackColor = Control.DefaultBackColor });
        TheYetAnotherPage.Controls.Add(new Label()      { Text = "Yet Another Placeholder Label", Width = 200, BackColor = Control.DefaultBackColor });

        this.DrawMode  = TabDrawMode.OwnerDrawFixed;
        this.Padding   = new Point(this.Padding.X + 10, this.Padding.Y + 4);
        this.DrawItem += new DrawItemEventHandler(OnDrawItem);

        //#region GUI Layout: SuspendLayout()/add controls/ResumeLayout()/PerformLayout()  -- Perhaps performance benefit if used here, once sufficient TabPages are contained...
        //this.SuspendLayout();

        this.Controls.Add(TheAbilitiesPage);
        this.Controls.Add(TheControlsTestbedPage);
        this.Controls.Add(TheYetAnotherPage);

        //this.ResumeLayout( false );
        //this.PerformLayout();
        //#endregion
    }
    #endregion

    #region Event Handlers
    private void OnDrawItem(object sender, DrawItemEventArgs eventArgs)
    {
        var tabControl = sender as TabControl;
        if (tabControl == null) { return; }
        int index = eventArgs.Index;

        TabPage      currentTab = tabControl.TabPages[index];
        Rectangle    tabRect    = tabControl.GetTabRect(index);
        Graphics     graphics   = eventArgs.Graphics;
        Pen          bluePen    = new Pen(Color.Blue,  3.0f);
        Pen          blackPen   = new Pen(Color.Black, 2.0f);
        StringFormat sf         = new StringFormat()
        {
            Alignment     = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };

        using (SolidBrush textBrush = new SolidBrush(Color.Black))
        {
            if (Convert.ToBoolean(eventArgs.State & DrawItemState.Selected))
            {
                // This is the selected tab, draw label in bold:
                Font boldFont = new Font(tabControl.Font.FontFamily, tabControl.Font.Size, FontStyle.Bold);
                graphics.DrawRectangle(bluePen, tabRect);
                graphics.DrawString(currentTab.Text, boldFont, textBrush, tabRect, sf);
            }
            else
            {
                // This is one of the not-selected tabs; draw label normally:
                graphics.DrawRectangle(blackPen, tabRect);
                graphics.DrawString(currentTab.Text, tabControl.Font, textBrush, tabRect, sf);
            }
        }
    }
    #endregion

} // class
