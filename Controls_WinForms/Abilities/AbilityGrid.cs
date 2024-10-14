using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Controls;

public class AbilityGrid : DataGridView
{
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

    private ContextMenuStrip ContextMenuForAbilitiesHeader { get; set; }
    private ContextMenuStrip ContextMenuForAbilitiesCell   { get; set; }

    #region Constructors
    public AbilityGrid()
    {
        //this.Font = new Font("Consolas", 8, FontStyle.Regular);  // Note: Various different heights, widths implied by font change...

        //this.ForeColor       = Color.Red;   // grid cells text color
        //this.BackgroundColor = Color.Blue;  // color for background of control where grid is not painted

        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // Some benefit seen here

        #region ContextMenuStrip setup -- does nothing noticable when applied to this.ContextMenuStrip
        // Setting this.ContextMenuStrip did NOT work;
        // seems like header-specific and cell-specific things are needed instead...

        // This (combined with handling in OnCellMouseHandler) works;
        // however we might want to build a custom ContextMenuStrip within OnCellMouseHandler,
        // so that it is custom to the item clicked-upon.
        this.ContextMenuForAbilitiesHeader = new ContextMenuStrip();
        var menuItem1 = new ToolStripMenuItem("AbilityGrid (Header) Item 1");
        var menuItem2 = new ToolStripMenuItem("AbilityGrid (Header) Item 2");
        this.ContextMenuForAbilitiesHeader.Items.AddRange(new ToolStripItem[] 
        {
            menuItem1,
            menuItem2,
        });

        this.ContextMenuForAbilitiesCell = new ContextMenuStrip();
        var menuItem3 = new ToolStripMenuItem("AbilityGrid (Cell) Item 1");
        var menuItem4 = new ToolStripMenuItem("AbilityGrid (Cell) Item 2");
        this.ContextMenuForAbilitiesCell.Items.AddRange(new ToolStripItem[] 
        {
            menuItem3,
            menuItem4,
        });
        #endregion

      

        this.EnableHeadersVisualStyles = false;
        this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;  // Disallow manual vertical resize of column headers
        this.ColumnHeadersDefaultCellStyle.BackColor          = Color.LightSteelBlue;
        this.ColumnHeadersDefaultCellStyle.ForeColor          = Color.Black;
        this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
        this.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
        this.ColumnHeadersDefaultCellStyle.Font               = new Font( this.Font, FontStyle.Bold );

        this.ScrollBars = ScrollBars.None;
        // Note:
        // DataGridViewAutoSizeColumnsMode.AllCells
        // works well, but appears to act to prohibit manual resize also? 
        // My workaround is accomplished by setting this in Form1.cs region.
        // A slightly different approach from Stack Overflow:
        // https://stackoverflow.com/questions/1025670/how-do-you-automatically-resize-columns-in-a-datagridview-control-and-allow-the
        this.AutoSizeColumnsMode      = DataGridViewAutoSizeColumnsMode.None;  // Changed per-column later
        this.AllowUserToResizeColumns = false;

        this.DefaultCellStyle.SelectionForeColor = Color.Black;
        this.DefaultCellStyle.SelectionBackColor = Color.PowderBlue;

        //this.AutoSizeRowsMode       = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        this.AllowUserToResizeRows    = false;
        this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        this.CellBorderStyle          = DataGridViewCellBorderStyle.Single;

        this.GridColor         = Color.Black;
        this.RowHeadersVisible = false;
        this.SelectionMode     = DataGridViewSelectionMode.FullRowSelect;  // or DataGridViewSelectionMode.CellSelect
        this.MultiSelect       = false;

        this.Margin = Padding.Empty;  // When in AbilityGroupingPanel, we want zero Margin
        this.Dock   = DockStyle.None;  // When inside AbilitiesFlowLayoutPanel, DockStyle.None is desirable

        //this.CellMouseDown += OnCellMouseHandler;  // Note: Currently, this interferes with column resizing, which was set up in AbilitiesFlowLayoutPanel.DoColumnsSetup()
        this.CellMouseUp    += OnCellMouseHandler;  // With this one, we get the ContextMenuStrip handling from parent Control (whereas CellMouseDown does not)
        //this.CellMouseClick += OnCellMouseHandler;  // Similar result as for CellMouseUp
    }
    #endregion

