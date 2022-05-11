using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UI{
public class CircleButtons : MonoBehaviour
{

    public float onHoverSize=8.97f;

    CircleCollider2D _coll;
    float _currentRadius;

    private void Awake() {
        _coll=GetComponent<CircleCollider2D>();
        _currentRadius=_coll.radius;
    }
    
    private void OnMouseEnter() {//hover
        this.transform.localScale=.9f*Vector3.one;
        _coll.radius=onHoverSize;
    }
    private void OnMouseExit() {//not hover
        this.transform.localScale=Vector3.one;
        _coll.radius=_currentRadius;
    }
}}
