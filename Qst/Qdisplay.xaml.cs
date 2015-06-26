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
    public sealed partial class Qdisplay : Page
    {
        public Qdisplay()
        {
            this.InitializeComponent();
        }

        private MobileServiceCollection<questions, questions> user;
        private IMobileServiceTable<questions> userTable = App.MobileService.GetTable<questions>();

        private MobileServiceCollection<users, users> usera;
        private IMobileServiceTable<users> useraTable = App.MobileService.GetTable<users>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        string str;
        questions qu;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
             qu = (questions)e.Parameter;
            str = qu.question_value;
            string opt1 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value==str)
                            .Select(questions => questions.option1)
                            .ToEnumerableAsync()).FirstOrDefault();

            string opt2 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option2)
                            .ToEnumerableAsync()).FirstOrDefault();

            string opt3 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option3)
                            .ToEnumerableAsync()).FirstOrDefault();

            string opt4 = (await App.MobileService.GetTable<questions>()
                            .Where(questions => questions.question_value == str)
                            .Select(questions => questions.option4)
                            .ToEnumerableAsync()).FirstOrDefault();
            ques.Text = str;
            op1.Content = opt1;
            op2.Content = opt2;
            op3.Content = opt3;
            op4.Content = opt4;
            pr.IsActive = false;
                    


        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            vot.Visibility = Visibility.Visible;
        }

        
        private async void vot_Click(object sender, RoutedEventArgs e)
        {
            bool tempflag = false;

            /*IEnumerable<string> usernames = (await App.MobileService.GetTable<answers>()
                                        .Where(answers => answers.questionid == qu.question_value)
                                        .Select(answers => answers.userid)
                                        .ToEnumerableAsync());
            foreach(string x in usernames)
            {
                if (x.Equals(qu.userid))
                {
                    await new MessageDialog("Already Answered").ShowAsync();
                    tempflag = true;
                }
            }*/

            if(tempflag==false)
            { 

            var temp = await App.MobileService.GetTable<questions>()
                       .Where(questions => questions.question_value == str)
                       .ToListAsync();

            var temp2 = temp.FirstOrDefault();
            int flag=-1;
            if (temp2 != null)
            {

                if (op1.IsChecked == true)
                {
                    temp2.answered1 = temp2.answered1 + 1;
                    flag = 1;
                }
                else if (op2.IsChecked == true)
                {
                    temp2.answered2 = temp2.answered2 + 1;
                    flag = 2;
                }
                else if (op3.IsChecked == true)
                {
                    temp2.answered3 = temp2.answered3 + 1;
                    flag = 3;
                }
                else if (op4.IsChecked == true)
                {
                    temp2.answered4 = temp2.answered4 + 1;
                    flag = 4;
                }

                temp2.no_of_responses = temp2.no_of_responses + 1;

                await App.MobileService.GetTable<questions>().UpdateAsync(temp2);

                answers newans = new answers { id = Guid.NewGuid().ToString(), userid = qu.userid, questionid = str, answer = flag };
                await App.MobileService.GetTable<answers>().InsertAsync(newans);

                Frame.Navigate(typeof(RDisplay), str);
            }
            }
        }
    }
}
