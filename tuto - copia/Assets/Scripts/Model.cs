using MongoDB.Bson;
using System;
using System.Collections.Generic;
public class Model 
{
	public ObjectId _id;

	public string nombre { set; get; }
	public string estado { set; get; }
	public string color { set; get; }

	public List<string> lista { set; get; }

	public Model()
	{
		lista = new List<string>();
		lista.Add("primero");
	}

	public override string ToString(){
		string aux = "";
		aux += "Id: " + _id + Environment.NewLine;
		aux += "nombre: " + nombre + Environment.NewLine;
		aux += "estado: " + estado + Environment.NewLine;
		aux += "color: " + color + Environment.NewLine;
		aux += "lista: " + Environment.NewLine;
		if(lista != null)
		{
			foreach(string s in lista)
			{
				aux += s + Environment.NewLine;
			}
		}
		
		return aux;
	}
}
