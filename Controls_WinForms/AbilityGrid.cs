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

    #region Constructors
    public AbilityGrid()
    {
        //this.Font = new Font("Consolas", 8, FontStyle.Regular);  // Note: Various different heights, widths implied by font change...

        //this.ForeColor       = Color.Red;   // grid cells text color
        //this.BackgroundColor = Color.Blue;  // color for background of control where grid is not painted

        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);  // Some benefit seen here

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

        //this.CellClick += OnGridRowClick;
        this.CellMouseDown += OnCellMouseDown;
        //this.CellMouseClick += 
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


   public void OnCellMouseDown(object sender, DataGridViewCellMouseEventArgs cmea)
    {
        var    parent      = this.Parent as AbilityLabelAndGridPanel;
        string labelText   = parent?.TheLabel?.Text;
        string columnName  = AbilitiesFlowLayoutPanel.ColumnNameForIndex(cmea.ColumnIndex);
        DataGridViewCell clickedCell = null;
        DataGridViewCell nameCell    = null;
        if (cmea.ColumnIndex >= 0 && cmea.RowIndex >= 0)
        {
            clickedCell = this[cmea.ColumnIndex, cmea.RowIndex];
            nameCell    = this[2, cmea.RowIndex];
        }
        string cellText    = clickedCell?.Value as string;
        string rowName     = nameCell?.Value    as string;
        string whichButton;

        switch (cmea.Button)
        {
            // cmea.RowIndex is -1 for a click on the Header, otherwise 0..n
            // cmea.ColumnIndex is 1..N

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
        string msg = MessageForClick(whichButton, labelText, cmea.RowIndex, cmea.ColumnIndex, rowName, cellText);
        MessageBox.Show(msg);
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
