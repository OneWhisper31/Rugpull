using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordEffect : MonoBehaviour
{
    public TextMeshProUGUI inputText;

    TextMeshProUGUI thisText;

    int stringcount;

    private void OnEnable() {
        thisText=GetComponent<TextMeshProUGUI>();
    }

    public void PasswordChange(){
        int newStringcount=inputText.text.Length;
        if(stringcount==1&&newStringcount==1)
            return;
        string newText="";
        if(stringcount==2&&newStringcount==1)
            newText="";
        else
            for (int i = 0; i < newStringcount; i++)
                newText+="*";
        stringcount=newStringcount;
        thisText.text=newText;

    }
}
