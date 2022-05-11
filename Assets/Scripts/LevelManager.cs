using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{
    public float speed;
    public GameObject dangerZone;
    public Transform[] dangerZoneUI;
    public GameObject firstdangerZoneUI;
    public Transform dangerParentUI;
    public Animator rugCoins;

    bool isOnEnd;//slow anim

    Animator _anim;


    void Start()
    {
        _anim=GetComponent<Animator>();
        _anim.speed=1/speed;
        rugCoins.speed=1/(speed*1.5f);
        //instantiate new dangersZones from to x position
        DangerZoneInstanciate(Random.Range(30,61),dangerZoneUI[0]);
        DangerZoneInstanciate(Random.Range(90,131),dangerZoneUI[1]);
        DangerZoneInstanciate(Random.Range(151,166),dangerZoneUI[2]);
        // Instantiate(dangerZone,Random.Range(30,61)*Vector3.right,Quaternion.Euler(Vector3.zero),this.transform);
        // Instantiate(dangerZone,Random.Range(90,131)*Vector3.right,Quaternion.Euler(Vector3.zero),this.transform);
        // Instantiate(dangerZone,Random.Range(151,166)*Vector3.right,Quaternion.Euler(Vector3.zero),this.transform);

    }

    private void Update() {
        if(isOnEnd){
            _anim.speed=Mathf.Lerp(_anim.speed, 0,0.01f);
            rugCoins.speed=Mathf.Lerp(rugCoins.speed, 0,0.01f);
        }
    }

    public void SlowDown(){
        isOnEnd=true;
    }
    void DangerZoneInstanciate(int random, Transform barPrefab){
        Instantiate(dangerZone,random*Vector3.right,Quaternion.Euler(Vector3.zero),this.transform);
        //var UI =Instantiate(barPrefab,random*Vector3.right,Quaternion.Euler(Vector3.zero),dangerParentUI);
        barPrefab.transform.localPosition=new Vector3(random*5,0,0);
    }

}
