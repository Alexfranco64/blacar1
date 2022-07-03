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
    public partial class Listblacar : ContentPage
    {
        public Listblacar()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Listblacarviewmodel();
        }
    }
}