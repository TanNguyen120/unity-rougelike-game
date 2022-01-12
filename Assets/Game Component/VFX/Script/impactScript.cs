using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impactScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // destroy the object after it have played animation
        Destroy(gameObject, 0.4f);

    }


}
