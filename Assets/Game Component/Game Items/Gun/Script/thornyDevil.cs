using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornyDevil : MonoBehaviour
{

    public GameObject bullet;
    // Start is called before the first frame update
    public Transform gunEndPoint;

    public float fireVelocity;

    public float reloadTime = 3.0f;


    private bool reloaded = true;

    private float reloadTimer;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        gunEndPoint = transform.Find("gunEndPoint");
        reloaded = true;

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        handleFireEvent();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void handleFireEvent()
    {
        if (!GameManeger.instance.isPaused)
        {
            if (Input.GetMouseButton(0))
            {
                fire();
            }
        }
        reloading();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void fire()
    {
        // if reloading we return no thing
        if (reloaded == false)
        {
            return;
        }
        else
        {
            Vector3 mousePos = getMousePosition();
            Vector3 gunEndPointPos = gunEndPoint.position;

            // create a bullet at gun end point

            // calculate direction for bullet
            Vector2 direction = (Vector2)(mousePos - transform.position);

            GameObject cloneBullet = Instantiate(bullet, gunEndPointPos, Quaternion.identity);

            direction.Normalize();

            // give the bullet some velocity
            Debug.Log("fire: " + cloneBullet.gameObject.name);

            // set up variables for reloading
            reloaded = false;
            reloadTimer = 0;

            // deplete all mana from mana bar
            UIController.instance.SetMana(0);

        }

    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void reloading()
    {
        if (reloaded == false)
        {

            // Time.deltaTime is exactly 1 seconds so it very useful to do count down or count up
            reloadTimer += Time.deltaTime;

            // set the bar of mana ui
            UIController.instance.SetMana(reloadTimer / reloadTime);
            // count down to 0 to reload
            if (reloadTimer > reloadTime)
            {
                reloadTimer = reloadTime;
                reloaded = true;
                UIController.instance.SetMana(1);
            }
        }
    }


}
