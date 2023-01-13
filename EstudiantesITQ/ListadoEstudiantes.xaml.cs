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
        private const string Url = "http://192.168.1.11/itq/post.php";

        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;
        public int codigo = -1;
        public string nombre, apellido, correo, telefono;

        private async void btnElimiarEstudiante_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EliminarEstudiamnte());

        }

        private void lstEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Datos)e.SelectedItem;
            codigo = obj.codigo;
            nombre = obj.nombre;
            apellido = obj.apellido;
            correo = obj.correo;
            telefono = obj.telefono;
        }

        private async void btnActualizarEstudiante_Clicked(object sender, EventArgs e)
        {
            if (codigo > 0)
            {
                await Navigation.PushAsync(new ActualizarEstudiante(codigo, nombre, apellido, correo, telefono));
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro", "OK");
            }
        }

        private async void btnEliminarId_Clicked(object sender, EventArgs e)
        {
            if (codigo > 0)
            {
                string Uri = "http://192.168.1.11/itq/post.php?codigo={0}";
                try
                {

                    HttpClient client = new HttpClient();
                    var uri = new Uri(string.Format(Uri, codigo.ToString()));
                    var result = await client.DeleteAsync(uri);
                    if (result.IsSuccessStatusCode)
                    {
                        Get();
                        await DisplayAlert("Success", "Estudiante: " + nombre + " " + apellido + " eliminado", "OK");
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