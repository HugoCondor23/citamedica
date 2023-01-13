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
    public partial class AgregarCita : ContentPage
    {
        private const string Url = "http://192.168.173.27/citamedica/post1.php";
        public AgregarCita(int cedulacedula)
        {
            InitializeComponent();
            entCedulaPaciente.Text = cedulacedula.ToString();
        }

        private void insertar_Clicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("idcita", entIdCita.Text);
                parameters.Add("cedulapaciente", entCedulaPaciente.Text);
                parameters.Add("nombremedico", entNombreMedico.Text);
                parameters.Add("especialidad", entEspecialidad.Text);
                parameters.Add("fechacita", entFecha.Text);
                parameters.Add("horacita", entHora.Text);
                

                client.UploadValues(Url, "POST", parameters);
                
                DisplayAlert("Completado", "Cédula del paciente: " + entCedulaPaciente.Text + " Nombre Médico " + entNombreMedico.Text, "Cerrar");
                Limpiar();

            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
                DisplayAlert("Error", "Se produjo un Errrrroooorr: ", "Cerrar");

            }
        }

        public void Limpiar()
        {
            entIdCita.Text = string.Empty;
            entCedulaPaciente.Text = string.Empty;
            entNombreMedico.Text = string.Empty;
            entEspecialidad.Text = string.Empty;
            entFecha.Text = string.Empty;
            entHora.Text = string.Empty;
            
        }
    }
}