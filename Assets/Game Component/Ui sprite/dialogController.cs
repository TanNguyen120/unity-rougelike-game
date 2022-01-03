using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class dialogController : MonoBehaviour
{
    // make this class become a simple singleton
    public static dialogController instance { get; private set; }
    public GameObject dialogObject;
    public GameObject textBox;

    public TextMeshProUGUI dialog;

    public float displayTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // disable the ui 
        dialog = textBox.GetComponent<TextMeshProUGUI>();
        dialogObject.SetActive(false);
    }

    public void talk(string message)
    {
        // show the function async
        StartCoroutine(talkhandle(message));
    }
    IEnumerator talkhandle(string message)
    {
        dialogObject.SetActive(true);
        dialog.SetText(message);
        yield return new WaitForSeconds(displayTime);
        dialogObject.SetActive(false);
    }
}
