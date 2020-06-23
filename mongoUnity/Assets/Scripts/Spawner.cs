using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; 
    public float spawnTime = 5.0f;
    public int numCapsules = 10;
    public int currentLayer = 9;
    private float elapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = 0.0f;
        Instantiate(prefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= spawnTime) 
        {
            //Cada X segundos se comprueba si el numero de capsulas son las necesarias, si no lo son se crean nuevas
            if(getNumberCapsules(currentLayer) < numCapsules)
            {
                Debug.Log("Jugadores insuficientes, creando nuevos");
                Instantiate(prefab, transform.position, transform.rotation);
            }
            elapsed = 0.0f; 
         }
    }
    private int getNumberCapsules(int layerCount)
    {
        int playerCount = 0;
        switch(layerCount)
        {
            case 9:
                playerCount = GameObject.FindGameObjectsWithTag("RedPlayer").Length;
                break;
            case 10:
                playerCount = GameObject.FindGameObjectsWithTag("PurplePlayer").Length;
                break;
            case 11:
                playerCount = GameObject.FindGameObjectsWithTag("BluePlayer").Length;
                break;
            default:
                playerCount = -1;
                break;
        }
        return playerCount;
    }
}
