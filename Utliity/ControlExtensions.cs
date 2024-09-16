using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WizardMakerPrototype.Controls;

namespace WizardMakerPrototype;

/// <summary>
/// Static class for Extension methods acting upon Windows.Forms Controls.
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    /// Finds the topmost Form in the containment hierarchy and returns it.
    /// </summary>
    /// <param name="control"></param>
    /// <returns>The topmost Form</returns>
    /// <exception cref="Exception"></exception>
    public static Form FindTopmostForm(Control control)
    {
        if (control is null) { return null; }
        if (control.Parent is null)
        {
            Form topmost = control as Form;
            if (topmost is null)
            {
                // One cause of this, was when data setup (which wanted to get TheCharacter from topmost form)
                // was called within the ctor of a Control.
                // Moving such data setup to be done AFTER all the ctors were called
                // (and Controls added into the hierarchy) fixed these occurrences.
                throw new Exception("ControlExtensions.FindTopmostForm(): Got strange topology, topmost is not a Form");
            }
            return topmost;
        }
        Control nextToTry = control.Parent;
        return ControlExtensions.FindTopmostForm(nextToTry);
    }

    /// <summary>
    /// Given a Control.ControlCollection, return the contents as a List of Control.<br/>
    /// This is useful for LINQ, and similar purposes.
    /// </summary>
    /// <param name="cc"></param>
    /// <returns>List of the Controls in the ControlCollection</returns>
    public static List<Control> ControlsAsList(Control.ControlCollection cc)
    {
        List<Control> list = new List<Control>();
        for (int ii = 0; ii < cc.Count; ii++)
        {
            var control = cc[ii];
            list.Add(control);
        }
        return list;
    }

    /// <summary>
    /// Given a FlowLayoutPanel, inspect the contained Controls (top-level only) <br/>
    /// and return a List of those which are DataGridView.
    /// </summary>
    /// <param name="panel"></param>
    /// <returns>List of the contained DataGridView controls</returns>
    public static List<DataGridView> DataGridsInAbilitiesFlowLayoutPanel(AbilitiesFlowLayoutPanel panel)
    {
        // Hmmm...still figuring out what is "best style" to work with these things.
        // It is inconvenient that WinForms uses 'ControlCollection' instead of a List<Control>,
        // but of course it was written before C# 2.0 and Generics, so List<T> was not a thing yet...
        var resultList = new List<DataGridView>();

        Control.ControlCollection controlsInAbilitiesFlowLayoutPanel = panel.Controls;
        for (int ii = 0; ii < controlsInAbilitiesFlowLayoutPanel.Count; ii++)
        {
            var cc = controlsInAbilitiesFlowLayoutPanel[ii];
            var abilityGroupingPanel = cc as AbilityLabelAndGridPanel;
            if (abilityGroupingPanel == null) { continue; }  // Skip any other controls

            var controlsInAbilityGroupingPanel = abilityGroupingPanel.Controls;
            for (int jj = 0; jj < controlsInAbilityGroupingPanel.Count; jj++)
            {
                var controlInAbilityGroupingPanel = controlsInAbilityGroupingPanel[jj];
                var gg = controlInAbilityGroupingPanel as AbilityGrid;
                if (gg == null) { continue; }  // Skip any other controls

                resultList.Add(gg);
            }
        }
        return resultList;
    }

} // class
