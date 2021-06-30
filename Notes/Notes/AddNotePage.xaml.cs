using Notes.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
       
        public AddNotePage()
        {
            InitializeComponent();
            
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (string.IsNullOrEmpty(note.FileName))
            {
                note.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Path.GetRandomFileName()}.notes.txt");
            }
            File.WriteAllText(note.FileName, Editor.Text);
            await Navigation.PopModalAsync();

        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (File.Exists(note.FileName))
            {
                File.Delete(note.FileName);
            }
            await Navigation.PopModalAsync();

        }
        protected override void OnAppearing()
        {
            var note = (Note)BindingContext;
            if (!string.IsNullOrEmpty(note.FileName))
            {
                Editor.Text = File.ReadAllText(note.FileName);
            }
           

        }
    }
}