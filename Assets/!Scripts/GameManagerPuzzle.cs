using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPuzzle : MonoBehaviour
{
    public GameObject SquaresHolder;
    public GameObject[] Squares;

    [SerializeField]
    public int TotalPipes = 0;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        TotalPipes = SquaresHolder.transform.childCount;

        Squares = new GameObject[TotalPipes];

        for (int i = 0; i < Squares.Length; i++)
        {
            Squares[i] = SquaresHolder.transform.GetChild(i).gameObject;
        }

    }
}
