using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TwittyApp.Resources;
using Microsoft.Phone.Net.NetworkInformation;
using TweetSharp;

namespace TwittyApp {
	public partial class MainPage : PhoneApplicationPage {

		private string apiKey = "tr4zL07a1kbMFSYapIrYJ3QZ9";
		private string apiKeySecret = "l77XyL4Wf2XuquLngEInQpIOi1Tv1eOoPBHJfbDdZhwIoO7GP3";

		private string accessToken = "2733902508-dUZ0PntOXx3WqvNFy3e1Yk3UaU7vXTsqvxowjlb";
		private string accessTokenSecret = "JtOvSMZktJhVEpGutlEXC6QjB2qo9Dq467DN1QtNCSnor";

		// Constructor
		public MainPage() {
			InitializeComponent();
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e) {
			string twitterHandle = tbxTwitterHandle.Text;

			if (DeviceNetworkInformation.IsNetworkAvailable) {
				try {
					var service = new TwitterService(apiKey, apiKeySecret);
					service.AuthenticateWith(accessToken, accessTokenSecret);
					llsSearchTwitterResults.ItemsSource = null;

					System.Collections.Generic.IEnumerable<TweetSharp.TwitterStatus> tweetsTest = null;
					service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() {
						ScreenName = twitterHandle
					}, (tweets, response) => {

						if (response.StatusCode == HttpStatusCode.OK) {
							this.Dispatcher.BeginInvoke(() => {
								tblkHeader.Text = twitterHandle;
								llsSearchTwitterResults.ItemsSource = tweets.ToList();
								tweetsTest = tweets;
							});
						}
					});
				} catch (Exception ex) {
					MessageBox.Show("Oops! Can't fetch this user's tweets! Ex: " + ex.ToString());
				}
			} else {

			}
		}
	}
}