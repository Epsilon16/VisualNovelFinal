using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour//, IPointerEnterHandler
{
    private static DialogueManager instance;

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [SerializeField] private GameObject spritePlacement;
    [SerializeField] private GameObject background;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    //[SerializeField] private Button[] choicesButtons;
    private TextMeshProUGUI[] choicesText;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;
    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;


    [Header("Inkle")]
    private Story currentStory;

    public bool dialogueIsPlaying { get; private set;  }

    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    [Header("Tags")]
    private const string NAME_TAG = "name";
    private const string SPRITE_TAG = "sprite";
    private const string PLACEMENT_TAG = "place";
    private const string CLEAR_TAG = "clear";
    private const string TRANSITION_TAG = "trans";
    private const string BACKGROUND_TAG = "bg";
    private const string ITEM_TAG = "item";
    private const string AUDIO_TAG = "audio";
    private const string MUSIC_TAG = "music";
    private const string SOUND_TAG = "sound";


    private DialogueVariables dialogueVariables;

    private int Sceneindex;

    private bool MouseControl;

    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        //Set des variables
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        audioSource = this.gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        InitializeAudioInfoDictionary();

        Sceneindex = SceneManager.GetActiveScene().buildIndex;
    }

    //Choisis la bank sonore de base
    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    //Choisis la bank sonore utilisé
    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        Debug.Log(audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.LogWarning("Failed to find audio info for id: " + id);
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    //Rentre dans le fichier ink
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        /*currentStory.BindExternalFunction("playEmote", (string emoteName) => {
            Debug.Log(emoteName);
        });*/

        displayNameText.text = "???";

        ContinueStory();
    }

    //Exit du fichier Ink
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueVariables.StopListening(currentStory);
        //currentStory.UnbindExternalFunction("playEmote");
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        SetCurrentAudioInfo(defaultAudioInfo.id);
        SceneManager.LoadScene(Sceneindex + 1);
    }

    //Lorsqu'on appuit sur submit
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //Affichage de la ligne
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        continueIcon.SetActive(false);
        HideChoices();
        canContinueToNextLine = false;
        bool isAddingRichTextTag = false;
        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        continueIcon.SetActive(true);
        DisplayChoices();
        canContinueToNextLine = true;
    }

    //Joue les sons de dialogue
    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialoqueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;
            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();
                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }
            audioSource.PlayOneShot(soundClip);
        }
    }

    //Cache les choix
    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    //Tag Inkle
    private void HandleTags(List<string> currentTags)
    {
        int place = 2;

        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case NAME_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PLACEMENT_TAG:
                    place = int.Parse(tagValue);
                    break;
                case SPRITE_TAG:
                    spritePlacement.transform.GetChild(place).GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + tagValue);
                    spritePlacement.transform.GetChild(place).GetComponent<Image>().color = Color.white;
                    spritePlacement.transform.GetChild(place).GetComponent<Image>().SetNativeSize();
                    break;
                case CLEAR_TAG:
                    if (tagValue == "all")
                    {
                        foreach (Transform child in spritePlacement.transform)
                        {
                            child.GetComponent<Image>().color = Color.clear;
                        }
                    }
                    else
                    {
                        spritePlacement.transform.GetChild(int.Parse(tagValue)).GetComponent<Image>().color = Color.clear;
                    }
                    break;
                case TRANSITION_TAG:
                    Debug.Log(tagValue); //ce sera à travers un animator qu'on les fera
                    break;
                case BACKGROUND_TAG:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + tagValue);
                    break;
                case ITEM_TAG:
                    Debug.Log(tagValue); //set de l'animation et de l'image
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;
                case MUSIC_TAG:
                    //set la musique de fond
                    break;
                case SOUND_TAG:
                    //play sound effect once
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled " + tag);
                    break;
            }
        }
    }

    //Montre les choix
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More Choices were given than the UI can support. Number of choises given : " 
                + currentChoices.Count);
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }

    //Sélectionne le premier choix des boutons
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        //for (int i = 0; i >= choices.Length; i++)
        /*{
            if (choicesButtons[i].GetComponent<Button>().)
            {
                EventSystem.current.SetSelectedGameObject(choices[i].gameObject);
            }
            else
                EventSystem.current.SetSelectedGameObject(null);

        }*/
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

   //Sélection des choix avec la souris
    public void Highlightchoice(int Number)
    {
        StopCoroutine(SelectFirstChoice());
        EventSystem.current.SetSelectedGameObject(choices[Number].gameObject);
        MouseControl = true;
    }
    public void stopHighlightchoice()
    {
        MouseControl = false;
    }

    //Fait le choix avec le bouton
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    //Get Variable
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    //???????
    public void OnApplicationQuit()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }    
    }
}


