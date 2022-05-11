using System;
using UnityEngine;

namespace FirebaseWebGL.Scripts.Objects
{
    [Serializable]
    public class FirebaseUserRugPullData
    {

        public FirebaseUserRugPullData(){
            score=0;
            timesPlayed=0;
        }

        public float score;

        public int timesPlayed;

        //CAMBIAR A INT Y Float
    }
}