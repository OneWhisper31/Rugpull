using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float randomCooldown;

    public Animator dangerZoneUI;
    public CoinsBGManager coinsDeco;

    public UnityEvent onLose;

    float _randomCooldown;
    bool isOnDangerZone;

    private void Start() {
        _randomCooldown=randomCooldown;
    }

    private void Update() {
        _randomCooldown-=Time.deltaTime;
        if(_randomCooldown<=0){
            _randomCooldown=Random.Range(1,randomCooldown);//between 1 and rc
            if(Random.Range(1,101)<=(isOnDangerZone?15:10))//more chances to lose
                onLose.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="DangerZone")
            DangerZone(true);
        else if(other.tag=="EndZone")
            onLose.Invoke();

    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="DangerZone")
            if(dangerZoneUI.GetBool("IsOnDangerZone")==false)
                DangerZone(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
         if(other.tag=="DangerZone")
            DangerZone(false);
    }
    public void DangerZone(bool _isOnDangerZone){
        coinsDeco.OnDangerZone(_isOnDangerZone);
        isOnDangerZone=_isOnDangerZone;
        dangerZoneUI.SetBool("IsOnDangerZone",_isOnDangerZone);
    }
}
