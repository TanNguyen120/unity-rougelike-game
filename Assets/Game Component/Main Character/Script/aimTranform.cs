using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aimTranform : MonoBehaviour
{
    private Transform gunAimTranform;
    private GameObject gun;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.Find("gun"))
        {
            gunAimTranform = gameObject.transform.Find("gun");
            gun = GameObject.Find("gun");
            handleAiming();
        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public static Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void handleAiming()
    {
        if (!GameManeger.instance.isPaused)
        {
            Vector3 mousePos = getMousePosition();
            // tim vector phap tuyen giua 2 diem
            Vector3 aimDirection = (mousePos - gunAimTranform.position).normalized;


            // tim goc tao boi vecto phap tuyen, theo degree
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;


            gun.GetComponent<SpriteRenderer>().flipY = false;
            if (angle > 90f || angle < -90f)
            {
                gun.GetComponent<SpriteRenderer>().flipY = true;
            }
            gunAimTranform.eulerAngles = new Vector3(0, 0, angle);
            // flip gun sprite if it go left

        }

    }
}
