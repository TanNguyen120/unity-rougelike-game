using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    public Transform gunEndPoint;

    public float fireVelocity;
    private void Awake()
    {
        gunEndPoint = transform.Find("gunEndpoint");

    }

    // Update is called once per frame
    void Update()
    {
        handleFireEvent();
        reload();
    }

    void handleFireEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = getMousePosition();
            Vector3 gunEndPointPos = gunEndPoint.position;

            // create a bullet at gun end point
            GameObject cloneBullet = Instantiate(bullet, gunEndPointPos, Quaternion.identity);

            // calculate direction for bullet
            Vector3 direction = (mousePos - gunEndPointPos).normalized;

            // give the bullet some velocity
            cloneBullet.GetComponent<Rigidbody2D>().velocity = direction * fireVelocity;
        }
    }

    public Vector3 getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    private void reload()
    {
        float seconds = 4;
        while (seconds > 0)
        {
            seconds -= Time.deltaTime;
        }
    }
}
