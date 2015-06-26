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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Qst
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {


        private MobileServiceCollection<users, users> new_user;
        private IMobileServiceTable<users> usertable = App.MobileService.GetTable<users>();
        string upass;
        public Login()
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
            string name = unames.Text;
            pr.IsActive = true;
            upass = (await App.MobileService.GetTable<users>()
                .Where(users => users.userid == name)
                .Select(users => users.password)
                .ToEnumerableAsync()).FirstOrDefault();
            MobileServiceInvalidOperationException e1 = null;
            pr.IsActive = false;

            try
            {
                if (pass.Password == upass)
                {
                   
                    await new MessageDialog("Login Successful").ShowAsync();
                    
                    this.Frame.Navigate(typeof(Feed),name);
                }
                else
                {
                    
                    await new MessageDialog("Invalid Credentials").ShowAsync();
                }
            }
            catch (MobileServiceInvalidOperationException e2)
            {
                e1 = e2;
            }
            if (e1 != null)
            {
                await new MessageDialog(e1.Message).ShowAsync();
            }

        }
    }
}
