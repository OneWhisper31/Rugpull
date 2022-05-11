using System.Collections;
using System.Collections.Generic;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class DatabaseHandler : MonoBehaviour
{
    AuthHandler auth;
    FirebaseUser userData;//id of the player
    FirebaseUserRugPullData rugpullData;
    TotalscoreManager totalscoreManager;

    private void Awake()
    {
        var db = FindObjectOfType<DatabaseHandler>().gameObject;
        if (db && db != this.gameObject)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            DontDestroyOnLoad(this.gameObject);
        rugpullData = new FirebaseUserRugPullData();
        totalscoreManager=GetComponentInChildren<TotalscoreManager>();
    }

    public void OnPlayed(float score)
    {//updates timesPlayed and score, called when try ends

        //calculate
        if (score > 4650||rugpullData.timesPlayed>=50){

            if(rugpullData.timesPlayed>=50)
                totalscoreManager.DisplayInfo("No attempts left, your final score is "+rugpullData.score);
            return;
        }
        if(score!=0)
            rugpullData.score += score;
        rugpullData.timesPlayed++;

        //send it to firebase
        Debug.Log(rugpullData.timesPlayed+" "+ rugpullData.score);
        RegisterScore();//POSTJSON in uid/score rugpullData.score
        RegisterTimesPlayed();//POSTJSON in uid/timesplayed rugpullData.timesplayed
    }

    public void GetUserData(FirebaseUser _userData)//when auth log in, trigger this
    {
        userData = _userData;
        string _score = "users/"+userData.uid + "/score";
        string _timesPlayed = "users/"+userData.uid + "/timesPlayed";
        FirebaseDatabase.GetJSON(_score, gameObject.name, "DisplayDataScore", "DisplayErrorObject");
        FirebaseDatabase.GetJSON(_timesPlayed, gameObject.name, "DisplayDataTimesPlayed", "DisplayErrorObject");
    }
    void Update()
    {
        if (userData != null && auth != null)//if already log in, display menu
            auth.GetUserData(userData);
        else if (auth == null && SceneManager.GetActiveScene().name == "Menu")//if in menu, and auth null, find it
            auth = GameObject.FindObjectOfType<AuthHandler>();
        
    }
    public void DisplayDataScore(string data)//called from GetJSON()
    {
        if (data == "null")
            RegisterScore();
        else
        {
            //unpack
            float.TryParse(data,out float parsedScore);
            rugpullData.score = parsedScore;
            if(rugpullData.timesPlayed>=50)//if 50 attemps played, display
                totalscoreManager.DisplayInfo("No attempts left, your final score is "+rugpullData.score);
            else //else display how many attepms 
                totalscoreManager.DisplayInfo((50-rugpullData.timesPlayed)+" attempts left, your total score is "+rugpullData.score);
        }
    }

    public void DisplayDataTimesPlayed(string data)//called from GetJSON()
    {
        if (data == "null")
            RegisterTimesPlayed();
        else
        {
            //unpack
            Int32.TryParse(data,out int parsedtimesPlayed);
            rugpullData.timesPlayed = parsedtimesPlayed;
            if(rugpullData.timesPlayed>=50)//if 50 attemps played, display
                totalscoreManager.DisplayInfo("No attempts left, your final score is "+rugpullData.score);
            else //else display how many attepms 
                totalscoreManager.DisplayInfo((50-rugpullData.timesPlayed)+" attempts left, your total score is "+rugpullData.score);
        }
    }

    public void RegisterScore()
    {
        string _score = "users/"+userData.uid + "/score";
        FirebaseDatabase.PostJSONFloat(_score, rugpullData.score, gameObject.name,
        "DisplayInfo", "DisplayErrorObject");
    }
    public void RegisterTimesPlayed()
    {
        string _timesPlayed = "users/"+userData.uid + "/timesPlayed";
        FirebaseDatabase.PostJSONInt(_timesPlayed, rugpullData.timesPlayed, gameObject.name,
        "DisplayInfo", "DisplayErrorObject");
    }

    public void DisplayInfo(string info)=>totalscoreManager.DisplayInfo((50-rugpullData.timesPlayed)+" attempts left, your total score is "+rugpullData.score);
    /*updates textUI, called in PostJSON();*/

    public void DisplayErrorObject(string error)
    {
        var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
        DisplayError(parsedError.message);
    }

    public void DisplayError(string error)=>Debug.LogError(error);

}