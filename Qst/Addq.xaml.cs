using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.Devices.Geolocation;
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
    public sealed partial class Addq : Page
    {
        bool f1, f2, f3, f4,q;
       // public List<int> numbers;

        public Addq()
        {
            this.InitializeComponent();
            sub.Visibility = Visibility.Collapsed; f1 = f2 = f3 = f4 = q = true;
            sv.Value = 20.0;
            grb.IsChecked = checked(true);
            rad = -1;       
                       
            
        }
        Geolocator loc = new Geolocator();
        

        users newuser2;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        double lon, lat;
        string useridnew2;
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            useridnew2 = (string)e.Parameter;
            Geoposition pos = await loc.GetGeopositionAsync();
             lat = pos.Coordinate.Latitude;
             lon = pos.Coordinate.Longitude;
        }

        private void aq_PivotItemLoading(Pivot sender, PivotItemEventArgs args)
        {
            if (aq.SelectedItem == ra)
                sub.Visibility = Visibility.Visible;
            else
                sub.Visibility = Visibility.Collapsed;
                
                
        }

       

        private void tb1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (f1)
            {
                f1 = false;
                tb1.Text = "";
            }
        }

        private void tb2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (f2)
            {
                tb2.Text = "";
                f2 = false;
            }
        }

        private void tb3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (f3)
            {
                f3 = false;
                tb3.Text = "";
                
            }
        }

        private void tb4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (f4)
            {
                f4 = false;
                tb4.Text = "";
            }
        }

        private void qu_GotFocus(object sender, RoutedEventArgs e)
        {
            if(q)
            {
                q = false;
                que.Text = "";
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double no = sv.Value;
            rad = sv.Value * 500;
            radis.Text = "Range: " + (no*500) + "m";
        }

        double rad;
        private void srrb_Checked(object sender, RoutedEventArgs e)
        {
            sr.Visibility = Visibility.Visible;
            rad = sv.Value*500;
        }

        private void grb_Checked(object sender, RoutedEventArgs e)
        {
            sr.Visibility = Visibility.Collapsed;
            rad = -1;
        }

        private async void sub_Click(object sender, RoutedEventArgs e)
        {


            if (que.Text == "" || tb1.Text == "" || tb2.Text == "" || tb3.Text == "" || tb4.Text == "")
            {
                await new MessageDialog("Please complete all fields").ShowAsync();

            }
            else
            {

                questions newque = new questions();

                try
                {
                    newque = new questions { question_id = Guid.NewGuid().ToString(), question_value = que.Text, userid = useridnew2, option1 = tb1.Text, option2 = tb2.Text, option3 = tb3.Text, option4 = tb4.Text, no_of_responses = 0, answered1 = 0, answered2 = 0, answered3 = 0, answered4 = 0, radius = rad, location_latitude = lat, location_longitude = lon };

                }
                catch (Exception eee)
                {
                    MessageDialog m = new MessageDialog(eee.ToString());
                    m.ShowAsync();

                }
                await App.MobileService.GetTable<questions>().InsertAsync(newque);
                Frame.Navigate(typeof(Feed));
            }
        }

    }
}
