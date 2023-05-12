using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLoading : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private GameObject dialoguePanel;

    private float time;
    private bool isStarted = false;
    // Start is called before the first frame update

    private void Awake()
    {
        //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);   
    }
    void Start()
    {
        /*if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }*/
    }

    void Update()
    {    
        time += Time.deltaTime;
        if (time >= 0.0000000001f && !isStarted)
        {
            isStarted = true;
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
}
