using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Game.UI{
public class StopButton : MonoBehaviour
{
    public void OnPressed(){
        //call anim wining showing the money you made
        //call anim to show the play again screen
        SceneManager.LoadScene("Level");
    }
}}
