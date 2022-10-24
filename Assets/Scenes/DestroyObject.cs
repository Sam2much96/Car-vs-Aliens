using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
   
     

    // Destroys rock objects at timeout
    public float timeLeft = 5.0f;
    void Update()
    {
       timeLeft -= Time.deltaTime;
     
        //double speed every 30 seconds
        if (timeLeft < 0){ Destroy(gameObject);}

    }

 
}
