using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class doorController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {

        checkPlayerNearBy();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    void checkPlayerNearBy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3f, LayerMask.GetMask("player"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            animator.SetBool("open", true);
        }
        else
        {
            animator.SetBool("open", false);
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("EnterDoor: go to random gen map scene");
            SceneManager.LoadScene("randomMap");
        }
    }
}
