using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMovement : MonoBehaviour
{

    //public AudioClip AlienScream;
    public float minSpeed;  // minimum range of speed to move
    public float maxSpeed;  // maximum range of speed to move
    private float speed;     // speed is a constantly changing value from the random range of minSpeed and maxSpeed 
    public List<string> collisionTags;             //  What are the GO tags that will act as colliders that trigger
    private Vector3 randomDirection;                // Random, constantly changing direction from a narrow range for natural motion
    private float step;
    private float timeVar;
    private float rotationRange;                  //  How far should the object rotate to find a new direction?
    private float baseDirection;
    private bool colisionado; 
    private float elapsed; 
    private float elapsedRecalculate; 
    public float reWanderTime;
    public float reCalculateTime;

    void Start()
    {
        minSpeed = 0.75f;
        maxSpeed = 1.5f;
        step = Mathf.PI / 120;
        timeVar = 0;
        rotationRange = 120;
        baseDirection = 0;
        colisionado = false;
        elapsed = 0.0f;
        elapsedRecalculate = 0.0f;
        reWanderTime = 0.7f;
        reCalculateTime = 0.1f;
    }
    void Update()
    {
        if(colisionado == false)
        {       
            elapsedRecalculate += Time.deltaTime;
            if (elapsedRecalculate >= reCalculateTime) 
            {
                //Debug.Log("BOP");
                randomDirection = new Vector3(0, Mathf.Sin(timeVar) * (rotationRange / 2) + baseDirection, 0); //   Moving at random angles 
                timeVar += Random.Range(0,2*step);
                speed = Random.Range(minSpeed, maxSpeed);              //      Change this range of numbers to change speed
                //GetComponent<Rigidbody>().AddForce(transform.forward * speed);
                transform.Rotate(randomDirection * Time.deltaTime);

                elapsedRecalculate = 0.0f;
            }
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
