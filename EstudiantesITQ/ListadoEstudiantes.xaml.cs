using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EstudiantesITQ
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoEstudiantes : ContentPage
    {
        private const string Url = "http://192.168.173.27/citamedica/post.php";

        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;
        public int cedulapaciente = -1;
        public string nombrepaciente, apellidopaciente, correopaciente, telefonopaciente,direccionpaciente,estadopaciente;

        private async void btnElimiarEstudiante_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EliminarEstudiamnte());

        }

        private void lstEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Datos)e.SelectedItem;
            cedulapaciente = obj.cedulapaciente;
            nombrepaciente = obj.nombrepaciente;
            apellidopaciente = obj.apellidopaciente;
            correopaciente = obj.correopaciente;
            telefonopaciente = obj.telefonopaciente;
            direccionpaciente= obj.direccionpaciente;
            estadopaciente= obj.estadopaciente; 
        }

        private async void btnCita_Clicked(object sender, EventArgs e)
        {
            if (cedulapaciente > 0)
            {
                await Navigation.PushAsync(new AgregarCita(cedulapaciente));
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro", "OK");
            }
        }

       

        private async void btnActualizarEstudiante_Clicked(object sender, EventArgs e)
        {
            if (cedulapaciente > 0)
            {
                await Navigation.PushAsync(new ActualizarEstudiante(cedulapaciente, nombrepaciente, apellidopaciente, correopaciente, telefonopaciente,direccionpaciente,estadopaciente));
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro", "OK");
            }
        }

        private async void btnEliminarId_Clicked(object sender, EventArgs e)
        {
            if (cedulapaciente > 0)
            {
                string Uri = "http://192.168.173.27/citamedica/post.php?cedulapaciente={0}";
                try
                {

                    HttpClient client = new HttpClient();
                    var uri = new Uri(string.Format(Uri, cedulapaciente.ToString()));
                    var result = await client.DeleteAsync(uri);
                    if (result.IsSuccessStatusCode)
                    {
                        Get();
                        await DisplayAlert("Success", "Paciente: " + nombrepaciente + " " + apellidopaciente + " eliminado", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error consulte con el administrador", "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta", "Ocurrio un Error", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro", "OK");
            }
        }


        private async void btnNuevoEstudiante_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarEstudiante());
        }

        public ListadoEstudiantes()
        {
            InitializeComponent();
            Get();

        }

        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Datos> post =
                    JsonConvert.DeserializeObject<List<Datos>>(content);
                _post = new ObservableCollection<Datos>(post);
                lstEstudiantes.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}