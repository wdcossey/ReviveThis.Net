using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace ReviveThis.Entities
{
  public class IniParser: IDisposable
  {
    private Hashtable _keyPairs = new Hashtable();
    private string _iniFilePath;
    

    private struct SectionPair
    {
      public string Section;
      public string Key;
    }

#region 
    private bool _fileExists;

    public bool FileExists
    {
      get { return _fileExists; }
    }

    #endregion

    /// <summary>
    /// Opens the INI file at the given path and enumerates the values in the <see cref="IniParser"/>.
    /// </summary>
    /// <param name="iniPath">Full path to INI file.</param>
    public IniParser(string iniPath)
    {
      string currentRoot = null;

      _iniFilePath = iniPath;

      _fileExists = File.Exists(iniPath);

      if (!_fileExists)
      {
        return;
      }


      try
      {
        using (var iniFile = new StreamReader(iniPath))
        {
          var strLine = iniFile.ReadLine();

          while (strLine != null)
          {
            strLine = strLine.Trim();

            if (!string.IsNullOrEmpty(strLine) && !strLine.StartsWith(";"))
            {
              if (strLine.StartsWith("[") && strLine.EndsWith("]"))
              {
                currentRoot = strLine.Substring(1, strLine.Length - 2);
              }
              else
              {
                var keyPair = strLine.Split(new[] {'='}, 2);

                SectionPair sectionPair;
                string value = null;

                if (currentRoot == null)
                  currentRoot = "ROOT";

                sectionPair.Section = currentRoot;
                sectionPair.Key = keyPair[0];

                if (keyPair.Length > 1)
                {
                  //var commentPair = strLine.Split(new[] { ';' }, 2);
                  value = keyPair[1];
                }

                _keyPairs.Add(sectionPair, value);
              }
            }

            strLine = iniFile.ReadLine();
          }
        }

      }
      catch (Exception)
      {
        throw;
      }
      finally
      {

      }

      //else
      //  throw new FileNotFoundException("Unable to locate " + iniPath);

    }

    /// <summary>
    /// Returns the value for the given section, key pair.
    /// </summary>
    /// <param name="sectionName">Section name.</param>
    /// <param name="settingName">Key name.</param>
    public string GetSetting(string sectionName, string settingName)
    {
      var sectionPair = _keyPairs.Keys.Cast<SectionPair>()
        .FirstOrDefault(
          pair =>
            pair.Section.Equals(sectionName, StringComparison.InvariantCultureIgnoreCase) &&
            pair.Key.Equals(settingName, StringComparison.InvariantCultureIgnoreCase));

      var result = (string) _keyPairs[sectionPair];
      if (string.IsNullOrEmpty(result)) 
        return null;

      var commentIndex = result.IndexOf('#');
      if (commentIndex != -1)
      {
        result = result.Substring(0, commentIndex).Trim();
      }
      return result;
    }

    /// <summary>
    /// Enumerates all lines for given section.
    /// </summary>
    /// <param name="sectionName">Section to enum.</param>
    public bool SectionExists(string sectionName)
    {
      return
        _keyPairs.Keys.Cast<SectionPair>()
          .Any(pair => pair.Section.Equals(sectionName, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Enumerates all lines for given section.
    /// </summary>
    /// <param name="sectionName">Section to enum.</param>
    public string[] EnumSection(string sectionName)
    {
      var tmpArray = new ArrayList();

      foreach (var pair in _keyPairs.Keys.Cast<SectionPair>().Where(pair => pair.Section.Equals(sectionName, StringComparison.InvariantCultureIgnoreCase)))
      {
        tmpArray.Add(pair.Key);
      }

      return (string[])tmpArray.ToArray(typeof(string));
    }

    /// <summary>
    /// Adds or replaces a setting to the table to be saved.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    /// <param name="settingValue">Value of key.</param>
    public void AddSetting(string sectionName, string settingName, string settingValue)
    {
      SectionPair sectionPair;
      sectionPair.Section = sectionName;
      sectionPair.Key = settingName;

      if (_keyPairs.ContainsKey(sectionPair))
        _keyPairs.Remove(sectionPair);

      _keyPairs.Add(sectionPair, settingValue);
    }

    /// <summary>
    /// Adds or replaces a setting to the table to be saved with a null value.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    public void AddSetting(string sectionName, string settingName)
    {
      AddSetting(sectionName, settingName, null);
    }

    /// <summary>
    /// Remove a setting.
    /// </summary>
    /// <param name="sectionName">Section to add under.</param>
    /// <param name="settingName">Key name to add.</param>
    public void DeleteSetting(string sectionName, string settingName)
    {
      SectionPair sectionPair;
      sectionPair.Section = sectionName;
      sectionPair.Key = settingName;

      if (_keyPairs.ContainsKey(sectionPair))
        _keyPairs.Remove(sectionPair);
    }

    /// <summary>
    /// Save settings to new file.
    /// </summary>
    /// <param name="newFilePath">New file path.</param>
    public void SaveSettings(string newFilePath)
    {
      var sections = new ArrayList();
      var strToSave = "";

      foreach (SectionPair sectionPair in _keyPairs.Keys)
      {
        if (!sections.Contains(sectionPair.Section))
          sections.Add(sectionPair.Section);
      }

      foreach (string section in sections)
      {
        strToSave += ("[" + section + "]\r\n");

        foreach (SectionPair sectionPair in _keyPairs.Keys)
        {
          if (sectionPair.Section != section) 
            continue;

          var tmpValue = (string)_keyPairs[sectionPair];

          if (tmpValue != null)
            tmpValue = "=" + tmpValue;

          strToSave += (sectionPair.Key + tmpValue + "\r\n");
        }

        strToSave += "\r\n";
      }

      try
      {
        TextWriter tw = new StreamWriter(newFilePath);
        tw.Write(strToSave);
        tw.Close();
      }
      catch (Exception)
      {
        throw;
      }
    }

    /// <summary>
    /// Save settings back to ini file.
    /// </summary>
    public void SaveSettings()
    {
      SaveSettings(_iniFilePath);
    }

    public void Dispose()
    {
      _keyPairs = null;

      _iniFilePath = null;
    }
  }
}