using System;
using System.Net;
using FamilyMenu;
using Xamarin.Forms;
using FamilyMenu.iOS;
using System.IO;

[assembly: Dependency (typeof (NetworkFunctions_iOS))]

namespace FamilyMenu.iOS
{
	public class NetworkFunctions_iOS : INetworkFunctions
	{
		WebClient myWebClient = new WebClient ();

		public async void callAsyncPHPScript (string commandline) {
			Console.WriteLine ("callAsyncPHPScript - Start: " + commandline);

			myWebClient = new WebClient ();

			string bytes = null;

			myWebClient.DownloadStringCompleted += (sender, e) => {

//				OnDownloadCompleted(sender, e);
			};

			try {
				bytes = await myWebClient.DownloadStringTaskAsync (new Uri (commandline));
			
			} catch (OperationCanceledException) {
				Console.WriteLine ("Task Cancelled");
			} catch (Exception ex) {
				Console.WriteLine (ex);
			}

			Console.WriteLine (bytes);

			Console.WriteLine ("callAsyncPHPScript - Einde");
		}
	}
}

