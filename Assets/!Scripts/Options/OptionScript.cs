using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public GameObject validationGO;   

    void Start()
    {
        validationGO.SetActive(false);
    }

    public void Validation()
    {
        validationGO.SetActive(true);
    }

    public void Validee()
    {
        SaveOption.GetInstance().Savedata();
        validationGO.SetActive(false);
    }

    public void Annulation()
    {
        validationGO.SetActive(false);
    }
}
