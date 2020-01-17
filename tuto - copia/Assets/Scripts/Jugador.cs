using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour 
{
	// Variables Globales
	private DB_Script db;
	public string CodSer;
	public string color;
	public bool activo;
	private int HP;
	private float size;

	// Use this for initialization
	void Start () 
	{
		color = LayerMask.LayerToName(this.gameObject.layer);
		System.Random random = new System.Random();
		int randomNumber = random.Next(0, 2048);
		CodSer = "UNITY_"+randomNumber;
		activo = false;
		HP = 3;
		size = 0.05f;
		//Debug.Log("CODSER: "+CodSer+" esta ahora activo");

		db = new DB_Script();
		db.Init();

		//Debug.Log("Creando Nuevo Usuario");
		db.InsertarUsuario(CodSer);
		db.ActualizarColorUsuario(CodSer, color);
		for (int i=1;i<HP;i++)
		{
			db.AniadirListaUsuario(CodSer, "CREADO");
		}
		//Debug.Log(db.ObtenerLongitudLista(CodSer));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activo && Input.GetKeyUp(KeyCode.A))
        {
			//Debug.Log("Creando Nuevo Usuario");
			db.InsertarUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.S))
        {
			//Debug.Log("Buscando Usuario");
			db.BuscarUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.F))
        {
			//Debug.Log("Aniadir elemento a lista Usuario");
			db.ActualizarUsuario(CodSer,"ANADIDO");
			HP = db.ObtenerLongitudLista(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.G))
        {
			//Debug.Log("Borrar elemento a lista Usuario");
			db.BorrarListaUsuario(CodSer,"BORRADO");
			HP = db.ObtenerLongitudLista(CodSer);
        }
		if (activo && Input.GetKeyUp(KeyCode.D))
        {
			//Debug.Log("Borrando Usuario");
			db.BorrarUsuario(CodSer);
        }
		if (activo && Input.GetKeyUp("space"))
        {
			activo = false;
			Debug.Log("CODSER: "+CodSer+" going DARK");
        }

	}
	void OnCollisionEnter(Collision col)
    {
		if(true){ //si existe la instancia
			if(this.gameObject.layer == 10 && col.gameObject.layer == 9
				|| this.gameObject.layer == 9 && col.gameObject.layer == 11 
				|| this.gameObject.layer == 11 && col.gameObject.layer == 10)
			{
				if(HP>5)
				{
					StartCoroutine(escalarSuave(1,-size));
				}	
				StartCoroutine(borrarDato(this.CodSer));
			}
			if(this.gameObject.layer == 9 && col.gameObject.layer == 10
				|| this.gameObject.layer == 11 && col.gameObject.layer == 9 
				|| this.gameObject.layer == 10 && col.gameObject.layer == 11)
			{
				StartCoroutine(escalarSuave(1,size));
				StartCoroutine(aniadirDato(this.CodSer));
			}
		}
    }
	public void activarJugador(bool activar)
	{
		if(activar)
			this.activo=true;
		else
			this.activo=false;
	}

	private IEnumerator aniadirDato(string CodSer) 
	{
		db.AniadirListaUsuario(CodSer,"ANIADIDO");
		StartCoroutine(leerLongitud(CodSer));
		yield return null;
	}

	private IEnumerator borrarDato(string CodSer) 
	{
		db.BorrarListaUsuario(CodSer,"BORRADO");
		StartCoroutine(leerLongitud(CodSer));
		yield return null;
	}
	private IEnumerator escalarSuave(float time, float scale_size)
    {
        Vector3 originalScale = this.transform.localScale;
        Vector3 destinationScale = this.transform.localScale + new Vector3(scale_size, scale_size, scale_size);
         
        float currentTime = 0.0f;
         
        while (currentTime <= time)
        {
            this.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        };
	}
	private IEnumerator leerLongitud(string CodSer) 
	{
		HP = db.ObtenerLongitudLista(CodSer);
		//COMPROBAR VIDA
		if (HP<=0)
		{
			//Debug.Log("AUTODESTROYENDOSE");
			db.BorrarUsuario(CodSer);
			Destroy(gameObject);
		}
		yield return null;
	}
}
