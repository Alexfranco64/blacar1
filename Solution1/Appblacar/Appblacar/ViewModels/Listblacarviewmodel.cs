using Appblacar.Models;
using Appblacar.Services;
using Appblacar.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Appblacar.ViewModels
{
    public class Listblacarviewmodel:BaseViewModel
    {
        // Comandos
        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        private Command _RefreshCommand;
        public Command RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new Command(RefreshViajes));
        private Command _LoadCommand;
        public Command LoadCommand => _LoadCommand ?? (_LoadCommand = new Command(LoadAction));



        private Command _SelectedCommand;
        public Command SelectedCommand => _SelectedCommand ?? (_SelectedCommand = new Command(SelectedAction));
        // Propiedades
        private List<ViajeModel> _Listblacar;
        public List<ViajeModel> Listblacar
        {
            get => _Listblacar;
            set => SetProperty(ref _Listblacar, value);
        }
        private ViajeModel _SelectedProduct;
        public ViajeModel SelectedProduct
        {
            get => _SelectedProduct;
            set => SetProperty(ref _SelectedProduct, value);
        }

        // Constructores
        public Listblacarviewmodel()
        {
            Loadblacar();
        }

        // Métodos
        private void LoadAction(object obj)
        {
            throw new NotImplementedException();
        }
        private void SelectedAction(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ViajePage (this, SelectedProduct));
        }
        private async void Loadblacar()
        {
            IsBusy = true;
            Listblacar = null;
            ApiResponse response = await new ApiServices().GetDataAsync("blacar");
            if (response == null || !response.IsSuccess)
            {
                // No hubo una respuesta exitosa
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Error al cargar los viajes: {response.Message}", "Ok");
                return;
            }
            Listblacar = JsonConvert.DeserializeObject<List<ViajeModel>>(response.Result.ToString());
            IsBusy = false;
        }
        public void RefreshViajes()
        {
            Loadblacar();
        }

        private void NewAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new ViajePage(this));
        }

    }
}
