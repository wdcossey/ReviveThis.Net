using System.IO;

namespace ReviveThis.Entities.ExtensionMethods
{
  public static class StringExtensionMethods
  {
    public static string ExtractFileName(this string input)
    {

      if (input.StartsWith("\""))
      {
        string temp;

        if (input.EndsWith("\""))
        {
          temp = input.TrimStart('"').TrimEnd('"'); //.Remove(input.Length - 1, 1).Remove(0, 1);

          //Check if the file exists, if it does then we return.
          if (File.Exists(temp))
            return temp;
        }

        temp = input.TrimStart('"');
        temp = temp.Substring(0, temp.IndexOf('"'));

        if (File.Exists(temp))
          return temp;
      }

      //Check if the file exists, if it does then we return.
      if (File.Exists(input))
        return input;

      if (input.IndexOf('.') > -1)
      {
        var index = input.LastIndexOf('.');
        var temp = input.Substring(0, input.IndexOf(' ', index));

        if (File.Exists(temp))
          return temp;
      }

      return input;
    }
  }
}