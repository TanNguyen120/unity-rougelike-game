using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour
{
    public GameObject gameManeger;
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManeger.instance == null)
        {
            Instantiate(gameManeger);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
