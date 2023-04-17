using MvcCoreApiCrudDoctores.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreApiCrudDoctores.Services
{
    public class ServiceApiDoctores
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceApiDoctores(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiCrudDoctores");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        //METODOS
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "/api/doctores";
            List<Doctor> doctores =
                await this.CallApiAsync<List<Doctor>>(request);
            return doctores;
        }

        public async Task<Doctor> FindDoctorAsync(int id)
        {
            string request = "/api/doctores/" + id;
            Doctor doctor =
                await this.CallApiAsync<Doctor>(request);
            return doctor;
        }



        //LOS METODOS DE ACCION NO SUELEN SER GENERICOS
        //YA QUE NORMALMENTE CADAUNO RECIBE DISTINTOS PARAMETROS
        public async Task DeleteDoctoresAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //NO NECESITA EL HEADER PORQUE NO DEVUELVE NADA
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
                //SI DESEAMOS PERSONALIZAR LA EXPERIENCIA DEVILCIENDO
                //ALGUN VALOR PARA LA PETICION
                //return response.StatusCode;
            }
        }

        public async Task InsertDepartamentosAsync(int id,int idhospital, string apellido, string especialidad, int salario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                //TENEMOS QUE ENVIAR UN OBJETO JSON
                //NOS CREAMOS UN OBJETO DE LA CLASE DEPARTAMENTO
                Doctor doctor = new Doctor()
                {
                    IdHospital = idhospital,
                    IdDoctor = id,
                    Apellido = apellido,
                    Especialidad = especialidad,
                    Salario = salario
                }; 
                

                //CONVERTIMOS EL OBJETO A JSON
                string json = JsonConvert.SerializeObject(doctor);
                //PARA ENVIAR DATOS (data) AL SERVICIO SE UTILIZA
                //LA CLAVE StringContent, DONDE DEBEMOS INDICAR
                //LOS DATOS , SU ENCODING Y SU TIPO
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync(int id, int idhospital, string apellido, string especialidad, int salario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                //TENEMOS QUE ENVIAR UN OBJETO JSON
                //NOS CREAMOS UN OBJETO DE LA CLASE DEPARTAMENTO
                Doctor doctor = new Doctor()
                {
                    IdHospital = idhospital,
                    IdDoctor = id,
                    Apellido = apellido,
                    Especialidad = especialidad,
                    Salario = salario
                };

                string json = JsonConvert.SerializeObject(doctor);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
