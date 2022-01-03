using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour
{
    public GameObject shopCanvas;
    public Button close;
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        shopCanvas.SetActive(false);
        // add event listeners with parameters
        close.onClick.AddListener(delegate { deActiveObject(shopCanvas); });
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void chatting()
    {
        shopCanvas.SetActive(true);
    }

    void deActiveObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
