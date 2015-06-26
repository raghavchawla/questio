using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Qst
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            pr.IsActive = true;
            if (displayname.Text == "" || userid.Text == "" || password.Password == "")
            {
                msg.Visibility = Visibility.Visible;
                pr.IsActive = false;
            }
            else
            {
                users newuser = new users { id = Guid.NewGuid().ToString(), userid = userid.Text, password = password.Password, displayname = displayname.Text };
                await App.MobileService.GetTable<users>().InsertAsync(newuser);
                pr.IsActive = false;
                Frame.Navigate(typeof(Feed), newuser.userid);
            }
        }
    }
}
