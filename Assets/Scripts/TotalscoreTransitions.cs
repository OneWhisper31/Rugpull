using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalscoreTransitions : MonoBehaviour
{
    public void OnTransition(){
        FindObjectOfType<TotalscoreManager>().OnTransition();
    }

}
