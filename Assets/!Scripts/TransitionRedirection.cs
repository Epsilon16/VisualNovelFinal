using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRedirection : MonoBehaviour
{
    public void Redirection()
    {
        DialogueManager.GetInstance().Transition();
    }
}