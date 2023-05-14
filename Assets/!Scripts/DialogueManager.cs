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
    [SerializeField] private float normalTypingSpeed = 0.04f;
    [SerializeField] private float grigriTypingSpeed = 0.04f;
    private float typingSpeed;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    
    [Header("Dialogue Const")]
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private GameObject spritePlacement;
    [SerializeField] private GameObject background;

    private GameObject dialoguePanel;
    private TextMeshProUGUI dialogueText;
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Grigri Const")]
    [SerializeField] private GameObject grigriButton;
    private bool isGrigriActivated;
    [SerializeField] private int grigriLives;

    [Header("Normal UI")]
    [SerializeField] private GameObject normalPanel;
    [SerializeField] private TextMeshProUGUI normalText;
    [SerializeField] private GameObject[] normalChoices;

    [Header("Grigri UI")]
    [SerializeField] private GameObject grigriPanel;
    [SerializeField] private TextMeshProUGUI grigriText;
    [SerializeField] private GameObject[] grigriChoices;

    [Header("Menu UI")]
    [SerializeField] private GameObject menuParent;
    private bool isMenuOn = false;

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
    private const string GRIGRI_TAG = "grigri";

    [Header("Save ?")]
    private DialogueVariables dialogueVariables;

    private int Sceneindex;


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
        menuParent.SetActive(true);
        isGrigriActivated = false;
        grigriButton.GetComponent<Button>().interactable = false;
        SetUIObjects();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        InitializeAudioInfoDictionary();

        Sceneindex = SceneManager.GetActiveScene().buildIndex;
    }

    private void SetUIObjects()
    {
        if (grigriLives > 0 && !isGrigriActivated)
        {
            grigriButton.SetActive(true);
        }
        else
        {
            grigriButton.SetActive(false);
        }

        grigriPanel.SetActive(false);
        normalPanel.SetActive(false);
        if (isGrigriActivated)
        {
            dialoguePanel = grigriPanel;
            dialogueText = grigriText;
            choices = grigriChoices;
            typingSpeed = grigriTypingSpeed;
        }
        else
        {
            dialoguePanel = normalPanel;
            dialogueText = normalText;
            choices = normalChoices;
            typingSpeed = normalTypingSpeed;
        }
        dialoguePanel.SetActive(true);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
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

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (canContinueToNextLine && !isMenuOn
            && currentStory.currentChoices.Count == 0 
            && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }

        if (InputManager.GetInstance().GetMenuPressed())
        {
            MenuActivation();
        }
    }

    //Rentre dans le fichier ink
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        if (!string.IsNullOrEmpty(loadedState))
        {
            currentStory?.state?.LoadJson(loadedState);

            loadedState = null;
        }

        Debug.Log(currentStory);

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
            Debug.Log(currentStory.currentText);
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
        line += " <sprite=\"ContinueIcon\" index=0>";
        //line += " <sprite=\"ContinueIcon\" anim=\"0, 69, 5\">";
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        HideChoices();
        canContinueToNextLine = false;
        bool isAddingRichTextTag = false;
        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed() || isGrigriActivated)
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

        DisplayChoices();
        canContinueToNextLine = true;
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
                    if (tagValue == "nothing")
                    {
                        displayNameText.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        displayNameText.transform.parent.gameObject.SetActive(true);
                        displayNameText.text = tagValue;
                    }
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
                case GRIGRI_TAG:
                    if (grigriLives > 0)
                    {
                        if (tagValue == "true")
                        {
                            grigriButton.GetComponent<Button>().interactable = true;
                        }
                        else if (tagValue == "false")
                        {
                            grigriButton.GetComponent<Button>().interactable = false;
                        }
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled " + tag);
                    break;
            }
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
        StartCoroutine(MenuScript.GetInstance().SelectFirstChoice(choices[0].gameObject));
    }

    //Cache les choix
    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    //Fait le choix avec le bouton
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            MenuScript.GetInstance().StopHighlightchoice();
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    //Active/Désactive le Menu Déroulant
    public void MenuActivation()
    {
        if (!isMenuOn)
        {
            isMenuOn = true;
            menuParent.GetComponent<Animator>().Play("menu_on_anim");

            foreach (GameObject choice in choices)
            {
                choice.GetComponent<Button>().interactable = false;
            }

            //activation bouton grigri

            StartCoroutine(MenuScript.GetInstance().SelectFirstChoice(menuParent.transform.GetChild(1).gameObject));
        }
        else
        {
            isMenuOn = false;
            menuParent.GetComponent<Animator>().Play("menu_off_anim");

            foreach (GameObject choice in choices)
            {
                choice.GetComponent<Button>().interactable = true;
            }

            //activation bouton grigri

            StartCoroutine(MenuScript.GetInstance().SelectFirstChoice(choices[0]));
        }
    }

    //Lance le mode Grigri
    public void EnterGrigriMode()
    {
        isGrigriActivated = true;
        SetUIObjects();

        //set du background
        //animation du monde grigri
        //changement des icônes menu
    }

    public void ExitGrigriMode()
    {
        isGrigriActivated = false;
        SetUIObjects();

        //set du background
        //animation fin du monde grigri
        //montrer qu'on a réussi/perdu
        //changement des icônes menu
    }


    //get the state of the current story
    public string GetStoryState()
    {
        return currentStory.state.ToJson();
    }

    private static string loadedState;

    public static void LoadState(string state)
    {
        loadedState = state;
    }

    public void StartStory()
    {
        //currentStory = new Story(_inkJsonAsset.text);

        if (!string.IsNullOrEmpty(loadedState))
        {
            currentStory?.state?.LoadJson(loadedState);

            loadedState = null;
        }
    }

    //Get Variable ????
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
