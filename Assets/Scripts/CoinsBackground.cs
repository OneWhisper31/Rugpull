using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Destroy(){//called from anim when animation ends
        Destroy(this.gameObject);
    }
}
