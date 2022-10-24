using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Additional Headers
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool isGamePaused;
    public bool show_wallet;

    //Buttons
    public Button menu_button;
    public Button wallet_button;

    public GameObject titleScreen;
    public TextMeshProUGUI walletText;
    
    public TextMeshProUGUI  menu_text;
    

    void Start()
    {
        isGamePaused = false;   //GameOver_text.SetActive(false);
    }

    public void StartGame()
    {
        isGameActive = true;
	    
        //hides titlescreen
        titleScreen.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        //shows the wallet
        //wallet_button.onClick.AddListener(ShowWallet);
        
        //Pauses the Game
        if (!isGamePaused)
        {
            menu_button.onClick.AddListener(PauseGame);
            //should hide a UI
        }
    
        if (isGamePaused)
        {
            menu_button.onClick.AddListener(ResumeGame);
            //should show a UI
        }
    
    }

    public GameObject Algorand;
    public void ShowWallet(){
        show_wallet = true;

         //doesnt work
       // Algorand =GameObject.Find("Algorand Manager").GetComponent<AlgorandManager>();
       //walletText.text = Algorand.ShowAccount();

        
        
        }


    public void RestartGame()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        menu_text.text = "Paused";
    }   
    void ResumeGame ()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        menu_text.text = "Play";
    }

    public GameObject  GameOver_text;
    public void GameOver()
    {
        isGameActive = false;
        GameOver_text.SetActive(true);
        RestartGame();
       
    }

   
}
