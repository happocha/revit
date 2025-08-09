using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace CreateCube
{
  [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
  public class Command : IExternalCommand
  {
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
      UIDocument uidoc = commandData.Application.ActiveUIDocument;
      if (uidoc == null) return Result.Failed;
      Document doc = uidoc.Document;

      using (Transaction t = new Transaction(doc, "Create Cube"))
      {
        t.Start();

        double size = 3.280839895; // ~1 meter in feet
        XYZ min = new XYZ(0, 0, 0);
        XYZ max = new XYZ(size, size, size);
        BoundingBoxXYZ bbox = new BoundingBoxXYZ { Min = min, Max = max };

        Solid cube = GeometryCreationUtilities.CreateBox(bbox);
        DirectShape ds = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));
        ds.ApplicationId = "RevitPlugins";
        ds.ApplicationDataId = Guid.NewGuid().ToString();
        ds.SetShape(new List<GeometryObject> { cube });

        t.Commit();
      }

      return Result.Succeeded;
    }
  }
}
