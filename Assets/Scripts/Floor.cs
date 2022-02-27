using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]private int _Speed = 3; //Speed of floor movement
    [SerializeField]public int _delayDestroy= 3; //Delay to destroy floor
    private Bird _bird;
    void Start()
    {
        _bird = GameObject.Find("Bird").GetComponent<Bird>(); //Find GameObject in game with the name and gets the component
        Destroy(gameObject, _delayDestroy); //Destroy the GameObject with a time delay
    }

    void Update()
    {
        if(!_bird._isDead)
        {
            transform.position += new Vector3(-_Speed, 0, 0) * Time.deltaTime; //Move the floor in each frame
        }
    }
}