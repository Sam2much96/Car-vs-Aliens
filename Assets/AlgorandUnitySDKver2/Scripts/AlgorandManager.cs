/*
iMIT License

Copyright (c) 2022 enrico.speranza@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ALGORAND
using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;

//Additional Headers


//System IO for loading and Saving wallet Data
using System.IO;
///</use Using Algorand.Algod.Model.Application for smart Contracts>

public class AlgorandManager : Singleton<AlgorandManager>
{
    [Header("Player Configuration:")]
    [SerializeField]
    protected string m_PlayerName;
    protected string _Version = "0.20 Alfa";
    protected Account _AMAccount = null;
    private const string _InternalPassword = "0sIhlNRkMfDH8J9cC0Ky";

    [Header("Algorand Configuration:")]
    [Tooltip("ALGOD/PureStake URL Endpoint")]
    [SerializeField]
    public string ALGOD_URL_ENDPOINT = string.Empty;
    [Tooltip("ALGOD/PureStake Token")]
    [SerializeField]
    public string ALGOD_TOKEN = string.Empty;
    [Tooltip("INDEXER/PureStake URL Endpoint")]
    [SerializeField]
    public string ALGOD_URL_ENDPOINT_INDEXER = string.Empty;

    // OnEnable is called before Start
    protected virtual void OnEnable()
    {
        Debug.Log("Starting Algorand Manager...");
    }

    /// <summary>
    /// Get AlgorandSDK Version
    /// </summary>
    /// <returns>AlgorandDSK Version</returns>
    public string Version()
    {
        return _Version;
    }

    /// <summary>
    /// Get Actual Player Name
    /// </summary>
    /// <returns>Player Name</returns>
    public string GetPlayerName()
    {
        return m_PlayerName;
    }
    protected virtual void OnApplicationQuit()
    {
        Debug.Log("Algorand Manager stopped.");
    }
    //Publics Methods

    /// <summary>
    /// Generate new Algorand Account but not saved in Playprefs
    /// </summary>
    /// <returns>Algorand Account Mnemonic Passphrase</returns>
    public string GenerateAccount()
    {
        Account _Account = new Account();
        return _Account.ToMnemonic().ToString();
    }


    //should store account variables
    public Account account;
    
    //for saving and loading Local wallet data
    private string saveFile; 

    public string [] acc = {"",""};
    public string [] _GenerateAccount()
    {
    	Account _Account = new Account();
	    //string [] acc = {"A","B"};
	    acc[0] = _Account.Address.ToString();
	    acc[1] = _Account.ToMnemonic().ToString();

        //save account info

        
        //Debugger for address and Mnemonic
        Debug.Log(acc[0]) ;
        Debug.Log(acc[1]) ;
        return acc;
        
    }

    //saves account info locally
    //buggy:
        //Saves the Array object as a string instead of the contents of the array
    public void SaveAccountInfo(string AccountDetails )
    {
        File.WriteAllText(saveFile, AccountDetails.ToString());

    }

    //loads account info from local storage
    public string loaded_acc ;
    public void LoadAccountInfo()
    {   //check if files exist
        if (File.Exists(saveFile))
        {
            // Read the Entire file and save its contents
            string fileContents = File.ReadAllText(saveFile);

            //Print Account Details
            //should ideally be .json for parsing

            Debug.Log(fileContents.ToString());

            //loads account info to a variable
            loaded_acc = fileContents;

        }
    }

    //import mnemonic from presaved accounts
    public string [] ImportMnemonic(string mnemonic)
    {
        Account _Account = new Account(mnemonic);
        string [] acc = {"A","B"};
	    acc[0] = _Account.Address.ToString();
	    acc[1] = _Account.ToMnemonic().ToString();
	
        //Debugger for address and Mnemonic
        Debug.Log(acc[0]) ;
        Debug.Log(acc[1]) ;
        return acc;

    }




    //game manager
    private GameManager gameManager;
    void Start()
    {  
        //SAVES Wallet Data

       saveFile = UnityEngine.Application.persistentDataPath + "/wallet.token"; 
       
       //loads game manager
       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        
       //generates new player mnemonic 
       if (File.Exists(saveFile) == false)
       {
        //saves the array object instead of array contents
        _GenerateAccount();

        //Saves Account Info
        SaveAccountInfo(string.Format("Address: "+ acc[0] + " Mnemonic: " + acc[1]));
        
       
       }
    
       if (File.Exists(saveFile) ){LoadAccountInfo();}
   
   
        //Display Wallet Details
        //reference to game manager
        
    }


    void Update()
    {

        //Debugs to Screen
        
        if (gameManager.show_wallet)
        {
        
        gameManager.walletText.text = loaded_acc;

        }

    }
}
