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
    public partial class AgregarEstudiante : ContentPage
    {

        private const string Url = "http://192.168.173.27/citamedica/post.php";

        public AgregarEstudiante()
        {
            InitializeComponent();
        }

        private void insertar_Clicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("cedulapaciente", entCodigo.Text);
                parameters.Add("nombrepaciente", entNombre.Text);
                parameters.Add("apellidopaciente", entApellido.Text);
                parameters.Add("correopaciente", entCorreo.Text);
                parameters.Add("telefonopaciente", entTelefono.Text);
                parameters.Add("direccionpaciente", entDireccion.Text);
                parameters.Add("estadopaciente", entEstado.Text);

                client.UploadValues(Url, "POST", parameters);
                Limpiar();
                DisplayAlert("Completado", "Paciente Registrado: " + entNombre.Text + " " + entApellido.Text, "Cerrar");
                

            } catch(Exception ex) 
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
                DisplayAlert("Error", "Se produjo un Errrrroooorr: ", "Cerrar");

            }
        }

        public void Limpiar()
        {
            entCodigo.Text = string.Empty;
            entNombre.Text = string.Empty;
            entApellido.Text = string.Empty;
            entCorreo.Text = string.Empty;
            entTelefono.Text = string.Empty;
            entDireccion.Text = string.Empty;
            entEstado.Text = string.Empty;
        }

        private async void btnRegreso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadoEstudiantes());
        }
    }
}