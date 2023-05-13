using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Name_Color : MonoBehaviour
{
    public ColorName[] nameData;
    private TextMeshProUGUI nameText;

    void Start()
    {
        nameText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        foreach (ColorName data in nameData)
        {
            if (nameText.text == data.name)
            {
                nameText.color = data.color;
                break;
            }
            else
            {
                nameText.color = Color.white;
            }
        }
    }
}

[System.Serializable]
public class ColorName
{
    public string name;
    public Color color;
}
