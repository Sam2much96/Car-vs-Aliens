using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject playerObj = null;
    
    //short spawn Intervals
    private float startDelay = 5;
    private float spawnInterval = 5.5f;

    public float timeLeft = 10.0f;

    //Long spawn Intervals
    private float startDelay_2 = 5.0f;
    private float spawnInterval_2 = 15.0f;
    void Start()
    { 
       //finds player from name    
       if (playerObj == null)
       { playerObj = GameObject.Find("Player");}
	   
       //repeatedly spawns rocks 
	InvokeRepeating("ThrowGems", startDelay_2, spawnInterval_2);

        //repeatedly spawn Gems at longer interval

       //repeatedly spawns rocks 
    InvokeRepeating("ThrowRocks", startDelay, spawnInterval);

    }

    // Follows player
    void Update()
    {
        //calculates a 15 sec timer and then spawns to player's location
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            transform.position = PlayerPositionInFront();
            timeLeft = 10.0f; //resets timer
        }

    }
    
    public GameObject rock;
    
    private Rigidbody rockRb;	
    void ThrowRocks()
    
    {
    	//instance rocks
	Instantiate(rock ,PlayerPositionAbove(), rock.transform.rotation);
    
    //call for rock objects
    
        rockRb = rock.GetComponent<Rigidbody>();
        rockRb.AddForce( RandomForce(), ForceMode.Impulse );
	    rockRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
	

    }

   // Generate a spawn position above the Player
    Vector3 PlayerPositionAbove()
    {
        float spawnPosX = playerObj.transform.position.x;
        float spawnPosY = playerObj.transform.position.y + 20.0f;
	float spawnPosZ = playerObj.transform.position.z;

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        return spawnPosition;

    }

    public GameObject gems;
    void ThrowGems()

    {
        //instance gems

    Instantiate(gems , PlayerPositionAbove(), gems.transform.rotation);


    }

    // Generate a spawn position infront the Player
    //code Duplication. Concatenate with PlayerPositionAbove()
    Vector3 PlayerPositionInFront()
    {
        float spawnPosX = playerObj.transform.position.x ;
        float spawnPosY = playerObj.transform.position.y ;
	    float spawnPosZ = playerObj.transform.position.z - 20.0f;

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        return spawnPosition;

    }

    //THrow Gems with IE enumerator
     // Randomized Force Variables
    private float minSpeed = 20;
    private float maxSpeed = 50;
    private float maxTorque = 10;
     Vector3 RandomForce()
    {
    	return Vector3.down * Random.Range(minSpeed,maxSpeed);
    }

    float RandomTorque() {
        return Random.Range(-maxTorque, maxTorque);
    }

}

