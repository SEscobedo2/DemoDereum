using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudWPF.Shared
{
	class RestHelper
	{
		
		private static readonly string apiURL = "https://crudapisd.azurewebsites.net/api/";

		//Obtener lista de registros
		public static async Task<string> GetALL()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people")) { 

					using(HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener lista filtrada por tipo de persona
		public static async Task<string> GetByCategory(string category)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/category/" + category))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener lista filtrada por zona
		public static async Task<string> GetByZone(string zone)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/zone/" + zone))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener lista personas con sobrepeso
		public static async Task<string> GetByOverW()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/overweight"))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener lista de mujeres con bajo peso
		public static async Task<string> GetByWomenLow()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/lowweightwomen"))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener lista filtrada por tipo de persona obesa
		public static async Task<string> GetObese(string category)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/obese/" + category))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener estadísticas
		public static async Task<string> GetStatistics()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/statistics"))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Obtener registro específico
		public static async Task<string> Get(Guid id)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.GetAsync(apiURL + "people/" + id.ToString()))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Agregar registro nuevo
		public static async Task<string> Post(Guid id, 
			string nombre, 
			string apellido, 
			string fechaNacimiento, 
			string altura, 
			string peso, 
			string sexo, 
			string zona)
		{
			
			var jsonString = $"{{\"nombre\":\"{nombre}\"," +
				$"\"apellido\":\"{apellido}\"," +
				$"\"fechaNacimiento\":\"{fechaNacimiento}\"," +
				$"\"altura\":\"{altura}\"," +
				$"\"peso\":\"{peso}\"," +
				$"\"sexo\":\"{sexo}\"," +
				$"\"zona\":\"{zona}\"}}";
			var jsonObject = JObject.Parse(jsonString);

			var input = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
		
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.PostAsync(apiURL + "people", input))
				{

					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Editar
		public static async Task<string> Put(Guid id,
			string nombre,
			string apellido,
			string fechaNacimiento,
			string altura,
			string peso,
			string sexo,
			string zona)
		{
			var jsonString = $"{{\"nombre\":\"{nombre}\"," +
				$"\"apellido\":\"{apellido}\"," +
				$"\"fechaNacimiento\":\"{fechaNacimiento}\"," +
				$"\"altura\":\"{altura}\"," +
				$"\"peso\":\"{peso}\"," +
				$"\"sexo\":\"{sexo}\"," +
				$"\"zona\":\"{zona}\"}}";
			var jsonObject = JObject.Parse(jsonString);

			var input = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{	
				
				using (HttpResponseMessage res = await client.PutAsync(apiURL + "people/" + id.ToString(), input))
				{
					using (HttpContent content = res.Content)
					{
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							return data;
						}
					}
				}
			}

			return string.Empty;
		}

		//Borrar
		public static async Task<string> Delete(string id)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.DeleteAsync(apiURL + "people/" + id))
				{

					using (HttpContent content = res.Content)
					{
						
						string data = await content.ReadAsStringAsync();
						if (data != null)
						{
							MessageBox.Show("Elemento borrado con éxito: " + ((int)res.StatusCode).ToString() + "-" + res.StatusCode.ToString());
							return data;
						}
						else
						{
							MessageBox.Show(((int)res.StatusCode).ToString() + "-" + res.StatusCode.ToString());
						}


					}
				}
			}

			return string.Empty;
		}
	}
}
