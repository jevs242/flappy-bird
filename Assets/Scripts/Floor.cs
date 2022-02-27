using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]private int _Speed = 3; //Speed of floor movement
    [SerializeField]public int _delayDestroy= 3; //Delay to destroy floor
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
            transform.position += new Vector3(-_Speed, 0, 0) * Time.deltaTime; //Move the floor in each frame
        }
    }
}