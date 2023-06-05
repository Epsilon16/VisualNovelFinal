using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveValidation : MonoBehaviour
{


    public GameObject SecurityCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
