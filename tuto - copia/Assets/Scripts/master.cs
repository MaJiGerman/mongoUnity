﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class master : MonoBehaviour
{

	public GameObject prefab;
	public GameObject prefab2;

	public GameObject prefab3;
	List<GameObject> generatedObjects = new List<GameObject>();
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp("space"))
        {
            //Debug.Log("Instanciando nuevo prefab");
			var capsula = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
			generatedObjects.Add(capsula);
			var capsula2 = (GameObject)Instantiate(prefab2, transform.position + new Vector3(5, 0, -5), transform.rotation);
			generatedObjects.Add(capsula2);
			var capsula3 = (GameObject)Instantiate(prefab3, transform.position + new Vector3(5, 0, 5), transform.rotation);
			generatedObjects.Add(capsula3);
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
