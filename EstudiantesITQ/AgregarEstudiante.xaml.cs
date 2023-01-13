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

        private const string Url = "http://192.168.1.11/itq/post.php";

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
                parameters.Add("codigo", entCodigo.Text);
                parameters.Add("nombre", entNombre.Text);
                parameters.Add("apellido", entApellido.Text);
                parameters.Add("correo", entCorreo.Text);
                parameters.Add("telefono", entTelefono.Text);

                client.UploadValues(Url, "POST", parameters);
                DisplayAlert("Completado", "Estudiante Registrado: " + entNombre + " " + entApellido, "Cerrar");
                Limpiar();

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
        }

        private async void btnRegreso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListadoEstudiantes());
        }
    }
}