using CrudWPF.Shared;
using CrudWPF.Functions;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;


namespace CrudWPF
{
	/// <summary>
	/// Interação lógica para MenuLista.xam
	/// </summary>
	/// 


	public partial class MenuLista : Page
	{
		
		public MenuLista()
		{
			InitializeComponent();
			Refresh();
		}

		private async void Refresh()
		{
			LBL.Content = "Cargando datos...";

			string jsonString = await RestHelper.GetALL();
		
			DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);

			DG.ItemsSource = util.DataExtention(dt.DefaultView);

			LBL.Content = "";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			
			MainWindow.StaticMainFrame.Content = new Formulario(Guid.Empty);
			
		}

		private async void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string id = ((Button)sender).CommandParameter.ToString();
				
			await RestHelper.Delete(id);

			Refresh();
		}

		private void Button_Edit(object sender, RoutedEventArgs e)
		{
			Guid id = new Guid(((Button)sender).CommandParameter.ToString());

			Formulario pFormulario = new Formulario(id);
			

			MainWindow.StaticMainFrame.Content = pFormulario;

		}

		private void Button_Statistics(object sender, RoutedEventArgs e)
		{
			Estadísticas subWindow = new Estadísticas();
			subWindow.Show();

		}

		private async void cmboConsultas_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			LBL.Content = "Cargando datos...";

			if (cmboConsultas.SelectedIndex > -1 && cmboConsultas.SelectedIndex < 4) //Filtrar por tipo de persona	
			{
				string[] category = new string[] { "Niño", "Joven", "Adulto", "Tercera-edad" };
				string selectedCategory = "";

				selectedCategory = category[cmboConsultas.SelectedIndex];

				string jsonString = await RestHelper.GetByCategory(selectedCategory);

				DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
				if ( dt != null) DG.ItemsSource = util.DataExtention(dt.DefaultView);

				
			}

			else if (cmboConsultas.SelectedIndex > 3 && cmboConsultas.SelectedIndex < 7) //Filtrar por zona
			{
				string[] zone = new string[] {"Norte", "Centro", "Sur" };
				string selectedZone = "";

				selectedZone = zone[cmboConsultas.SelectedIndex - 4];

				string jsonString = await RestHelper.GetByZone(selectedZone);

				DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
				if (dt != null)  DG.ItemsSource = util.DataExtention(dt.DefaultView);
			}

			else if (cmboConsultas.SelectedIndex == 7) //Filtrar personas con sobrepeso
			{
				string jsonString = await RestHelper.GetByOverW();

				DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
				if (dt != null)  DG.ItemsSource = util.DataExtention(dt.DefaultView);
			}

			else if (cmboConsultas.SelectedIndex == 8) //Filtrar mujeres con bajo peso
			{
				string jsonString = await RestHelper.GetByWomenLow();

				DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
				if (dt != null) DG.ItemsSource = util.DataExtention(dt.DefaultView);
			}
			else if (cmboConsultas.SelectedIndex == 9) //Filtrar por niños obesos
			{
				
				string jsonString = await RestHelper.GetObese("Niño");

				DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonString);
				if (dt != null) DG.ItemsSource = util.DataExtention(dt.DefaultView);

			}
			else
			{
				Refresh();
			}

			LBL.Content = "";
		}

	}

}
