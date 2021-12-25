using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    public Transform gunEndPoint;

    public float fireVelocity;

    public float reloadTime;

    private bool reloaded = true;
    private void Awake()
    {
        gunEndPoint = transform.Find("gunEndpoint");
        reloaded = true;

    }

    // Update is called once per frame
    void Update()
    {
        handleFireEvent();
    }

    void handleFireEvent()
    {
        if (Input.GetMouseButtonDown(0) && reloaded)
        {
            StartCoroutine(fire());
        }

    }

    public Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    IEnumerator fire()
    {
        reloaded = false;
        Vector3 mousePos = getMousePosition();
        Vector3 gunEndPointPos = gunEndPoint.position;

        // create a bullet at gun end point
        GameObject cloneBullet = Instantiate(bullet, gunEndPointPos, Quaternion.identity);

        // calculate direction for bullet
        Vector3 direction = (mousePos - gunEndPointPos).normalized;

        // give the bullet some velocity
        cloneBullet.GetComponent<Rigidbody2D>().velocity = direction * fireVelocity;

        yield return new WaitForSeconds(reloadTime);

        reloaded = true;
    }
}



