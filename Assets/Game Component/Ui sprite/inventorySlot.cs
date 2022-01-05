using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    public bool isFull;
    [SerializeField] GameObject pointer;
    public void Start()
    {
        pointer.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isFull)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            setPointer();
        }
    }

    public void setPointer()
    {
        pointer.SetActive(true);
    }

    public void removePointer()
    {
        pointer.SetActive(false);
    }
}
