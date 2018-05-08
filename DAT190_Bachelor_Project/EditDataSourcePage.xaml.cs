using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DAT190_Bachelor_Project.Model;

namespace DAT190_Bachelor_Project
{
    public partial class EditDataSourcePage : ContentPage
    {

        DataSource DataSource;

        public EditDataSourcePage(DataSource dataSource)
        {
            InitializeComponent();
            this.DataSource = dataSource;
            DataSourceDescriptionEntry.Text = dataSource.Description;
            DataSourceAmountEntry.Text = dataSource.Value.ToString();
            DataSourceDateEntry.Date = dataSource.Timestamp;
        }

        void CancelButtonClicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void SaveButtonClicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }


    }
}
