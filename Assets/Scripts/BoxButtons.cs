using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButtons : MonoBehaviour
{
    public Vector2 onHoverSize;

    BoxCollider2D coll;

    Vector2 currentSize;


    private void Awake() {
        coll=GetComponent<BoxCollider2D>();
        currentSize=coll.size;
    }
    
    private void OnMouseEnter() {//hover
        this.transform.localScale=.9f*Vector3.one;
        coll.size=onHoverSize;
    }
    private void OnMouseExit() {//not hover
        this.transform.localScale=Vector3.one;
        coll.size=currentSize;
    }
}
