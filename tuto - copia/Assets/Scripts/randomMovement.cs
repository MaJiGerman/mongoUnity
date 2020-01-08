using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMovement : MonoBehaviour
{

    //public AudioClip AlienScream;
    public float minSpeed;  // minimum range of speed to move
    public float maxSpeed;  // maximum range of speed to move
    float speed;     // speed is a constantly changing value from the random range of minSpeed and maxSpeed 
    public List<string> collisionTags;             //  What are the GO tags that will act as colliders that trigger a
    float step = Mathf.PI / 120;
    float timeVar = 0;
    float rotationRange = 120;                  //  How far should the object rotate to find a new direction?
    float baseDirection = 0;
    bool colisionado = false;
    Vector3 randomDirection;                // Random, constantly changing direction from a narrow range for natural motion
    float elapsed = 0.0f; 
    public float reWanderTime = 1.0f;
    void Update()
    {
        if(colisionado == false)
        {   
            randomDirection = new Vector3(0, Mathf.Sin(timeVar) * (rotationRange / 2) + baseDirection, 0); //   Moving at random angles 
            timeVar += Random.Range(0,2*step);
            speed = Random.Range(minSpeed, maxSpeed);              //      Change this range of numbers to change speed
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            transform.Rotate(randomDirection * Time.deltaTime);
        }else
        {
            elapsed += Time.deltaTime;
            if (elapsed >= reWanderTime) 
            {
                colisionado = false;
                elapsed = 0.0f; 
            }
        }
        
        transform.Translate(speed*Time.deltaTime,0.0f,speed*Time.deltaTime);  
    }
    
    void OnCollisionEnter(Collision col) {
        for (int i=0; i<collisionTags.Count; i++)
        {
            if (col.gameObject.tag == collisionTags[i]){     
                randomDirection.y = ((randomDirection.y + 180) % 360);
                transform.Rotate(randomDirection);
                colisionado = true;
            }
        }
    }

}
