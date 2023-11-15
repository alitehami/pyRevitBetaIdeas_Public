#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
#endregion

namespace pyApi18
{
    public static class Helpers2018
    {
        public static class VersionControl
        {
            public static string[] GetUnitChoices(Document doc, out dynamic UnitOptions, out dynamic CurrentUnit)
            {
                TaskDialog.Show("Dynamic Version Test", "Helpers2018!");
                
                DisplayUnitType currentUnit = doc.GetUnits().GetFormatOptions(UnitType.UT_Length).DisplayUnits;
                DisplayUnitType[] unitOptions = new DisplayUnitType[]
                { DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_CENTIMETERS, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET };

                string[] UnitsNames = unitOptions.Select(i => i.ToString()).ToArray();
                UnitOptions = unitOptions;
                CurrentUnit = currentUnit;
                return UnitsNames;
            }
        }
    }
}