using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Controls;

// TODO: Likely will replace this with a treatment more similar to AbilityGrid / AbilityLabelAndGridPanel

// TODO:
// Not wholly happy with this control; the maximum effort solution might involve
// implementing control(s) capable of doing layout of all four combinations of (HH, HV, VH, VV) where
//     HH = (Horizontal arrangement, of two Horizontal groups each displaying 4 Characteristics)
//     HV = (Horizontal arrangement, of two Vertical   groups each displaying 4 Characteristics)
//     VH = (Vertical   arrangement, of two Horizontal groups each displaying 4 Characteristics)
//     VV = (Horizontal arrangement, of two Vertical   groups each displaying 4 Characteristics)
// 
// HH = ---- ----
// 
// HV = _  _
//      _  _
//      _  _
//      _  _
// 
// VH = ----
//      ----
// 
// VV = _
//      _
//      _
//      _
// 
//      _
//      _
//      _
//      _
// 
// Each of these has their uses.  For example, VV would be desirable for some all-text environments, such as "past text to Discord"
// 


/// <summary>
/// 
/// </summary>
public class CharacteristicsGridControl : DataGridView
{
    const int GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD = 8;
    const int GRID_CHARACTERISTICS_WIDTH_NO_BOLD    = 333;
    const int GRID_CHARACTERISTICS_WIDTH_BOLD       = GRID_CHARACTERISTICS_WIDTH_NO_BOLD + (2 * GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD);
    const int GRID_CHARACTERISTICS_WIDTH            = GRID_CHARACTERISTICS_WIDTH_BOLD;

    const int GRID_CHARACTERISTICS_HEIGHT_NO_HEADERS   = 91;
    const int GRID_CHARACTERISTICS_HEIGHT_WITH_HEADERS = (GRID_CHARACTERISTICS_HEIGHT_NO_HEADERS + 22);
    const int GRID_CHARACTERISTICS_HEIGHT              = GRID_CHARACTERISTICS_HEIGHT_WITH_HEADERS;

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
    public CharacteristicsGridControl ( int locationX, int locationY, bool hasIntelligence = true, bool showColumnHeaders = true )
    {
        string abilityNameIntOrCunning   = hasIntelligence ? "Intelligence" : "Cunning";
        string abilityAbbrevIntOrCunning = hasIntelligence ? "INT"          : "CUN";

        this.Name = "GridForCharacteristics";
        this.Location = new Point( locationX, locationY );
        this.Size = new Size( GRID_CHARACTERISTICS_WIDTH, GRID_CHARACTERISTICS_HEIGHT );
        this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        this.AllowUserToResizeColumns = false;
        this.AllowUserToResizeRows = false;

        var headerCellStyleBold = this.ColumnHeadersDefaultCellStyle;
        headerCellStyleBold.Font = new Font(headerCellStyleBold.Font, FontStyle.Bold);

        this.ColumnCount = 7;
        this.ColumnHeadersVisible = showColumnHeaders;  // Adjust GRID_CHARACTERISTICS_WIDTH, GRID_CHARACTERISTICS_HEIGHT based on this...
        this.ColumnHeadersDefaultCellStyle = headerCellStyleBold;
        this.RowHeadersVisible = false;

        this.Columns[0].Name = "Characteristic";
        this.Columns[1].Name = "Abbr";
        this.Columns[2].Name = "Value";
        this.Columns[3].Name = "";
        this.Columns[4].Name = "Characteristic";
        this.Columns[5].Name = "Abbr";
        this.Columns[6].Name = "Value";

        this.Columns[0].Width = 80 + GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD;  // adjust if bold/not
        this.Columns[1].Width = 40;
        this.Columns[2].Width = 40;
        this.Columns[3].Width = 10;
        this.Columns[4].Width = 80 + GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD;  // adjust if bold/not
        this.Columns[5].Width = 40;
        this.Columns[6].Width = 40;

        this.Columns[3].DefaultCellStyle.BackColor = this.BackgroundColor;

        this.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        this.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;

        object[] charRow1 = { "Strength",  "STR", "-1", "", abilityNameIntOrCunning,  abilityAbbrevIntOrCunning, "+3" };
        object[] charRow2 = { "Stamina",   "STA", "+2", "", "Perception",    "PER", "+2" };
        object[] charRow3 = { "Dexterity", "DEX", "-3", "", "Presence",      "PRE", "+0" };
        object[] charRow4 = { "Quickness", "QIK", "+0", "", "Communication", "COM", "+1" };

        this.Rows.Clear();
        this.AllowUserToAddRows = false;
        this.Rows.Add( charRow1 );
        this.Rows.Add( charRow2 );
        this.Rows.Add( charRow3 );
        this.Rows.Add( charRow4 );
    }
    #endregion

}
