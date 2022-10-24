using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class car : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    //touch input
    public TextMeshProUGUI displayText;  
    public TextMeshProUGUI multiTouchInfoDisplay;  
    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    private int maxTapCount = 0;
    private string multiTouchInfo;

    //game manager
    private GameManager gameManager;

    //game particles
    public ParticleSystem carParticle;

    void Start() //duplicate if _ready function
    {
        //reference to game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        
    }

    // Update is called once per frame
    public float speed= 80.0f;
    private float turnSpeed= 50.0f;
    public float horizontalInput;
    public float forwardInput;
    public float timeLeft = 30.0f;
    public bool isDriving;
    public bool FallingBelowMap = false;
    void Update() //duplicate of _process() function
    {
   
      //GameOver
    if (speed < 10){gameManager.GameOver();}
    if (FallingBelowMap){gameManager.GameOver();} //placeholder method

     //converts user input to vehicular input. uses a float variable 
     horizontalInput = Input.GetAxis("Horizontal");
     forwardInput =Input.GetAxis("Vertical");


     //converts touch input's vector 2 into floats for car movements
     //middle of screen is x-230, y-480
     if (Input.touchCount > 0){

     forwardInput = theTouch.position.y/100; //works

        if (theTouch.phase == TouchPhase.Moved)
	{
     	    if (theTouch.position.x < 230){ //left swipe
		horizontalInput = -theTouch.position.x/50;} 
	    if (theTouch.position.x > 300){ //right swipe
		horizontalInput = theTouch.position.x/100;}	
	  }
	}
     


    //implement kickback if player collides with rock



     //moves the car forward
     transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);   
     
     //moves the car sideways
     transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime );
     
     //driving logic
     //Debug.Log (horizontalInput); //for debug purposes only 
	
       multiTouchInfo = string.Format("Max tap count: {0}\n", maxTapCount);

       //Basic touch logic
       //Game Manager should handle Input Logic
       
       if (Input.touchCount > 0)
       {

          theTouch = Input.GetTouch(0);
	  if (theTouch.phase == TouchPhase.Ended)
	  {
	      displayText.text = theTouch.phase.ToString();
	      timeTouchEnded = Time.time;
	  
	  }
	  else if (Time.time - timeTouchEnded > displayTime)
	  {
	     displayText.text = theTouch.phase.ToString();
	     timeTouchEnded = Time.time;

	  
	  }
      	
       }
       else if (Time.time - timeTouchEnded > displayTime)
       {
       	  displayText.text = "";
       
       }

       //multi-touch logic
       if (Input.touchCount > 0)
       {
       	  for (int i = 0; i < Input.touchCount; i++)
	  {
	     theTouch = Input.GetTouch(i);

	     multiTouchInfo += string.Format("Touch {0} - Position {1} - Tap Count: {2} - FingerID: {3}\nRadius : {4} ({5}%)\n",i,
 	     theTouch.position, theTouch.tapCount,  theTouch.fingerId ,theTouch.radius,((theTouch.radius/(theTouch.radius + 
	     theTouch.radiusVariance)) * 100f).ToString("F1"));

	     if (theTouch.tapCount > maxTapCount)
	        {
		   maxTapCount = theTouch.tapCount;	 
	        }

			     
	  }
       }

       multiTouchInfoDisplay.text = multiTouchInfo;

     //car timer
	
	
    	timeLeft -= Time.deltaTime;
    
    
      //Prints Car Debug to the UI
    	speedText.text = "Speed : "+ speed + ' '+ "doubles in: " + Mathf.Round(timeLeft) + "score : " + score;
	//double speed every 30 seconds
	if (timeLeft < 0){ speed *= 04.0f ; timeLeft = 30.0f;}
	

    }

  //detects collision between 
  // gamemap, rocks, collectibles
  
  //rocks & gems
  //private GameObject [] rock;
  //private GameObject [] gems;
  private void OnTriggerStay(Collider other)
    { 
      

      if (other.gameObject.CompareTag("Rock") ) 
      {
       Debug.Log("Rock________");  
        //Reduce Speed

        speed /= 2;

        impact();
      }
      
      if (other.gameObject.CompareTag("Gems"))
      {

        Debug.Log("Gem________");
        UpdateScore(50);
        Destroy(other.gameObject);
    
      }

    }
  
  //bugs and reduces player speed too
  private void OnTriggerEnter(Collider capsle){}

  //if exiting Environment collider box
  private void OnTriggerExit(Collider box){}
  //Impact Physics
  void impact()
  {
    
  }

  //detect if player is upside DOwn
  // Trigger Game over text if true
  public bool isFallingBelowMap()
  {
    //if (car.eulerAngles.z == 180);
    return false;

  }

//can be rewritten to store Algos as Scores
  public int score;
  public void UpdateScore(int scoreToAdd)
  {
    score+= scoreToAdd;
	  Debug.Log( "Score: " + score);
    
  }

  //if speed is 0 should trigger game over

}

