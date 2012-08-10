using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace OlympicsScraper
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please enter the 16 digit event code...\n");
			var eventCode = Console.ReadLine();//"0000455AC9A2095C"; // the code for the evening athletics on 9th August 2012
			
			Console.WriteLine("\n...\n");

			IWebScraper scraper = new WebScraper();

			var url =
				string.Format("http://www.tickets.london2012.com/eventdetails?id={0}", eventCode);
			
			while (true)
			{
				var html = scraper.GetHtml(url);
				var doc = new HtmlDocument();
				doc.LoadHtml(html);
				Console.WriteLine(string.Format("{0} Searching for div...", DateTime.Now.ToShortTimeString()));
				var node = doc.DocumentNode.SelectSingleNode(".//div[@id='price_cat']");

				if (node == null)
				{
					// the div wasn't found so tickets aren't available
					var sleepTime = new Random().Next(18000, 30000);
					Console.WriteLine(string.Format("Nothing found. Sleeping for {0} ms then trying again...", sleepTime));
					
					Thread.Sleep(sleepTime);
					continue;
				}

				// if we get this far the div was found so tickets ARE available
				Process.Start("Firefox.exe", url);
				Console.WriteLine("Tickets found, opening browser. Press any key to keep searching.");

				Console.ReadLine();
			}
		}
	}
}
