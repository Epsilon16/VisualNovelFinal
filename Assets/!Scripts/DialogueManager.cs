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
    [SerializeField] private float typingSpeed;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    
    [Header("Dialogue Const")]
    public TextMeshProUGUI displayNameText;
    public GameObject spritePlacement;
    public GameObject background;

    private GameObject dialoguePanel;
    private TextMeshProUGUI dialogueText;
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private string canTransition;

    [Header("Grigri Const")]
    public bool isGrigriActivated;
    [SerializeField] private GameObject grigriButton;
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
    public DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;
    public DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;

    public AudioSource musicAS;


    [Header("Inkle")]
    [SerializeField] private TextAsset firstInkJSON;
    public TextAsset nextInkJSON;
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
    private const string NEXTSTORY_TAG = "next";

    [Header("Save/Load System")]
    private static SaveData loadedState;

    private DialogueVariables dialogueVariables;


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
        InitializeAudioInfoDictionary();

        dialogueIsPlaying = false;

        EnterDialogueMode(firstInkJSON);
    }

    private void SetUIObjects()
    {
        grigriPanel.SetActive(false);
        normalPanel.SetActive(false);
        if (isGrigriActivated)
        {
            dialoguePanel = grigriPanel;
            dialogueText = grigriText;
            choices = grigriChoices;
            typingSpeed = grigriTypingSpeed;

            grigriButton.SetActive(false);
        }
        else
        {
            dialoguePanel = normalPanel;
            dialogueText = normalText;
            choices = normalChoices;
            typingSpeed = normalTypingSpeed;

            if (grigriLives > 0 && !isGrigriActivated)
            {
                grigriButton.SetActive(true);
            }
            else
            {
                grigriButton.SetActive(false);
            }
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

    public void LoadSavedState()
    {
        currentStory?.state?.LoadJson(loadedState.InkStoryState);

        if (loadedState.name == "nothing")
        {
            displayNameText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            displayNameText.transform.parent.gameObject.SetActive(true);
            displayNameText.text = loadedState.name;
        }
        background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + loadedState.background);
        for (int i = 0; i < spritePlacement.transform.childCount; i++)
        {
            if (loadedState.sprites[i] == null)
            {
                spritePlacement.transform.GetChild(i).GetComponent<Image>().color = Color.clear;
            }
            else
            {
                spritePlacement.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                spritePlacement.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + loadedState.sprites[i]);
                spritePlacement.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
            }

        }

        if (loadedState.music != "nothing")
        {
            musicAS.clip = Resources.Load<AudioClip>("musics/" + loadedState.music);
            musicAS.Play();
        }
        else
        {
            musicAS.clip = null;
            musicAS.Stop();
        }

        for (int i = 0; i < audioInfos.Length; i++)
        {
            if (loadedState.audio == audioInfos[i].id)
            {
                currentAudioInfo = audioInfos[i];
                break;
            }
            else
            {
                currentAudioInfo = defaultAudioInfo;
            }
        }

        StartCoroutine(DisplayLine(currentStory.currentText));
        loadedState = null;
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
        grigriLives = ((Ink.Runtime.IntValue)GetVariableState("grigriLives")).value;

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
            MenuScript.GetInstance().StopHighlightchoice();
            MenuActivation();
        }
    }

    //Rentre dans le fichier ink
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (isGrigriActivated == true)
        {
            ExitGrigriMode();
        }

        canTransition = null;
        displayNameText.text = "???";
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        currentStory = new Story(inkJSON.text);

        if (loadedState != null)
        {
            dialogueVariables.StartListening(currentStory);
            LoadSavedState();
        }
        else
        {
            dialogueVariables.StartListening(currentStory);
            ContinueStory();
        }

        /*currentStory.BindExternalFunction("playEmote", (string emoteName) => {
            Debug.Log(emoteName);
        });*/
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

        //fade into oblivion
        if (nextInkJSON != null)
        {
            EnterDialogueMode(nextInkJSON);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

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
            
            if (canTransition != null)
            {
                StartCoroutine(Transition(nextLine));
            }
            else
            {
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }

        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //Transition Coroutine
    private IEnumerator Transition(string nextLine)
    {
        dialoguePanel.GetComponent<Animator>().Play(canTransition);

        yield return new WaitForSeconds(0.15f);

        HandleTags(currentStory.currentTags);
        dialoguePanel.GetComponent<Animator>().SetBool("End", true);

        yield return new WaitForSeconds(0.15f);

        displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        canTransition = null;
        dialoguePanel.GetComponent<Animator>().SetBool("End", false);
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

        dialogueText.maxVisibleCharacters = line.Length;
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
                    canTransition = tagValue;
                    break;
                case BACKGROUND_TAG:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + tagValue);
                    break;
                case ITEM_TAG:
                    Debug.Log(tagValue); //set de l'animation et de l'image
                    break;
                case AUDIO_TAG:
                    if (tagValue != "nothing")
                    {
                        SetCurrentAudioInfo(tagValue);
                    }
                    else
                    {
                        currentAudioInfo = defaultAudioInfo;
                    }
                    break;
                case MUSIC_TAG:
                    if (tagValue != "nothing")
                    {
                        musicAS.clip = Resources.Load<AudioClip>("musics/" + tagValue);
                        musicAS.Play();
                    }
                    else
                    {
                        musicAS.clip = null;
                        musicAS.Stop();
                    }
                    break;
                case SOUND_TAG:
                    musicAS.PlayOneShot(Resources.Load<AudioClip>("sounds/" + tagValue));
                    break;
                case NEXTSTORY_TAG:
                    string[] splitScene = tagValue.Split('/');
                    if (splitScene[0] == "nothing")
                    {
                        grigriButton.GetComponent<Button>().interactable = false;
                        nextInkJSON = null;
                    }
                    else
                    {
                        nextInkJSON = Resources.Load<TextAsset>("ink/" + splitScene[0]);

                        if (splitScene[1] == "false" && grigriLives > 0)
                        {
                            grigriButton.GetComponent<Button>().interactable = false;
                        }
                        else if (splitScene[1] == "true" && grigriLives > 0)
                        {
                            grigriButton.GetComponent<Button>().interactable = true;
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
            if (isGrigriActivated)
            {
                //AAAAAAH
            }

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
        //animation in grigri mode
        //changement des icônes menu

        EnterDialogueMode(nextInkJSON);

        isGrigriActivated = true;
        SetUIObjects();
    }

    public void ExitGrigriMode()
    {
        //animation out grigri mode
        //montrer qu'on a réussi/perdu
        //changement des icônes menu

        isGrigriActivated = false;
        SetUIObjects();
    }


    //get the state of the current story
    public string GetStoryState()
    {
        return currentStory.state.ToJson();
    }

    //Load the current story state into the SaveManager
    public static void LoadState(SaveData state)
    {
        loadedState = state;
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
