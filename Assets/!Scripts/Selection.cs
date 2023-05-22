using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public bool selected;
    [SerializeField] private int securite;
    private GameObject highlight;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        highlight.SetActive(selected);
        if (selected == true)
            securite = 1;
        else
            securite = 0;
    }

    private void OnMouseDown()
    {
        if (securite == 0)
        {
            selected = true;
            
        }
        else if (securite == 1)
        {
            selected = false;
        }
    }
}
