using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godKillerFire : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    Transform gunEndPoint;

    public float fireVelocity;

    public float reloadTime = 0.3f;

    private bool reloaded = true;

    private float reloadTimer;

    public void Start()
    {
        gunEndPoint = transform.Find("gunEndPoint");
        reloaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // re assign the gun end point
        if (gunEndPoint == null)
        {
            gunEndPoint = transform.Find("gunEndPoint");
        }
        handleFireEvent();
    }

    void handleFireEvent()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(fire());
        }
        reloading();

    }

    public Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    IEnumerator fire()
    {
        // if reloading we return no thing
        if (reloaded == true)
        {
            Vector3 mousePos = getMousePosition();
            Vector3 gunEndPointPos = gunEndPoint.position;

            // create a bullet at gun end point

            // calculate direction for bullet
            Vector2 direction = (Vector2)(mousePos - transform.position);



            direction.Normalize();

            // this gun will fire 3 bullet 


            // wait for some time before fire another bullets


            // set up variables for reloading
            reloaded = false;
            reloadTimer = 0;



            // deplete all mana from mana bar
            UIController.instance.SetMana(0);


            // burst fire 4 bullet with fire rate is 0.2 seconds per bullet
            for (int i = 0; i < 2; i++)
            {
                GameObject cloneBullet = Instantiate(bullet, gunEndPointPos, Quaternion.identity);
                GameObject cloneBullet1 = Instantiate(bullet, gunEndPointPos + new Vector3(1, 1, 0), Quaternion.identity);
                GameObject cloneBullet2 = Instantiate(bullet, gunEndPointPos + new Vector3(1, -1, 0), Quaternion.identity);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                //rotate the bullet a bit
                cloneBullet.transform.eulerAngles = new Vector3(0, 0, angle);
                cloneBullet1.transform.eulerAngles = new Vector3(0, 0, angle);
                cloneBullet2.transform.eulerAngles = new Vector3(0, 0, angle);



                cloneBullet.GetComponent<Rigidbody2D>().velocity = direction * fireVelocity;
                cloneBullet1.GetComponent<Rigidbody2D>().velocity = direction * fireVelocity;
                cloneBullet2.GetComponent<Rigidbody2D>().velocity = direction * fireVelocity;
                yield return new WaitForSeconds(0.2f);

            }

        }

    }

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
