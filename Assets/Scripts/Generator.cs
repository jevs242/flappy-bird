using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]private float _timeToInstantiate; // Time for spawn
    [SerializeField]private Vector3 _offsetPositionObstacle; //Position to spawn the obstacle 
    [SerializeField]private Vector3 _offsetPositionFloor; //Position to spawn the floor
    [SerializeField]private GameObject _obstacle;
    [SerializeField]private GameObject _floor;
    private bool _firstTime = true; // Is it the first time that it generates the obstacle?
    public bool isDead = false;
    
    void Start()
    {
        InvokeRepeating("CreateFloor" , _timeToInstantiate , _timeToInstantiate); //Call a function in loop
    }

    public void InvokeObstacle() //Function -> The game was began when player press space/click
    {
        StartCoroutine(BeginPlay()); //Start delay the Obstacle function
    }

    IEnumerator BeginPlay() //Function -> The game was began
    {
        if(_firstTime)
        {
            yield return new WaitForSeconds(1); //Delay
            _firstTime = false; //The first obstacle was spawned
        }
        else
        {
            yield return new WaitForSeconds(5);
        }

        InvokeRepeating("CreateObstacle" , _timeToInstantiate , _timeToInstantiate);
    }

    void CreateFloor()//Funcion ->Spawn a Floor 
    {
        if(!isDead)
        {
            Vector3 Location2 = new Vector3(_offsetPositionFloor.x, _offsetPositionFloor.y,0); //Set Location the floor
            Instantiate(_floor,Location2,Quaternion.identity); //Spawn a floor
        }
    }
    void CreateObstacle() //Funcion ->Spawn a Obstacle 
    {   
        if(!isDead)
        {
            Vector3 Location = new Vector3(_offsetPositionObstacle.x, Random.Range(5.5f , 9.0f),0); 
            Instantiate(_obstacle,Location,Quaternion.identity);
        }
    }
}

