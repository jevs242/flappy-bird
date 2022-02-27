using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacule : MonoBehaviour
{
    [SerializeField]private int _Speed = 5; //Speed of obstacle movement
    [SerializeField]private int _delayDestroy = 5; //Delay to destroy obstacle
    private Bird _bird; //Script from Bird
    
    void Start() //Function -> Start is called before the first frame update
    {
        _bird = GameObject.Find("Bird").GetComponent<Bird>(); //Find GameObject in game with the name and gets the component
        Destroy(gameObject, _delayDestroy); //Destroy the GameObject with a time delay
    }
 
    void Update() //Function -> Update is called once per frame
    {
        if(!_bird._isDead) //Is the Bird dead?
        {
            transform.position += new Vector3(-_Speed, 0, 0) * Time.deltaTime; //Move the obstacle in each frame
        }
    }
}
