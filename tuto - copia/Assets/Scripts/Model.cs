﻿using MongoDB.Bson;
using System;
using System.IO;
using System.Collections.Generic;

public class Model 
{
	public ObjectId _id;

	public string nombre { set; get; }
	public string estado { set; get; }
	public string color { set; get; }
	public List<UserData> datos { set; get; }
	// Big Data
	public byte[] imagen { get; set; }

	public Model()
	{
		datos = new List<UserData>();
		UserData u = new UserData();
		datos.Add(u);
		
		imagen = System.IO.File.ReadAllBytes("Assets/Images/Pokemon.jpg");
	}

	public override string ToString(){
		string aux = "";
		aux += "Id: " + _id + Environment.NewLine;
		aux += "nombre: " + nombre + Environment.NewLine;
		aux += "estado: " + estado + Environment.NewLine;
		aux += "color: " + color + Environment.NewLine;
		aux += "datos: " + Environment.NewLine;
		if(datos != null)
		{
			foreach(UserData d in datos)
			{
				aux += d.ToString();
				aux += Environment.NewLine;
			}
		}
		
		return aux;
	}
	public float getSize()
	{
		float size=0;
		if(datos != null)
		{
			foreach(UserData d in datos)
			{
				size += d.getSize();
			}
		}
		size += nombre.Length * sizeof(Char);
		size += estado.Length * sizeof(Char);
		size += color.Length * sizeof(Char);

		size += imagen.Length;
		return size;
	}
}
