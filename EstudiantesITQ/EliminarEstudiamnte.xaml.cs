using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstudiantesITQ
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EliminarEstudiamnte : ContentPage
    {

        private const string Url = "http://192.168.0.105/itq/post.php?codigo={0}";
        public EliminarEstudiamnte()
        {
            InitializeComponent();
        }

        private async void insertar_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var uri = new Uri(string.Format(Url, txtCodigo.Text));
                var result = await client.DeleteAsync(uri);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Dato Eliminado", "OK");
                    Limpiar();
                } else
                {
                    await DisplayAlert("Error", "Estudiante de codigo: " + txtCodigo.Text, "Cerrar");
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Se produjo un Errrrroooorr: ", "Cerrar");

            }
        }

        public void Limpiar()
        {
            txtCodigo.Text = string.Empty;

        }

        private async void btnRegreso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadoEstudiantes());
        }
    }
}