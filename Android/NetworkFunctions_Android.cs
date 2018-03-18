using System;
using System.Net;
using FamilyMenu;
using Xamarin.Forms;
using FamilyMenu.Android;
using System.IO;
using System.Collections.Generic;

[assembly: Dependency (typeof (NetworkFunctions_Android))]

namespace FamilyMenu.Android
{
	public class NetworkFunctions_Android : INetworkFunctions
	{
		WebClient myWebClient = new WebClient();

		public NetworkFunctions_Android ()
		{
		}

		public async void callAsyncPHPScript (string commandline) {
			Console.WriteLine ("callAsyncPHPScript - Start: " + commandline);

			myWebClient = new WebClient ();

			string bytes = null;

			try {
				myWebClient.DownloadStringCompleted += (sender, e) => {

					Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
//						MessagingCenter.Send<INetworkFunctions> (this, "AsyncCallComplete");

						MessagingCenter.Send<INetworkFunctions> (this, "UpdateListView");
					});

				};

				bytes = await myWebClient.DownloadStringTaskAsync(new Uri(commandline));
			}
			catch (OperationCanceledException) {
				Console.WriteLine ("Task Cancelled");
			}
			catch (Exception ex) {
				Console.WriteLine (ex.ToString ());
			}

			Console.WriteLine (bytes);

			Console.WriteLine ("callAsyncPHPScript - Einde");
		}
	}
}

