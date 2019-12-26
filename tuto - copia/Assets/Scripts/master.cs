using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class master : MonoBehaviour
{

	public GameObject prefab;
	List<GameObject> generatedObjects = new List<GameObject>();
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp("space"))
        {
            Debug.Log("Instanciando nuevo prefab");
			var capsula = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
			generatedObjects.Add(capsula);
        }
		if (Input.GetKeyUp(KeyCode.P))
        {
			foreach(var obj in generatedObjects)
			{
				Debug.Log(obj.GetComponent<Jugador>().CodSer);
				obj.GetComponent<Jugador>().activarJugador(true);
    			//Destroy(obj);
			}
		}
	}
}
