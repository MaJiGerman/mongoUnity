using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {

	
	// Variables Globales
	private DB_Script db;
	private string CodSer;
 	
	// Use this for initialization
	void Start () {
		System.Random random = new System.Random();
		int randomNumber = random.Next(0, 1000);
		CodSer = "UNITY_"+randomNumber;
		Debug.Log("CODSER: "+CodSer);

		db = new DB_Script();
		db.Init();

		//Model aux = db.BuscarUsuario("lul");
		//Debug.Log(aux.nombre);
		//Debug.Log("Creando Nuevo Usuario");
		//db.InsertarUsuario("UNITY");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.A))
        {
			Debug.Log("Creando Nuevo Usuario");
			db.InsertarUsuario(CodSer);
        }
		if (Input.GetKeyUp(KeyCode.S))
        {
			Debug.Log("Buscando Usuario");
			db.BuscarUsuario(CodSer);
        }
		if (Input.GetKeyUp(KeyCode.F))
        {
			Debug.Log("Aniadir elemento a lista Usuario");
			db.ActualizarUsuario(CodSer,"ANADIDO");
        }
		if (Input.GetKeyUp(KeyCode.G))
        {
			Debug.Log("Borrar elemento a lista Usuario");
			db.BorrarListaUsuario(CodSer,"BORRADO");
        }
		if (Input.GetKeyUp(KeyCode.D))
        {
			Debug.Log("Borrando Usuario");
			db.DeleteUsuario(CodSer);
        }
	}
}
