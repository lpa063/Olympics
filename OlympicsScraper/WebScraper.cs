using System;
using System.IO;
using System.Net;

namespace OlympicsScraper
{
	public interface IWebScraper
	{
		string GetHtml(string url);
	}

	public class WebScraper : IWebScraper
	{
		/// <summary>
		/// Returns the html for the page provided. Returns null if the page couldn't be loaded.
		/// </summary>
		/// <param name="url">The URL of the page to load.</param>
		/// <returns></returns>
		public string GetHtml(string url)
		{
			try
			{
				var webRequest = (HttpWebRequest) WebRequest.Create(url);

				SetWebRequestDefaults(webRequest);

				var html = string.Empty;

				using (var httpWebResponse = (HttpWebResponse) webRequest.GetResponse())
				{
					using (var responseStream = httpWebResponse.GetResponseStream())
					{
						if (responseStream != null)
						{
							using (var streamReader = new StreamReader(responseStream))
							{
								html = streamReader.ReadToEnd();
							}
						}
					}
				}
				return html;
			}
			catch (Exception) {
				// timeout or invalid url
				return null;
			}
		}

		private void SetWebRequestDefaults(HttpWebRequest httpWebRequest)
		{
			httpWebRequest.Method = "Get";
			httpWebRequest.Timeout = 12000;
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.CachePolicy = new System.Net.Cache.HttpRequestCachePolicy(System.Net.Cache.HttpRequestCacheLevel.NoCacheNoStore);
			httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:14.0) Gecko/20100101 Firefox/14.0.1";
			httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			httpWebRequest.KeepAlive = false;
			httpWebRequest.Proxy = null;
		}

	}
}
