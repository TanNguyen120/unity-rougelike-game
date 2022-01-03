using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

public class bugManController : MonoBehaviour
{
    public List<string> quotes;


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void chatting()
    {
        Debug.Log("bug man chat with main player:");
        int randomIndex = Random.Range(0, quotes.Count);
        dialogController.instance.talk(quotes[randomIndex].ToString());
    }
}
