using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveValidation : MonoBehaviour
{
    public GameObject SecurityCanvas;

    public void Validation()
    {
        SecurityCanvas.SetActive(true);
    }

    public void Validee()
    {
        SecurityCanvas.SetActive(false);
        
    }

    public void Annulation()
    {
        SecurityCanvas.SetActive(false);
    }
}
