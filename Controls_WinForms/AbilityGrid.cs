using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Controls;

// TODO:
// Investigate using a TableLayoutPanel, that contains a Label and a DataGridView
// This would allow some kind of label header for each grid...
public class AbilityGrid : DataGridView
{
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
    }
    #endregion

    public int ResizeGridToNeededHeight()
    {
        var query = this.Rows.Cast<DataGridViewRow>();
        int sumOfRowHeights = query.Sum(rr => rr.Height);
        int calculatedHeight = this.ColumnHeadersHeight + sumOfRowHeights;
        int result = calculatedHeight + 2;  // Fudge factor (2 px) to prevent vertical scrollbar
        this.Height = result;
        return result;
    }

} // class
