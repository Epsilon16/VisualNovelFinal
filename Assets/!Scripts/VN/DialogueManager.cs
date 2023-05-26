using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour//, IPointerEnterHandler
{
    private static DialogueManager instance;

    [Header("Params")]
    [SerializeField] private float typingSpeed;

    [Header("Load Globals JSON")]
    public TextAsset loadGlobalsJSON;
    public DialogueVariables dialogueVariables;

    [Header("Dialogue Const")]
    public TextMeshProUGUI displayNameText;
    public GameObject spritePlacement;
    public GameObject background;
    public GameObject item;

    private GameObject dialoguePanel;
    private TextMeshProUGUI dialogueText;
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public Animator transAnim;
    private string canTransition;

    [Header("Grigri Const")]
    public bool isGrigriActivated;
    public GameObject grigriButton;
    [SerializeField] private int grigriLives;

    [Header("Normal UI")]
    [SerializeField] private GameObject normalPanel;
    [SerializeField] private TextMeshProUGUI normalText;
    [SerializeField] private GameObject[] normalChoices;

    [Header("Grigri UI")]
    [SerializeField] private GameObject grigriPanel;
    [SerializeField] private TextMeshProUGUI grigriText;
    [SerializeField] private GameObject[] grigriChoices;
    public GameObject gSprite;

    [Header("Puzzle")]
    private string puzzleName = "nothing";
    private GameObject puzzleGO;

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
    public TextAsset firstInkJSON;
    public TextAsset nextInkJSON;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    private bool grigrinow;

    [Header("Tags")]
    private const string NAME_TAG = "name";
    private const string SPRITE_TAG = "sprite";
    private const string CLEAR_TAG = "clear";
    private const string TRANSITION_TAG = "trans";
    private const string BACKGROUND_TAG = "bg";
    private const string ITEM_TAG = "item";
    private const string AUDIO_TAG = "audio";
    private const string MUSIC_TAG = "music";
    private const string SOUND_TAG = "sound";
    private const string NEXTSTORY_TAG = "next";
    private const string PUZZLE_TAG = "puzzle";
    private const string GSPRITE_TAG = "gsprite";

    [Header("Save/Load System")]
    public static SaveData loadedState;


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

        foreach (Transform child in spritePlacement.transform)
        {
            child.GetComponent<Image>().color = Color.clear;
        }

        if (loadedState != null)
        {
            firstInkJSON = Resources.Load<TextAsset>("ink/" + loadedState.currentjson);

            if (loadedState.layoutstate == "True")
            {
                isGrigriActivated = true;
            }
        }

        SetUIObjects();
        InitializeAudioInfoDictionary();

        dialogueIsPlaying = false;

        EnterDialogueMode(firstInkJSON);
    }

    private void SetUIObjects()
    {
        normalPanel.SetActive(false);
        grigriPanel.SetActive(false);

        if (isGrigriActivated)
        {
            dialoguePanel = grigriPanel;
            dialogueText = grigriText;
            choices = grigriChoices;

            grigriButton.SetActive(false);
        }
        else
        {
            dialoguePanel = normalPanel;
            dialogueText = normalText;
            choices = normalChoices;
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
        currentStory.state.LoadJson(loadedState.InkStoryState);

        displayNameText.text = loadedState.name;
        if (loadedState.name == "nothing")
        {
            displayNameText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            displayNameText.transform.parent.gameObject.SetActive(true);
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
                spritePlacement.transform.GetChild(i).GetComponent<Animator>().Play("sprite_white");
            }
        }

        item.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + loadedState.item);

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

        if (loadedState.nextjson != "nothing")
        {
            nextInkJSON = Resources.Load<TextAsset>("ink/" + loadedState.nextjson);
        }
        else
        {
            nextInkJSON = null;
        }

        if (loadedState.grigristate == "True")
        {
            grigriButton.GetComponent<Button>().interactable = true;
        }
        else if (loadedState.grigristate == "False")
        {
            grigriButton.GetComponent<Button>().interactable = false;
        }

        if (loadedState.gsprite != "nothing")
        {
            Debug.Log(loadedState.gsprite);
            gSprite = Instantiate(Resources.Load<GameObject>("prefabs/" + loadedState.gsprite),
                grigriPanel.transform.position, grigriPanel.transform.rotation, grigriPanel.transform);
            gSprite.transform.SetAsFirstSibling();
            gSprite.name = loadedState.gsprite;
        }

        HandleTags(currentStory.currentTags);
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
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (canContinueToNextLine && !isMenuOn && currentStory.currentChoices.Count == 0
            && MenuScript.GetInstance().mouseControl == false && InputManager.GetInstance().GetSubmitPressed())
        {
            if (puzzleName != "nothing")
            {
                EnterPuzzle();
            }
            else
            {
                ContinueStory();
            }
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
        firstInkJSON = inkJSON;

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
    }

    //Exit du fichier Ink
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        SetCurrentAudioInfo(defaultAudioInfo.id);

        //fade into oblivion
        if (nextInkJSON != null)
        {
            if (isGrigriActivated)
            {
                StartCoroutine(ExitGrigriMode());
            }
            else
            {
                EnterDialogueMode(nextInkJSON);
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    //Lorsqu'on appuit sur submit
    private void ContinueStory()
    {
        if (grigrinow)
        {
            grigrinow = false;
            EnterGrigriMode();
        }
        else if (currentStory.canContinue)
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
        canContinueToNextLine = false;
        transAnim.Play(canTransition);

        yield return new WaitForSeconds(0.25f);

        HandleTags(currentStory.currentTags);
        canTransition = null;
        canContinueToNextLine = true;
        displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
    }

    //Affichage de la ligne
    private IEnumerator DisplayLine(string line)
    {
        //set up de l'�tat du bouton grigri ici car c'est tjr lu peu importe si il y a load ou pas
        GrigriButtonHandler();

        line = line.Substring(0, line.Length - 1);

        if (!isGrigriActivated)
        {
            line += " <sprite=\"ContinueIcon\" index=0>";
            //line += " <sprite=\"ContinueIcon\" anim=\"0, 69, 5\">";
        }

        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        HideChoices();
        canContinueToNextLine = false;
        bool isAddingRichTextTag = false;
        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed() && !isGrigriActivated)
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
                if (isGrigriActivated)
                {
                    yield return new WaitForSeconds(typingSpeed*2);
                }
                else
                {
                    yield return new WaitForSeconds(typingSpeed);
                }

            }
        }

        dialogueText.maxVisibleCharacters = line.Length;
        DisplayChoices();
        canContinueToNextLine = true;
    }

    private void GrigriButtonHandler()
    {
        grigriLives = ((IntValue)GetVariableState("grigriLives")).value;
        grigriButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + grigriLives + '-';

        if (!isGrigriActivated)
        {
            if (grigriLives > 0)
            {
                grigriButton.SetActive(true);
                //rajouter un "appuyez sur A" or whatever the fuck works
            }
            else
            {
                grigriButton.SetActive(false);
                //enlever le "appuyez sur A" or whatever the fuck
            }
        }
    }

    //Tag Inkle
    private void HandleTags(List<string> currentTags)
    {
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
                    if (tagValue == "nothing")
                    {
                        displayNameText.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        displayNameText.transform.parent.gameObject.SetActive(true);
                    }
                    break;
                case SPRITE_TAG:
                    string[] splitSprite = tagValue.Split('/');
                    spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + splitSprite[0]);
                    spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).GetComponent<Image>().SetNativeSize();
                    if (spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).GetComponent<Image>().color != Color.white)
                    {
                        //spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).GetComponent<Image>().color = Color.white;
                        spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).GetComponent<Animator>().Play("sprite_on");
                    }
                    break;
                case CLEAR_TAG:
                    if (tagValue == "all")
                    {
                        foreach (Transform child in spritePlacement.transform)
                        {
                            if (child.GetComponent<Image>().color != Color.clear)
                            {
                                //child.GetComponent<Image>().color = Color.clear;
                                child.GetComponent<Animator>().Play("sprite_off");
                            }
                        }
                    }
                    else
                    {
                        if (spritePlacement.transform.GetChild(int.Parse(tagValue)).GetComponent<Image>().color != Color.clear)
                        {
                            //spritePlacement.transform.GetChild(int.Parse(tagValue)).GetComponent<Image>().color = Color.clear;
                            spritePlacement.transform.GetChild(int.Parse(tagValue)).GetComponent<Animator>().Play("sprite_off");
                        }
                    }
                    break;
                case TRANSITION_TAG:
                    canTransition = tagValue;
                    break;
                case BACKGROUND_TAG:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + tagValue);
                    break;
                case ITEM_TAG:
                    if (tagValue == "nothing")
                    {
                        item.GetComponent<Animator>().SetBool("end", true);
                    }
                    else
                    {
                        item.GetComponent<Animator>().SetBool("end", false);
                        item.GetComponent<Image>().sprite = Resources.Load<Sprite>("items/" + tagValue);
                        item.GetComponent<Animator>().Play("item_start");
                    }
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
                    StartCoroutine(FadeOutMusic(tagValue));
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
                        else if (splitScene[1] == "now")
                        {
                            grigrinow = true;
                        }
                    }
                    break;
                case PUZZLE_TAG:
                    puzzleName = tagValue;
                    break;
                case GSPRITE_TAG:
                    gSprite = Instantiate(Resources.Load<GameObject>("prefabs/" + tagValue),
                        dialoguePanel.transform.position, dialoguePanel.transform.rotation, dialoguePanel.transform);
                    gSprite.transform.SetAsFirstSibling();
                    gSprite.name = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled " + tag);
                    break;
            }
        }
    }

    //Fade Out de la musique
    private IEnumerator FadeOutMusic(string tagValue)
    {
        if (musicAS.clip != null)
        {
            if (musicAS.volume <= 0.1f)
            {
                musicAS.Stop();
            }
            else
            {
                float newVolume = musicAS.volume - (0.01f);
                if (newVolume < 0f)
                {
                    newVolume = 0f;
                }
                musicAS.volume = newVolume;

                yield return new WaitForEndOfFrame();
                StartCoroutine(FadeOutMusic(tagValue));
                yield break;
            }
        }

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
        musicAS.volume = 1f;
    }

    //Choisis la bank sonore utilis�
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
            MenuScript.GetInstance().StopHighlightchoice();
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    public bool wasactivated;

    //Active/D�sactive le Menu D�roulant
    public void MenuActivation()
    {
        MenuScript.GetInstance().mouseControl = false;

        if (!isMenuOn)
        {
            isMenuOn = true;
            menuParent.GetComponent<Animator>().Play("menu_on_anim");

            foreach (GameObject choice in choices)
            {
                choice.GetComponent<Button>().interactable = false;
            }

            if (grigriButton.GetComponent<Button>().interactable == true)
            {
                wasactivated = true;
                grigriButton.GetComponent<Button>().interactable = false;
            }

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

            if (wasactivated)
            {
                grigriButton.GetComponent<Button>().interactable = true;
                wasactivated = false;
            }

            StartCoroutine(MenuScript.GetInstance().SelectFirstChoice(choices[0]));
        }
    }

    //Lance le mode Grigri
    public void EnterGrigriMode()
    {
        StartCoroutine(GrigriCoroutine());
    }
    public IEnumerator GrigriCoroutine()
    {
        MenuScript.GetInstance().mouseControl = false;
        canContinueToNextLine = false;

        transAnim.Play("trans_grigri_on");
        yield return new WaitForSeconds(1.25f);

        //changement des ic�nes menu

        canContinueToNextLine = true;
        isGrigriActivated = true;

        SetUIObjects();
        EnterDialogueMode(nextInkJSON);
    }

    public IEnumerator ExitGrigriMode()
    {
        canContinueToNextLine = false;
        dialoguePanel.SetActive(true);

        transAnim.Play("trans_grigri_off");
        yield return new WaitForSeconds(1.25f);

        if (gSprite != null)
        {
            Destroy(gSprite.gameObject);
        }

        //montrer qu'on a r�ussi/perdu
        //changement des ic�nes menu

        canContinueToNextLine = true;
        isGrigriActivated = false;

        dialoguePanel.SetActive(false);
        SetUIObjects();
        EnterDialogueMode(nextInkJSON);
    }

    public void EnterPuzzle()
    {
        canContinueToNextLine = false;
        puzzleGO = Instantiate(Resources.Load<GameObject>("prefabs/" + puzzleName), transform.position, transform.rotation);
        dialogueText.text = "";
        //animation d'activation
    }

    public IEnumerator ExitPuzzle()
    {
        puzzleName = "nothing";

        //animation out
        yield return new WaitForSeconds(1f);
        Destroy(puzzleGO);

        canContinueToNextLine = true;
        ContinueStory();
    }


    //TOUT LES SCRIPTS PAR RAPPORT A LA SAUVEGARDE/LOAD
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

    //Get Variable in Globals by name
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
}