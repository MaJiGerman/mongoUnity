using MongoDB.Bson;
using System;
using System.Collections.Generic;
public class UserData 
{
	public string nombre { set; get; }
	public string apellido { set; get; }
	public string direccion { set; get; }
	private string[] NOMBRES = new string[15]{"GERMAN", "JUAN", "PEDRO", "ANDRES", "SANTIAGO", "FELIPE", "TOMAS", "MATEO", "IRENE", "PATRICIA", "MARIA", "ANA", "LUCIA", "ELENA", "ISABEL"};
	private string[] APELLIDOS = new string[15]{"GARCIA", "GONZALEZ", "MARTINEZ", "RODRIGUEZ", "DIAZ", "JIMENEZ", "MATILLA", "RAMOS", "RUIZ", "ALONSO", "SERRANO", "MOLINA", "SUAREZ", "SANZ", "CASTRO"};
	private string[] TIPO_CALLE = new string[4]{"CALLE", "AVENIDA", "PASEO", "TRAVESIA"};
	private string[] NOMBRE_CALLE = new string[13]{"CASTAÑO", "ALMENDRO", "CEREZO", "ABEDUL", "ABETO", "FRESNO", "ACACIA", "OLMO", "SAUCE", "ENEBRO", "LIMONERO", "CEDRO", "SAUCE"};

	public UserData()
	{
		Random r = new Random();

		this.nombre = NOMBRES[r.Next(NOMBRES.Length)]; 
		this.apellido = APELLIDOS[r.Next(APELLIDOS.Length)];
		this.direccion = "";
		this.direccion += TIPO_CALLE[r.Next(TIPO_CALLE.Length)];
		this.direccion += " " + NOMBRE_CALLE[r.Next(NOMBRE_CALLE.Length)];
		this.direccion += " nº " + (1+r.Next(46));
	}

	public override string ToString()
	{
		string aux = "";
		aux += "nombre: " + nombre + Environment.NewLine;
		aux += "apellido: " + apellido + Environment.NewLine;
		aux += "direccion: " + direccion + Environment.NewLine;
		return aux;
	}
	
	public float getSize()
	{
		float size=0;
		size += nombre.Length * sizeof(Char);
		size += apellido.Length * sizeof(Char);
		size += direccion.Length * sizeof(Char);

		return size;
	}
}

