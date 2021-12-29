using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }

    public Image mask;

    public Image reloadMask;

    public Text levelDisplay;

    public Text soulsAmount;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetHealth(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void SetMana(float value)
    {
        reloadMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void displayLevel(int level)
    {
        levelDisplay.text = "FLOOR: " + level;
    }

    public void displaySouls(int amount)
    {
        soulsAmount.text = amount.ToString();
    }
}
