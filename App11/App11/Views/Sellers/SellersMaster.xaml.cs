﻿using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Sellers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersMaster : MasterDetailPage
    {
        public SellersMaster()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SellersMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            if (item.Title.Equals("Logout"))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await DisplayAlert("Logout?", "Are you sure you want to logout?", "Yes", "No"))
                    {
                        CrossSettings.Current.AddOrUpdateValue("userlogin", "false");
                        await Navigation.PopModalAsync();
                    }

                });
            }
            else
                Detail.Navigation.PushAsync(page);

            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}