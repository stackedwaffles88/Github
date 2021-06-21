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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitnessApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    //create a global variable fitness class
 
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }
        public FitnessClass currentuser;
        private void FitnessButton_Click(object sender, RoutedEventArgs e)
        {
            //get the user inputted details in a class
            // navigate to the next page
            //check which option is selected - male or female
            Gender gen = Gender.female;
            if ((bool)MaleCheckBox.IsChecked) {
                gen = Gender.male;
                    }
            currentuser = new FitnessClass(NameInputTextBox.Text, EmailInputTextBox.Text, gen, double.Parse(WeightInputTextBox.Text), double.Parse(HeightInputTextBox.Text), int.Parse(AgeInputTextBox.Text));
            //set current fitnessdata
            AppManager.currentData = currentuser;
            this.Frame.Navigate(typeof(MainPage));
        }

        private void FitnessButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MaleCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
