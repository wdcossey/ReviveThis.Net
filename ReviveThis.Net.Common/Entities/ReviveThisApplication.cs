using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;

namespace ReviveThis.Entities
{

  public class ReviveThisApplication
  {
    private static volatile ReviveThisApplication _instance;
    private static readonly object LockObj = new Object();

    public static ReviveThisApplication Default
    {
      get
      {
        if (_instance == null)
        {
          lock (LockObj)
          {
            if (_instance == null)
              _instance = new ReviveThisApplication();
          }
        }

        return _instance;
      }
    }

    private ReviveThisSettings _settings = null;

    public ReviveThisSettings Settings
    {
      get
      {
        if (_settings != null)
          return _settings;

        return _settings = ReviveThisSettings.Default;
      }
    }

    private AddInBootStrapper _addInBootStrapper = null;

    public AddInBootStrapper AddIns
    {
      get
      {
        if (_addInBootStrapper != null)
          return _addInBootStrapper;

        _addInBootStrapper = new AddInBootStrapper();

        //An aggregate catalog that combines multiple catalogs
        var catalog = new AggregateCatalog();
        
        //Adds all the parts found in same directory where the application is running!
        var currentPath =
          Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof (ReviveThisApplication)).Location);
        if (currentPath == null || !Directory.Exists(currentPath))
        {
          throw new DirectoryNotFoundException(currentPath);
        }

        if (!Directory.Exists(currentPath = Path.Combine(currentPath, "AddIns")))
        {
          Directory.CreateDirectory(currentPath);
        }

        catalog.Catalogs.Add(new DirectoryCatalog(currentPath));

        //Create the CompositionContainer with the parts in the catalog
        var container = new CompositionContainer(catalog);

        //Fill the imports of this object
        try
        {
          container.ComposeParts(_addInBootStrapper);
        }
        catch (CompositionException ex)
        {
          Debug.WriteLine(ex.ToString());
        }
        catch (Exception ex)
        {
          Debug.WriteLine(ex.ToString());
        }

        return _addInBootStrapper;
      }
    }
    
  }
}