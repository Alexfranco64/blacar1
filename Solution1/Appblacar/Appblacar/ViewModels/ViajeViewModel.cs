using Appblacar.Models;
using Appblacar.Services;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Appblacar.ViewModels
{
    public class ViajeViewModel : BaseViewModel
    {
        // Variables locales
        public readonly Listblacarviewmodel Listblacar;
        // Commandos
        private Command _CancelCommand;
        public Command CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(CancelAction));

        private Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        private Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        // Propiedades
        private ViajeModel _ViajeSelected;
        public ViajeModel TaskSelected
        {
            get => _ViajeSelected;
            set => SetProperty(ref _ViajeSelected, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }
        private int _ViajeID;
        public int ViajeID
        {
            get => _ViajeID;
            set => SetProperty(ref _ViajeID, value);
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
        private string _Orige;
        public string Orige
        {
            get => _Orige;
            set => SetProperty(ref _Orige, value);
        }
        private string _Destino;
        public string Destino
        {
            get => _Destino;
            set => SetProperty(ref _Destino, value);
        }
        private string _ImageBase64;
        public string ImageBase64
        {
            get => _ImageBase64;
            set => SetProperty(ref _ImageBase64, value);
        }
        private int _NoPasajeros;
        public int NoPasajeros
        {
            get => _NoPasajeros;
            set => SetProperty(ref _NoPasajeros, value);
        }
        private DateTime _DateTime;
        public DateTime DateTime
        {
            get => _DateTime;
            set => SetProperty(ref _DateTime, value);
        }
        private double _Tarifa;
        public double Tarifa
        {
            get => _Tarifa;
            set => SetProperty(ref _Tarifa, value);
        }
        private bool _ViajeRedondo;
        public bool ViajeRedondo
        {
            get => _ViajeRedondo;
            set => SetProperty(ref _ViajeRedondo, value);
        }
        // Constructor
        public ViajeViewModel(Listblacarviewmodel listblacarviewmodel)
        {
            this.Listblacar = listblacarviewmodel;
        }
        public ViajeViewModel(Listblacarviewmodel listblacarviewmodel, ViajeModel blacar)
        {
            this.Listblacar = listblacarviewmodel;
           ViajeID = blacar.ID;
           Name = blacar.Name;
            Latitude = blacar.Latitud;
            Longitude = blacar.Longitud;
            DateTime = blacar.DateTime;
            Orige = blacar.Origen;
            Destino = blacar.Destino;
            ViajeRedondo= blacar.ViajeRedondo;
            Tarifa = blacar.Tarifa;
            ImageBase64= blacar.ImageBase64;
            NoPasajeros= blacar.NoPasajeros;

        }
        // Métodos
        private async void CancelAction(object obj)
        {
            // Regresamos al listado
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void SaveAction(object obj)
        {
            ApiResponse response;
            try
            {
                // Iniciamos el spinner
                IsBusy = true;

                // Creamos el modelo con los datos de los controles
                ViajeModel model = new ViajeModel
                {
                    ID = ViajeID,
                    Name = Name,
                    Latitud = Latitude,
                    Longitud = Longitude,
                    ImageBase64 = ImageBase64,
                    Destino = Destino,
                    Origen = Orige,
                    NoPasajeros = NoPasajeros,
                    DateTime = DateTime,
                    Tarifa = Tarifa,
                    ViajeRedondo = ViajeRedondo,
                };

                if (model.ID == 0)
                {
                    // Crear un nuevo producto
                  
                    response = await new ApiServices().PostDataAsync("blacar",model);
                }
                else
                {
                    // Actualizar un producto existente
        
                    response = await new ApiServices().PutDataAsync("blacar", model);
                }

                // Si no fue satisfactorio enviamos un mensaje y terminamos el método
                if (response == null || !response.IsSuccess)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Appblacar", response.Message, "Ok");
                    return;
                }

                // Actualizamos el listado de productos
                Listblacar.RefreshViajes();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Se generó un error al guardar el viaje: {ex.Message}", "Ok");
            }
        } 
        private async void DeleteAction(object obj)
        {
            ApiResponse response;
            try
            {
                // Iniciamos el spinner
                IsBusy = true;

                // Creamos el modelo con los datos de los controles

                {
                    // eliminar un nuevo producto
                   
                   response = await new ApiServices().DeleteDataAsync("blacar",ViajeID);

                }

                // Actualizamos el listado de productos
                Listblacar.RefreshViajes();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();


                // Actualizamos el listado de productos
                Listblacar.RefreshViajes();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Appblacar", $"Se generó un error al eliminar el producto: {ex.Message}", "Ok");
            }
        }
        private async void TakePictureAction(object obj)
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();

            // Si la cámara no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // Toma la fotografía y la regresa en el objeto file
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppSQLite",
                Name = "cam_picture.jpg"
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
           // Picture = TaskSelected.ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path); //file.Path;

        }
        private async void SelectPictureAction(object obj)
        {
            await CrossMedia.Current.Initialize();

            // Si el seleccionar fotografía no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Pick Photo", ":( No pick photo available.", "OK");
                return;
            }

            // Selecciona la fotografía del carrete y la regresa en el objeto file
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            // Si el objeto file es null termina este método
            if (file == null) return;

            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            //Picture = TaskSelected.ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);  //file.Path;

        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
