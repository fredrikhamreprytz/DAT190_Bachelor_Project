using System;
using DAT190_Bachelor_Project.Model;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class EmissionDetailsPage : ContentPage
    {
        ObservableCollection<DataSource> dataSourceList = new ObservableCollection<DataSource>();

        public EmissionDetailsPage(IEmission emission)
        {
            InitializeComponent();
            DataSourceListView.ItemsSource = dataSourceList;
            Title = emission.Type.ToString() + "utslipp";
            dataSourceList.Add(new DataSource(DateTime.Now, "Some datasource", 23.98, EmissionType.Flight));
            dataSourceList.Add(new DataSource(DateTime.Now, "Some other datasource", 89.43, EmissionType.Flight));
            dataSourceList.Add(new DataSource(DateTime.Now, "Some third datasource", 219.91, EmissionType.Flight));
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DataSource ds = (DataSource)mi.CommandParameter;
            Navigation.PushModalAsync(new EditDataSourcePage(ds));
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}
