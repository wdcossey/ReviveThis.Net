using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReviveThis.Helpers
{
  public static class MultipartFormData
  {
    public static async Task<string> Post(string postUrl, string userAgent,
      IDictionary<string, string> contentData)
    {
      using (var response = await PostFormAsync(postUrl, userAgent, contentData))
      {
        try
        {
          response.EnsureSuccessStatusCode();

          return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
          throw new Exception(ex.Message);
        }
      }
    }
    
    //public static async Task<byte[]> PostRetunBytes(string postUrl, string userAgent,
    //  IDictionary<string, string> contentData)
    //{
    //  using (var response = await PostFormAsync(postUrl, userAgent, contentData))
    //  {
    //    try
    //    {
    //      response.EnsureSuccessStatusCode();

    //      return await response.Content.ReadAsByteArrayAsync();
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //      throw new Exception(ex.Message);
    //    }
    //  }
    //}

    private static async Task<HttpResponseMessage> PostFormAsync(string postUrl, string userAgent,
      IDictionary<string, string> contentData)
    {
      using (var handler = new HttpClientHandler {AllowAutoRedirect = false})
      using (var client = new HttpClient(handler))
      {
        client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        using (var content =
          new MultipartFormDataContent(String.Format("----------{0:N}", Guid.NewGuid())))
        {
          foreach (var valuePair in contentData)
          {
            content.Add(new StringContent(valuePair.Value), valuePair.Key);
          }

          return await client.PostAsync(postUrl, content);
        }
      }
    }
  }
}