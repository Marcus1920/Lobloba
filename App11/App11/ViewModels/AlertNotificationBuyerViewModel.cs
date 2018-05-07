using Acr.UserDialogs;
using App11.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App11.Views.Buyers;
using Xamarin.Forms;
using App11.Models;

namespace App11.ViewModels
{
    public class AlertNotificationBuyerViewModel : INotifyPropertyChanged
    {
        public Command OpenOtherPageCommand { get; }
        INavigation Navigation;


        private List<AlertproductModel> usersCartList;


        public List<AlertproductModel> UsersCartList
        {
            get { return usersCartList; }

            set
            {
                usersCartList = value;
                OnpropertyChanged();

            }

        }

        public Command<AlertproductModel> RemoveComand

        {

            get
            {
                return new Command<AlertproductModel>(async (CartProduct) =>

                {

                    //UsersCartList.Remove(CartProduct);

                    try
                    {
                        if (CartProduct.active == "1")

                        {

                            UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                            var values = new FormUrlEncodedContent(new[]
                            {

                                new KeyValuePair<string, string>("id", CartProduct.id),


                            });

                            var client = new HttpClient();
                            var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/deactivateInterest",
                                values);
                            var respond = await response.Content.ReadAsStringAsync();
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowSuccess("Product was successfully Remove", 3000);
                            await OpenOtherPage();

                        }


                        else
                        {
                            UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
                            var values = new FormUrlEncodedContent(new[]
                            {

                                new KeyValuePair<string, string>("id", CartProduct.id),


                            });

                            var client = new HttpClient();
                            var response = await client.PostAsync("http://system.foodforus.cloud/api/v1/activateInterest",
                                values);
                            var respond = await response.Content.ReadAsStringAsync();
                            UserDialogs.Instance.HideLoading();
                            UserDialogs.Instance.ShowSuccess("Product was successfully  Updated", 3000);
                            await OpenOtherPage();

                        }


                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Iteme   fails");
                        //await DisplayAlert("Erro", " Connection interrupted.Please check your network status, refresh the page or try again later.", "OK");
                    }
                    finally
                    {

                        //  await DisplayAlert("Notification", "No Item found", "OK"); 
                    }



                });
            }


        }


        public AlertNotificationBuyerViewModel()

        {
            InitializeDataAsync();



        }



        public AlertNotificationBuyerViewModel(INavigation MainPageNav)

        {
            Navigation = MainPageNav;
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());

            InitializeDataAsync();



        }

        public async Task OpenOtherPage()
        {
            await Navigation.PushModalAsync(new BuyersHome());
        }



        private async Task InitializeDataAsync()
        {

            //  var userserve = new CaseServices();
            var CartProductDatas = new CallbackdataAlert();

            UsersCartList = await CartProductDatas.GetalertIntrestList();


        }



        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}