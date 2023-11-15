#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using pyApi18;
using pyApi21;
#endregion

namespace pyRevitGirihXPlayground
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            int.TryParse(app.VersionNumber, out int version);

            string[] unitsNames;
            dynamic unitsTypes;
            dynamic currUnitType;

            if (version <= 2020)
            //unitsTypes is DisplayUnitType[] / currUnitType is DisplayUnitType
            { unitsNames = Helpers2018.VersionControl.GetUnitChoices(doc, out unitsTypes, out currUnitType); }
            else
            //unitsTypes is ForgeTypeId[] / currUnitType is ForgeTypeId
            { unitsNames = Helpers2021.VersionControl.GetUnitChoices(doc, out unitsTypes, out currUnitType); }

            TaskDialog.Show("Available Units", "Available Unit Options:\n" + string.Join("\n", unitsNames));
            if (version <= 2020)
            {
                TaskDialog.Show("Current Unit", $"Current Document Units:\n{currUnitType}");
                foreach (var unit in unitsTypes) TaskDialog.Show("Unit Option Type", $"{unit.GetType()}:\n{unit}");
            }
            else 
            //even though the "ForgeTypeId.TypeId" property is only available in 2021+ APIs, it will still work fine in 2020 and earlier versions as this code will not be excuted for 2020 and earlier versions; You MUST build the below code against 2021+ APIs to compile it without errors.
            {
                TaskDialog.Show("Current Unit", $"Current Document Units:\n{currUnitType.TypeId}");
                foreach (var unit in unitsTypes) TaskDialog.Show("Unit Option Type", $"{unit.GetType()}:\n{unit.TypeId}");
            }

            return Result.Succeeded;
        }
    }
}