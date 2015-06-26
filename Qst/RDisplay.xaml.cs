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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Qst
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RDisplay : Page
    {
        public RDisplay()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string str = e.Parameter.ToString();
            que.Text = str;
            opt1.Text= (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option1)
                            .ToEnumerableAsync()).FirstOrDefault();

            opt2.Text = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option2)
                            .ToEnumerableAsync()).FirstOrDefault();

            opt3.Text = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option3)
                            .ToEnumerableAsync()).FirstOrDefault();

            opt4.Text = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option4)
                            .ToEnumerableAsync()).FirstOrDefault();

            IEnumerable<int> temp = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.answered1)
                            .ToEnumerableAsync());
            int ctr1 = temp.First();
            op1.Text = ctr1.ToString();

            IEnumerable<int> temp2 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.answered2)
                            .ToEnumerableAsync());
            int ctr2 = temp2.First();
            op2.Text = ctr2.ToString();

            IEnumerable <int> temp3 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.answered3)
                            .ToEnumerableAsync());
            int ctr3 = temp3.First();
            op3.Text = ctr3.ToString();

            IEnumerable<int> temp4 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.answered4)
                            .ToEnumerableAsync());
            int ctr4 = temp4.First();
            op4.Text = ctr4.ToString();
            pr.IsActive = false;

            
        }
    }
}
