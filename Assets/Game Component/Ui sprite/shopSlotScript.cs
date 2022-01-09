using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shopSlotScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject tooltip;
    public GameObject priceTag;

    public string itemToolTip;


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Buy items");
    }

    private void OnMouseOver()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tooltip.SetActive(true);
        tooltip.transform.position = mousePosition + new Vector3(2, 0, 0);
        GameObject tipText = tooltip.transform.Find("tipText").gameObject;
        tipText.GetComponent<Text>().text = itemToolTip;
    }

    private void OnMouseExit()
    {
        tooltip.SetActive(false);
    }

}
