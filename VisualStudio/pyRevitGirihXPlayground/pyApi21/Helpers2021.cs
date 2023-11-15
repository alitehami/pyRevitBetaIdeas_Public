#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
#endregion

namespace pyApi21
{
    public static class Helpers2021
    {
        public static class VersionControl
        {
            public static string[] GetUnitChoices(Document doc, out dynamic UnitOptions, out dynamic CurrentUnit)
            {
                TaskDialog.Show("Dynamic Version Test", "Helpers2021!");

                ForgeTypeId currentUnit = doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId();
                ForgeTypeId[] unitOptions = new ForgeTypeId[] 
                { UnitTypeId.Meters, UnitTypeId.Centimeters, UnitTypeId.Millimeters, UnitTypeId.Feet };

                string[] UnitsNames = unitOptions.Select(i => i.TypeId.Length.ToString()).ToArray();
                UnitOptions = unitOptions;
                CurrentUnit = currentUnit;
                return UnitsNames;
            }
        }
    }
}