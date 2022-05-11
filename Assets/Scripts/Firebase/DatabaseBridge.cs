using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using System;

public class DatabaseBridge : MonoBehaviour
{

    DatabaseHandler db;

    void Start()
    {
        db = FindObjectOfType<DatabaseHandler>();
    }

    public void OnPlayed(float score){
        if(db==null)
            db = FindObjectOfType<DatabaseHandler>();

        db.OnPlayed(score);
    }

}
