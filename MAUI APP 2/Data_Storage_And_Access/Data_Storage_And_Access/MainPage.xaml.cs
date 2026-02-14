using System;
using System.Collections.ObjectModel;
using Data_Storage_And_Access.DataAccess;
using Data_Storage_And_Access.Models;

namespace Data_Storage_And_Access
{
    public partial class MainPage : ContentPage
    {
        PersonData personData;
        public ObservableCollection<Person> People { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            personData = new PersonData();
            BindingContext = this;
            UpdatePeopleList();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfirstName.Text)) { await DisplayAlert("Error", "First name cannot be empty", "OK"); return; }
            if (string.IsNullOrWhiteSpace(txtlastName.Text)) { await DisplayAlert("Error", "Last name cannot be empty", "OK"); return; }
            if (dpDateOfBirth.Date > DateTime.Today) { await DisplayAlert("Error", "Date of Birth cannot be in the future", "OK"); return; }
            var person = new Person
            {
                FirstName = txtfirstName.Text,
                LastName = txtlastName.Text,
                DoB = (DateTime)dpDateOfBirth.Date
            };
            await personData.SavePersonAsync(person);

            txtfirstName.Text = string.Empty;
            txtlastName.Text = string.Empty;
            
            UpdatePeopleList();
        }
        private async void UpdatePeopleList()
        {
            var people = await personData.GetPeopleAsync();

            People.Clear();
            
            foreach (var person in people)
            {
                People.Add(person);
            }
        }
        private async void OnClearClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Reset", "Delte all entries?", "Yes", "No");
            if (confirm)
            {
                await personData.ClearAllPeropleAsync();
                UpdatePeopleList();
                await DisplayAlert("Success", "Database cleared", "OK");
            }
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedPerson = e.CurrentSelection[0] as Person;
                if (selectedPerson != null)
                {
                    txtfirstName.Text = selectedPerson.FirstName;
                    txtlastName.Text = selectedPerson.LastName;
                    dpDateOfBirth.Date = selectedPerson.DoB;
                }
            }
        }
    }
}
