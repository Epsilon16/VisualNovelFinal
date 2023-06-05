using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPuzzle : MonoBehaviour
{
    public GameObject SquaresHolder;
    public GameObject[] Squares;

    [SerializeField] private List<GameObject> inversion = new List<GameObject>();

    public int TotalPipes = 0;

    private void Start()
    {
        TotalPipes = SquaresHolder.transform.childCount;

        Squares = new GameObject[TotalPipes];

        for (int i = 0; i < Squares.Length; i++)
        {
            Squares[i] = SquaresHolder.transform.GetChild(i).gameObject;
            Squares[i].GetComponent<Animator>().Play("square_on");
        }
    }

    void Update()
    {
        int squared = 0;

        for (int i = 0; i < Squares.Length; i++)
        {
            if (Squares[i].GetComponent<Selection>().isGood == true)
            {
                squared++;
            }
        }

        if (squared == Squares.Length)
        {
            for (int i = 0; i < Squares.Length; i++)
            {
                Squares[i].GetComponent<Animator>().Play("square_off");
            }

            if (DialogueManager.GetInstance() != null)
            {
                StartCoroutine(DialogueManager.GetInstance().ExitPuzzle());
            }
            else
            {
                Debug.Log("Gagné !");
            }
        }

        if (InputManager.GetInstance().GetSubmitPressed())
        {
            Inversion();
        }

    }

    private void Inversion()
    {
        for (int i = 0; i < Squares.Length; i++)
        {
            if (Squares[i].GetComponent<Selection>().selected == true)
            {
                bool boolian = false;
                for (int j = 0; j < inversion.Count; j++)
                {
                    if (inversion[j] == Squares[i])
                    {
                        boolian = true;
                    }
                }

                if (!boolian)
                {
                    inversion.Add(Squares[i]);
                }
            }

            for (int j = 0; j < inversion.Count; j++)
            {
                if (inversion[j].GetComponent<Selection>().selected == false)
                {
                    inversion.Clear();
                }
            }
        }

        if (inversion.Count == 2)
        {
            Vector2 pos0 = inversion[0].transform.position;
            Vector2 pos1 = inversion[1].transform.position;
            inversion[0].transform.position = pos1;
            inversion[1].transform.position = pos0;
            inversion[0].GetComponent<Selection>().selected = false;
            inversion[1].GetComponent<Selection>().selected = false;
            inversion[0].GetComponent<Animator>().Play("square_changed");
            inversion[1].GetComponent<Animator>().Play("square_changed");
            inversion.Clear();
        }
    }
}
