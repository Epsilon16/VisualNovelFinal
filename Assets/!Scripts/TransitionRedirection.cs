using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRedirection : MonoBehaviour
{
    public void Redirection()
    {
        DialogueManager.GetInstance().isOptionOn = true;
        DialogueManager.GetInstance().isMenuOn = true;
        DialogueManager.GetInstance().Transition();
    }

    public void EndOfRedirection()
    {
        DialogueManager.GetInstance().isOptionOn = false;
        DialogueManager.GetInstance().isMenuOn = false;
    }

    public void EndPuzzle()
    {
        DialogueManager.GetInstance().ExitGrigriMode();
    }
}
