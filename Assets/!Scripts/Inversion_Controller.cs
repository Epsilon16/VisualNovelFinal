using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inversion_Controller : MonoBehaviour
{

    [SerializeField] private GameObject[] carrés;
    [SerializeField] private List<GameObject> inversion = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < carrés.Length; i++)
        {
            if (carrés[i].GetComponent<Selection>().selected == true)
            {
                bool boolian = false;
                for (int j = 0;  j < inversion.Count;  j++)
                {
                    if (inversion[j] == carrés[i])
                    {
                        boolian = true;
                    }
                }
                if (!boolian)
                inversion.Add(carrés[i]);
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
            inversion.Clear();
        }
    }
}
