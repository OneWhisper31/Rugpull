using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AuthHandlerUI : MonoBehaviour
{

    public UnityEvent logIn;

    public void OnUserLogIn(){
        StartCoroutine(UserlogIn());
    }
    IEnumerator UserlogIn(){
        yield return new WaitForSecondsRealtime(1);
        logIn.Invoke();
        yield break;
    }
}
