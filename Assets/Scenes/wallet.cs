using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallet : MonoBehaviour
{
    public GameObject algoNode = null;


    //Wallet Node in Unity

    void Start()
    {
       if (algoNode == null) {
	   algoNode = FindObjectOfType<AlgorandManager>().gameObject;
       	
	   // doesnt work algoNode.GenerateAccount();
       
       }
 
	//algoNode.GenerateAccount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
