using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBGManager : MonoBehaviour
{
    public float coinsCooldown;

    public GameObject coinsPrefab;

    public Transform levelMovement;

    float _coinsCooldown;

    bool onDangerZone;

    // Start is called before the first frame update
    void Start()
    {
        //cada 5 min aparece una moneda
    }
    void Update(){

        if(_coinsCooldown<=0){
            //if isondangerzone, coins will spawn less
            if(!onDangerZone)
                _coinsCooldown=coinsCooldown;
            else
                _coinsCooldown=coinsCooldown*3;

            //instanciate in a random unity circle and randomize the offset of the circle
            Instantiate(coinsPrefab,Random.insideUnitCircle*6+Vector2.right*Random.Range(0,8),
            Quaternion.Euler(Vector3.zero),levelMovement);
        }
        else
            _coinsCooldown-=Time.deltaTime;

    }
    public void OnDangerZone(bool _onDangerZone){
        onDangerZone=_onDangerZone;
    }
}
