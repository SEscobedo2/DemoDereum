using CrudWPF.Shared;
using System.Windows;

namespace CrudWPF
{
	/// <summary>
	/// Lógica de interacción para Estadísticas.xaml
	/// </summary>
	public partial class Estadísticas : Window
	{
		public Estadísticas()
		{
			InitializeComponent();
			Refresh();
		}
		private async void Refresh()
		{
			txtStatistics.Text = "Cargando datos...";

			string statistics = await RestHelper.GetStatistics();

			txtStatistics.Text = statistics;

		}
	}
}
