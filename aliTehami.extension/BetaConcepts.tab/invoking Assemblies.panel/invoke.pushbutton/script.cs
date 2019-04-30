using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;


/*
//Ali Tehami, Woods Bagot, Melbourne, Australia.
//30/04/2019

Thanks to Joshua Lumley for his great tips that enabled this proof of concept in PyRevit.
Link to Joshua's youtube video explaning the method:
https://youtu.be/KHMwd4U_Lrs

more on Joshua's video from Jermey Tammik:
https://thebuildingcoder.typepad.com/blog/2018/09/five-secrets-of-revit-api-coding.html

 */

namespace PyRevitInvokeTesting
{
   [Autodesk.Revit.Attributes.Transaction (Autodesk.Revit.Attributes.TransactionMode.Manual)]
   public class invokingTest : IExternalCommand
   {

      //PyRevit MetaData
      public const string __title__ = "Dynamic\nInvoke";
      public const string __doc__ = "HelloWorld Testing Dynamic Invokes of external Assemblies through PyRevit";
      public const string __author__ = "PyRevit Implementation by: Ali Tehami \n+ \nOriginal code by Joshua Lumley";
      public const string __helpurl__ = @"https://github.com/alitehami";
      public const string __min_revit_ver__ = "2016";
      public const string __max_revit_ver__ = "2019";
      public const bool __beta__ = false;

      public Result Execute (ExternalCommandData commandData, ref string message, ElementSet elements)
      {
         //path to the assembly. (can be automated if there is a way to access pyrevit's 'command_path' (so assemblies can live under a lib\ folder with script.cs)
         string path = @"C:\publicRepos\pyRevitBetaIdeas_Public\aliTehami.extension\BetaConcepts.tab\invoking Assemblies.panel\invoke.pushbutton\";
         String exeConfigPath = Path.GetDirectoryName (path)  + "\\TestAssembly\\AliTpyRevitConcepts.dll";
         String exeConfigPath2 = Path.GetDirectoryName (path) + "\\TestAssembly";

         //name of the class for the command to Execute
         string strCommandName = "Command";

         byte[ ] assemblyBytes = File.ReadAllBytes (exeConfigPath);
         Assembly objAssembly = Assembly.Load (assemblyBytes);
         IEnumerable<Type> myIEnumerableType = GetTypesSafely (objAssembly);
         foreach (Type objType in myIEnumerableType)
         {
            if (objType.IsClass)
            {
               if (objType.Name.ToLower ( ) == strCommandName.ToLower ( ))
               {
                  object ibaseObject = Activator.CreateInstance (objType);
                  object[ ] arguments = new object[ ]
                  {
                     commandData,
                     exeConfigPath2,
                     elements
                  };
                  object result = null;
                  result = objType.InvokeMember ("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, ibaseObject, arguments);
                  break;
               }
            }
         }
         return Result.Succeeded;
      }

      
      private static IEnumerable<Type> GetTypesSafely (Assembly assembly)
      {
         try
         {
            return assembly.GetTypes ( );
         }
         catch (ReflectionTypeLoadException ex)
         {
            return ex.Types.Where (x => x != null);
         }
      }

   }
}
