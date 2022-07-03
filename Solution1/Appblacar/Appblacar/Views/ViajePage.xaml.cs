using Appblacar.Models;
using Appblacar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appblacar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViajePage : ContentPage
    {
        private Listblacarviewmodel listblacarviewmodel;
  

        public ViajePage(Listblacarviewmodel listblacarviewmodel)
        {
     
        InitializeComponent();

            BindingContext = new ViajeViewModel( listblacarviewmodel);
        }
        public ViajePage(Listblacarviewmodel ViajeViewModel, ViajeModel blacar)
        {
            InitializeComponent();

            BindingContext = new ViajeViewModel(ViajeViewModel, blacar);
        }
    }
}