using CrudWPF.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CrudWPF
{

	public partial class Formulario : Page
	{
		public Guid Id { get; set; }

		public Formulario(Guid Id)
		{
			InitializeComponent();
			this.Id = Id;

			if (this.Id != Guid.Empty)
			{
				txblkCurp.Text = "Fecha Nacimiento";
				FillData(this.Id);
			}
			else
			{
				txblkCurp.Text = "CURP";
			}
		}

		private async void FillData(Guid Id)
		{
			string jsonString = await RestHelper.Get(Id);
			var jsonObject = JObject.Parse(jsonString);

			txtNombre.Text = jsonObject["nombre"].ToString();
			txtApellido.Text = jsonObject["apellido"].ToString();
			txtCurp.Text = jsonObject["fechaNacimiento"].ToString();
			txtAltura.Text = jsonObject["altura"].ToString();
			txtPeso.Text = jsonObject["peso"].ToString();
			txtSexo.Text = jsonObject["sexo"].ToString();
			txtZona.Text = jsonObject["zona"].ToString();
		}

		public async void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Id == Guid.Empty) {
				//Nuevo registro

				var Nombre = txtNombre.Text;

				if (Nombre != "")
				{
					var response = await RestHelper.Post(Id, Nombre,
						txtApellido.Text,
						CURPtoDate(txtCurp.Text),
						txtAltura.Text,
						txtPeso.Text,
						txtSexo.Text,
						txtZona.Text
						);
				}
				else
				{
					MessageBox.Show("El nombre es requirido, el registro no se guardará");
				}


				MainWindow.StaticMainFrame.Content = new MenuLista();

			}
			else
			{
				//Editando el registro existente

				var Nombre = txtNombre.Text;

				if (Nombre != "")
				{
					var response = await RestHelper.Put(Id, Nombre,
						txtApellido.Text,
						txtCurp.Text,
						txtAltura.Text,
						txtPeso.Text,
						txtSexo.Text,
						txtZona.Text
						);
				}
				else
				{
					MessageBox.Show("El campo es requirido, el registro no se guardará");
				}


				MainWindow.StaticMainFrame.Content = new MenuLista();
			}
		}

		private string CURPtoDate(string curp)
		{
			string fechaNacimiento = "";
			
			//Los lugares 4-10 del curp contienen la fecha de nacimiento
			string year = curp.Substring(4, 2);
			string month = curp.Substring(6, 2);
			string day = curp.Substring(8, 2);
			string dec = "19";

			if (Convert.ToInt32(year) < 40) dec = "20";

			if (curp.Length > 10)	
			 fechaNacimiento = string.Format("{2}/{1}/{3}{0}", year, month, day, dec);
			
			return fechaNacimiento;
		}
	}
}
