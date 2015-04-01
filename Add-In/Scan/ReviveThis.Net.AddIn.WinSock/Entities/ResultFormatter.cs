using System;
using System.Globalization;
using System.IO;
using ReviveThis.AddIn.WinSock.Enums;
using ReviveThis.AddIn.WinSock.Interfaces;
using ReviveThis.Entities.ExtensionMethods;

namespace ReviveThis.AddIn.WinSock.Entities
{
  public static class ResultFormatter
  {
    private const string HIJACKED_ACCESS = "Hijacked Internet access by: ";

    public static string FormatToString(this ILayeredServiceProviderResultItem item)
    {
      var resultType = item.ResultType.FormatToString();

      //string catalogType;
      //if (item is INameSpaceResultItem)
      //  catalogType = "Current_NameSpace_Catalog";
      //else if (item is IProtocolResultItem)
      //  catalogType = "Current_Protocol_Catalog";
      //else
      //  catalogType = "(none)";

      switch (item.ProviderResultType)
      {
        case LayeredServiceProviderType.EmptyEntry:
          return string.Format("{0} - {1}: {2} = (none).", resultType, item.RegistryInformation.Path,
            item.RegistryInformation.Name);

        case LayeredServiceProviderType.HijackedCommonName:
          //"O10 - Hijacked Internet access by CommonName"
          return string.Format("{0} - {1}CommonName, {2}.", resultType, HIJACKED_ACCESS, item.FileName);

        case LayeredServiceProviderType.HijackedNewDotNet:
          //"O10 - Hijacked Internet access by New.Net"
          return string.Format("{0} - {1}New.Net, {2}.", resultType, HIJACKED_ACCESS, item.FileName);

        case LayeredServiceProviderType.HijackedWebHancer:
          //"O10 - Hijacked Internet access by WebHancer"
          return string.Format("{0} - {1}WebHancer, {2}.", resultType, HIJACKED_ACCESS, item.FileName);

        case LayeredServiceProviderType.MissingChainGap:
        {
          //"O10 - Broken Internet access because of LSP chain gap (#" & CStr(i) & " in chain of " & CStr(lNumNameSpace) & " missing)"
          return string.Format("{0} - Broken Internet access because of LSP chain gap (#{1} in chain of {2} missing).",
            resultType, item.RegistryInformation.Name, item.RegistryInformation.Value);
        }

        case LayeredServiceProviderType.MissingLibrary:
        {
          Int32 chainGap;
          var parseString = Path.GetFileName(item.RegistryInformation.Path);
          parseString = !Int32.TryParse(parseString, out chainGap)
            ? "(unknown)"
            : chainGap.ToString(CultureInfo.InvariantCulture);

          return string.Format("{0} - {1} for chain #{2} is empty and/or invalid.", resultType, item.RegistryInformation.Name, parseString);
        }

        case LayeredServiceProviderType.MissingProvider:
          //"O10 - Broken Internet access because of LSP provider '" & sFile & "' missing"
          return string.Format("{0} - Broken Internet access because of LSP provider '{1}' missing.", resultType,
            item.FileName);

        case LayeredServiceProviderType.NoRegistryEntries:
          return string.Format("{0} - \"Num_Catalog_Entries\" is 0 (or null).", resultType);

          //case LayeredServiceProviderType.RegistryAccessError:
          //  return string.Format("{0} - (undefined).", resultType);

        case LayeredServiceProviderType.RegistryKeyNotFound:
          return string.Format("{0} - Could not find LSP registry key, '{1}'.", resultType, item.RegistryInformation.Path);

        case LayeredServiceProviderType.UnknownFile:
          //"O10 - Unknown file in Winsock LSP: " & sFile
          return string.Format("{0} - Unknown file in Winsock LSP: '{1}'.", resultType, item.FileName);

        default:
          return string.Format("{0} - '{1}.{2}' is undefined.", resultType, item.ProviderResultType.GetType().Name, item.ProviderResultType);
      }
    }
  }
}