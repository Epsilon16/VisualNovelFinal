using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonImageSetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite flowerless;
    public Sprite algues;
    public Sprite flowerfull;
    private Button thisButton;

    public void Awake()
    {
        thisButton = GetComponent<Button>();

        SimpleChange();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (thisButton.interactable)
        {
            GetComponent<Image>().sprite = flowerfull;
        }
        else
        {
            GetComponent<Image>().sprite = flowerless;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SimpleChange();
    }

    public void SimpleChange()
    {
        if (thisButton.interactable)
        {
            GetComponent<Image>().sprite = algues;
        }
        else
        {
            GetComponent<Image>().sprite = flowerless;
        }
    }
}
