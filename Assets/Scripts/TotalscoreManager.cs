using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TotalscoreManager : MonoBehaviour
{

    public TextMeshProUGUI textMenu;
    public TextMeshProUGUI textLevel;
    public GameObject textLevel_gameobject;

    TextMeshProUGUI currentText;
    GameObject currentText_gameobject;

    private void Update() {
        if(SceneManager.GetActiveScene().name=="Menu"&&currentText!=textMenu){
            currentText=textMenu;
            currentText_gameobject=textMenu.gameObject;
            textLevel_gameobject.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().name=="Level"&&currentText!=textLevel){
            currentText=textLevel;
            currentText_gameobject=textLevel_gameobject;
            textMenu.gameObject.SetActive(false);
        }
    }

    public void DisplayInfo(string message){
        StartCoroutine(_OnTransition(message,1));
    }
    public void OnTransition(){
        currentText_gameobject.SetActive(false);
        StartCoroutine(_OnTransition(currentText.text,2.2f));
    }

    IEnumerator _OnTransition(string newText,float waitTime){
        yield return new WaitForSeconds(waitTime);
        currentText_gameobject.SetActive(true);
        currentText.text=newText;
        yield break;
    }
}
