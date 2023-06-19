using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveValidation : MonoBehaviour
{
    public GameObject SecurityCanvas;

    public void Validation()
    {
        DialogueManager.GetInstance().isOptionOn = true;
        SecurityCanvas.SetActive(true);

        foreach (Transform child in SecurityCanvas.transform)
        {
            if (child.GetComponent<ButtonImageSetter>())
            {
                child.GetComponent<ButtonImageSetter>().SimpleChange();
            }
        }
    }

    public void Validee()
    {
        DialogueManager.GetInstance().isOptionOn = false;
        SecurityCanvas.SetActive(false);
    }

    public void Annulation()
    {
        DialogueManager.GetInstance().isOptionOn = false;
        SecurityCanvas.SetActive(false);
    }
}
