using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public bool selected;
    [SerializeField] private int securite;
    private GameObject highlight;

    [SerializeField] private float GoodX;
    [SerializeField] private float GoodY;
    [SerializeField] private bool isGood;

    void Start()
    {
        highlight = transform.GetChild(0).gameObject;

        isGood = false;
        if (transform.position.x == GoodX && transform.position.y == GoodY)
            isGood = true;
    }

    void Update()
    {
        highlight.SetActive(selected);
        if (selected == true)
            securite = 1;
        else
            securite = 0;

        if (transform.position.x == GoodX && transform.position.y == GoodY && !isGood)
            isGood = true;
        else if (isGood && (transform.position.x != GoodX || transform.position.y != GoodY))
            isGood = false;
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

        /*if (transform.position.x == GoodX && transform.position.y == GoodY && !isGood)
            isGood = true;
        else if (isGood)
            isGood = false;*/
    }
}
