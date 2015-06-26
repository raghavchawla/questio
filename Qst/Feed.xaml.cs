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
using Windows.Devices.Geolocation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Qst
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Feed : Page
    {
        public Feed()
        {
            this.InitializeComponent();
        }
        Geolocator loc = new Geolocator();
        double lon, lat;
        public struct Posit
        {
            public double Latitude;
            public double Longitude;
            public Posit(double a, double b)
            {
                Latitude = a;
                Longitude = b;
            }
        }

        Posit user, question;

       

        string useridnew;
      //  users un;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            useridnew = (String)e.Parameter;
            Geoposition pos = await loc.GetGeopositionAsync();
            lat=pos.Coordinate.Latitude;
            lon=pos.Coordinate.Longitude;
            user = new Posit(lat, lon);

            /*IEnumerable<double> latitemp = (await App.MobileService.GetTable<questions>()
                                            .Select(questions => questions.location_latitude)
                                            .ToListAsync());
            IEnumerable<double> longitemp = (await App.MobileService.GetTable<questions>()
                                        .Select(questions => questions.location_longitude)
                                        .ToListAsync());

            for(int i=0;i<latitemp.Count();i++)
           {
               Posit a = new Posit(latitemp.ElementAt(i), longitemp.ElementAt(i));
           }*/
        }


        public double Distance(Posit pos1, Posit pos2)
        {
            double R = 6371;
            double dLat = this.toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = this.toRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.toRadian(pos1.Latitude)) * Math.Cos(this.toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }

        private double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }  

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Addq),useridnew);
        }
        Guid g;

        List<questions> myList = new List<questions>();
        
        public async void Logic()
        {
            List<questions> quest = (await App.MobileService.GetTable<questions>().ToListAsync());

            foreach (var item in quest)
            {
                double lat = item.location_latitude;
                double lon = item.location_longitude;
                Posit a = new Posit(lat, lon);

                if (item.radius == -1)
                {
                    myList.Add(item);

                }

                if (Distance(user, a) < item.radius)
                {
                    myList.Add(item);
                }

            }

        }
        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            pr.IsActive = true;
            intro.Visibility = Visibility.Collapsed;
            intro1.Visibility = Visibility.Collapsed;
            feed.Visibility = Visibility.Visible;
        MobileServiceCollection<questions, questions> user;
        IMobileServiceTable<questions> userTable = App.MobileService.GetTable<questions>();

           user = await userTable.ToCollectionAsync();
           
        Logic();
        ourlistview.ItemsSource = user;
       // ourlistview.ItemsSource = myList;
            pr.IsActive = false;
            
        }

        
        private void ourlistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            questions q = new questions();
            q = (questions)ourlistview.SelectedItem;
            Frame.Navigate(typeof(Qdisplay),q);
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyQ));
        }

       
    }
}
