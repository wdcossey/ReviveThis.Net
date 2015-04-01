using System.Collections.Generic;

namespace ReviveThis.AddIn.HijackThisDotDe.ExtensionMethods
{
  public static class StringExtentionMethods
  {

    private static IEnumerable<string> GetChunks(string value, int chunkSize)
    {
      var triplets = new List<string>();
      for (var i = 0; i < value.Length; i += chunkSize)
        triplets.Add(i + chunkSize > value.Length ? value.Substring(i) : value.Substring(i, chunkSize));
      return triplets;
    }

    /// <summary>
    /// Workaround for http://www.hijackthis.de/, as it adds a space every 220th char?
    /// </summary>
    /// <param name="input"></param>
    /// <param name="chunkSize"></param>
    /// <returns></returns>
    public static string Split(this string input, int chunkSize)
    {
      if (string.IsNullOrEmpty(input) || input.Length <= chunkSize)
        return input;

      //var result = string.Join(" ", GetChunks(input, chunkSize));

      return string.Join(" ", GetChunks(input, chunkSize));
    }
  }
}