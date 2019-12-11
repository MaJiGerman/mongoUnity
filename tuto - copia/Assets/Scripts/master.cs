using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class master : MonoBehaviour
{

	public GameObject prefab;
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp("space"))
        {
            Debug.Log("Instanciando nuevo prefab");
			Instantiate(prefab, transform.position, transform.rotation);
        }
	}
}