    public int ResizeGridToNeededHeight()
    {
        var query = this.Rows.Cast<DataGridViewRow>();
        int sumOfRowHeights  = query.Sum(rr => rr.Height);
        int calculatedHeight = this.ColumnHeadersHeight + sumOfRowHeights;
        int result = calculatedHeight + 2;  // Fudge factor (2 px) to prevent vertical scrollbar
        this.Height = result;
        return result;
    }

   public void OnCellMouseHandler(object sender, DataGridViewCellMouseEventArgs cmea)
    {
        var    parent      = this.Parent as AbilityLabelAndGridPanel;
        string labelText   = parent?.TheLabel?.Text;
        string columnName  = AbilitiesFlowLayoutPanel.ColumnNameForIndex(cmea.ColumnIndex);
        DataGridViewCell clickedCell = null;
        DataGridViewCell nameCell    = null;
        bool isClickInCells = false;
        if (cmea.ColumnIndex >= 0 && cmea.RowIndex >= 0)
        {
            clickedCell = this[cmea.ColumnIndex, cmea.RowIndex];
            nameCell    = this[2, cmea.RowIndex];  // WART: 2 is a magic number, const AbilitiesFlowLayoutPanel.COL_AbilityName
            isClickInCells = true;
        }
        string cellText    = clickedCell?.Value.ToString();
        string rowName     = nameCell?.Value.ToString();
        string whichButton;

        var dgv = (AbilityGrid)sender;
        var hit = dgv.HitTest(cmea.X, cmea.Y);  // Results always show DataGridViewHitTestType.ColumnHeader, which seems wrong...

        switch (cmea.Button)
        {
            // cmea.RowIndex    is 0..n, or -1 for a click on the ColumnHeader
            // cmea.ColumnIndex is 0..N  (would perhaps be -1 for the RowHeader, if we had one?)

            case MouseButtons.Left:
                whichButton = "Left";
                break;

            case MouseButtons.Middle:
                whichButton = "Middle";
                break;

            case MouseButtons.Right:
                whichButton = "Right";
                ContextMenuStrip cms = null;
                //switch (hit.Type)
                //{
                //    // Do we want a different ContextMenuStrip for header versus cells ?
                //    case DataGridViewHitTestType.ColumnHeader:
                //        cms = this.ContextMenuForAbilitiesHeader;
                //        break;
                //    case DataGridViewHitTestType.Cell:
                //        // Hmmm... never reaches this; all clicks are counted as being in the ColumnHeader.  What's up with that?
                //        cms = this.ContextMenuForAbilitiesCell;
                //        break;
                //}
                if (isClickInCells)
                {
                    cms = this.ContextMenuForAbilitiesCell;
                }
                else
                {
                    cms = this.ContextMenuForAbilitiesHeader;
                }
                if (cms != null)
                {
                    cms.Show(dgv, cmea.Location);
                    return;
                }
                break;
            default:
                whichButton = "(No button, impossible?)";
                break;
        }
        string caption = "AbilityGrid.OnCellMouseDown()";
        string msg     = MessageForClick(whichButton, labelText, cmea.RowIndex, cmea.ColumnIndex, rowName, cellText);
        MessageBox.Show(msg, caption);
    }

    private string MessageForClick(string whichButton, string labelText, int rowIndex, int columnIndex, string rowName, string cellText)
    {
        string columnName = AbilitiesFlowLayoutPanel.ColumnNameForIndex(columnIndex);

        string str = string.Format(
            "Grid: {0} click on grid for '{1}' \n" +
            "    Row={2}, Col={3}\n" +
            "    Ability Name = '{4}'\n" +
            "    Column Name = '{5}'\n" +
            "    Clicked Cell Text = '{6}'",
            whichButton, labelText, rowIndex, columnIndex, rowName, columnName, cellText
            );
        return str;
    }

} // class
