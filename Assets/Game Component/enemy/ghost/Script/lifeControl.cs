using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeControl : MonoBehaviour
{
    public float maxhealth;

    float health;

    Animator animator;

    public Image mask;

    public GameObject healthUi;

    public GameObject souls;

    float originalSize;

    void Awake()
    {
        if (GameManeger.instance.sceneState == SceneState.bossScene)
        {
            healthUi.SetActive(true);
        }
        else
        {
            healthUi.SetActive(false);
        }
        health = maxhealth;
        animator = GetComponent<Animator>();
        originalSize = mask.rectTransform.rect.width;
    }

    private void Update()
    {
        checkLife();
        // we show enemy health only when it damged
        if (health < maxhealth)
        {
            healthUi.SetActive(true);
        }
    }

    private void checkLife()
    {
        // destroy game object when it run out of health and spawn some souls
        if (health <= 0)
        {
            Instantiate(souls, transform.position, Quaternion.identity);
            Debug.Log("Destroy " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void receiveDamage(float damage)
    {

        health -= damage;

        Debug.Log("recive damage: " + damage + " health: " + health);

        // update health ui
        float healthBar = health / maxhealth;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * healthBar);

        // make unity wait for notDamgeAnimation to resolve
        StartCoroutine(notDamgeAnimation());

    }
    IEnumerator notDamgeAnimation()
    {
        animator.SetBool("damged", true);
        // trigger the damged animation for 2 seconds
        yield return new WaitForSeconds(2);
        animator.SetBool("damged", false);

    }

    public float getCurrentHealth()
    {
        return (float)(health);
    }

    public float getMaxHealth()
    {
        return (float)(maxhealth);
    }
}
