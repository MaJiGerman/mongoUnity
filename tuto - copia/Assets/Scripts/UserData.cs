using MongoDB.Bson;
using System;
using System.Collections.Generic;
public class UserData 
{
	public string nombre { set; get; }
	public string apellido { set; get; }
	public string direccion { set; get; }

	public override string ToString(){
		string aux = "";
		aux += "nombre: " + nombre + Environment.NewLine;
		aux += "apellido: " + apellido + Environment.NewLine;
		aux += "direccion: " + direccion + Environment.NewLine;
		return aux;
	}
}
