using System;
using System.Data;
using System.Globalization;

namespace CrudWPF.Functions
{
	class util
	{
		public static double Miller(double weigth, string sex, double heigth)
		{
			double w_miller;
			if (sex == "H") w_miller = 50 + 0.555 * (heigth * 100 - 152.4);
			else w_miller = 45.5 + 0.535 * (heigth * 100 - 152.4);

			return System.Math.Round(w_miller,2);
		}

		//Definir dataview para agregar la clumna de edad y peso ideal.......
		public static DataView DataExtention(DataView dv)
		{
			dv.Table.Columns.Add("edad", typeof(int));
			dv.Table.Columns.Add("peso-ideal", typeof(double));

			foreach (DataRow row in dv.Table.Rows)
			{
				DateTime FechaNacimiento = Convert.ToDateTime(DateTime.ParseExact(
				row["fechaNacimiento"].ToString(),
				"dd/MM/yyyy",
				CultureInfo.InvariantCulture
				));

				row["edad"] = (DateTime.Today - FechaNacimiento).TotalDays / 365;

				double peso = double.Parse(row["peso"].ToString(), CultureInfo.InvariantCulture);
				double altura = double.Parse(row["altura"].ToString(), CultureInfo.InvariantCulture);

				row["peso-ideal"] = util.Miller(peso, row["sexo"].ToString(), altura);
			}

			return dv;
		}

	}
}
