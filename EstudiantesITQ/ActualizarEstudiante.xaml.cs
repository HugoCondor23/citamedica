using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstudiantesITQ
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActualizarEstudiante : ContentPage
    {
        public const string Url = "http://192.168.173.27/citamedica/post.php?cedulapaciente={0}&nombrepaciente={1}&apellidopaciente={2}&correopaciente={3}&telefonopaciente={4}&direccionpaciente={5}&estadopaciente={6}";


        public ActualizarEstudiante(int cedulapaciente, string nombrepaciente, string apellidopaciente, string correopaciente, string telefonopaciente, string direccionpaciente, string estadopaciente)
        {
            InitializeComponent();
            entCodigo.Text= cedulapaciente.ToString();
            entNombre.Text= nombrepaciente.ToString();
            entApellido.Text= apellidopaciente.ToString();
            entCorreo.Text= correopaciente.ToString();
            entTelefono.Text= telefonopaciente.ToString();
            entDireccion.Text = direccionpaciente.ToString();
            entEstado.Text = estadopaciente.ToString();
        }

        private void actualizar_Clicked(object sender, EventArgs e)
        {

            WebClient client = new WebClient();
            try 
            {
                using (var webClient = new WebClient())
                {
                    var uri = new Uri(string.Format(Url,
                    entCodigo.Text, entNombre.Text, entApellido.Text, entCorreo.Text, entTelefono.Text,entDireccion.Text,entEstado.Text));
                    webClient.UploadString(uri, "PUT", string.Empty);

                    DisplayAlert("Success", "Registro de: " + entNombre.Text + " " + entApellido.Text + " actualizado correctamente", "Cerrar");
                }
                
            
            } catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }
        }

        private async void btnRegreso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadoEstudiantes());
        }
    }
}