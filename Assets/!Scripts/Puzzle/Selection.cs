using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [Header("Params")]
    public bool isBlocked;
    public bool isEndingNerve;
    public ConnecCheck[] connecChecks;

    [Header("Others")]
    public Sprite blocked;
    public Sprite nodsprite;
    public bool isGood;
    private int isconnected = 0;
    public bool selected;
    private int securite;
    private Color highlight;

    void Start()
    {
        highlight = gameObject.GetComponent<SpriteRenderer>().color;
        isGood = false;


        if (isEndingNerve)
        {
            isBlocked = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = nodsprite;
        }

        if (isBlocked)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = blocked;
        }
    }

    void Update()
    {
        //Is selected ?
        if (selected == true)
        {
            securite = 1;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            securite = 0;
            gameObject.GetComponent<SpriteRenderer>().color = highlight;
        }

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
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, current.direction, transform.localScale.x);
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
            current.visual.GetComponent<SpriteRenderer>().sprite = current.lights;
            isconnected++;
        }
        else
        {
            current.visual.GetComponent<SpriteRenderer>().sprite = current.neutral;
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
    public Sprite neutral;
    public Sprite lights;
    public Vector2 direction;
    public int connecToHit;
    public bool istouching;
}
