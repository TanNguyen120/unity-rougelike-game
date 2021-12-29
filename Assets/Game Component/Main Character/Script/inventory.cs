using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    private static int souls;

    private void Start()
    {
        UIController.instance.displaySouls(souls);
    }
    public void absorbSouls(int amount)
    {
        souls += amount;
        Debug.Log("souls: " + souls);
        UIController.instance.displaySouls(souls);

    }
}
