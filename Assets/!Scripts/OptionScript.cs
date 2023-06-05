using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public GameObject OptionPanel;
    public GameObject validationGO;
    
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
        validationGO.SetActive(true);
    }

    public void Validee()
    {
        SaveOption.GetInstance().Savedata();
        validationGO.SetActive(false);
        OptionPanel.SetActive(false);
    }

    public void annulation()
    {
        validationGO.SetActive(false);
    }
}
