using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.CM.Common.CmCallContext;

public class Selection : MonoBehaviour
{
    [Header("Params")]
    public bool isBlocked;
    public ConnecCheck[] connecChecks;

    [Header("Others")]
    public bool isGood;
    private int isconnected = 0;
    public bool selected;
    private int securite;
    private GameObject highlight;

    void Start()
    {
        highlight = transform.GetChild(0).gameObject;
        isGood = false;

        if (isBlocked)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void Update()
    {
        //Is selected ?
        highlight.SetActive(selected);
        if (selected == true)
            securite = 1;
        else
            securite = 0;


        //Is good ?
        isconnected = 0;

        for (int i = 0; i < connecChecks.Length; i++)
        {
            ConnecCheck(connecChecks[i]);
        }

        if (isconnected == 4)
        {
            isGood = true;
        }
        else
        {
            isGood = false;
        }
    }

    private void ConnecCheck(ConnecCheck current)
    {
        current.istouching = false;

        if (current.visual.activeInHierarchy)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, current.direction, 1f);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null && hits[i].collider.gameObject != gameObject
                    && hits[i].transform.GetComponent<Selection>().connecChecks[current.connecToHit].visual.activeInHierarchy)
                {
                    current.istouching = true;
                }
            }
        }
        else
        {
            isconnected++;
        }

        if (current.istouching)
        {
            current.visual.GetComponent<SpriteRenderer>().color = Color.green;
            isconnected++;
        }
        else
        {
            current.visual.GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    private void OnMouseDown()
    {
        if (!isBlocked)
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
}

[Serializable]
public class ConnecCheck
{
    public string name;
    public GameObject visual;
    public Vector2 direction;
    public int connecToHit;
    public bool istouching;
}
