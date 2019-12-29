using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour 
{

	
	// Variables Globales
	private DB_Script db;
	public string CodSer;
	public bool activo;
 	public float speed = 1.5f;
	private int HP;
	// Use this for initialization
	void Start () 
	{
		System.Random random = new System.Random();
		int randomNumber = random.Next(0, 1000);
		CodSer = "UNITY_"+randomNumber;
		activo = true;
		HP = 1;
		Debug.Log("CODSER: "+CodSer+" esta ahora activo");

		db = new DB_Script();
		db.Init();

		Debug.Log("Creando Nuevo Usuario");
		db.InsertarUsuario(CodSer);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activo && Input.GetKeyUp(KeyCode.A))
        {
			Debug.Log("Creando Nuevo Usuario");
			db.InsertarUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.S))
        {
			Debug.Log("Buscando Usuario");
			db.BuscarUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.F))
        {
			Debug.Log("Aniadir elemento a lista Usuario");
			db.ActualizarUsuario(CodSer,"ANADIDO");
			HP = db.GetDataLenght(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.G))
        {
			Debug.Log("Borrar elemento a lista Usuario");
			db.BorrarListaUsuario(CodSer,"BORRADO");
			HP = db.GetDataLenght(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.D))
        {
			Debug.Log("Borrando Usuario");
			db.DeleteUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp("space"))
        {
			activo = false;
			Debug.Log("CODSER: "+CodSer+" going DARK");
        }
		//COMPROBAR VIDA
		if (HP<=0)
		{
			Debug.Log("AUTODESTROYENDOSE");
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision col)
    {
		if(col.gameObject.layer == 9)
		{
        	Debug.Log("Borrar elemento a lista Usuario");
			db.BorrarListaUsuario(CodSer,"BORRADO");
			HP = db.GetDataLenght(CodSer);
			Debug.Log("HP: "+HP);
		}
    }
	public void activarJugador(bool activar)
	{
		if(activar)
			this.activo=true;
		else
			this.activo=false;
	}
}
