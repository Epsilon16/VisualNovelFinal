using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour//, IPointerEnterHandler
{
    private static DialogueManager instance;

    [Header("Params")]
    public float typingSpeed;

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
    private string canTransition = "trans_intro";
    public Image transBoard;
    public bool itemset;

    [Header("Grigri Const")]
    public bool isGrigriActivated;
    public GameObject grigriButton;
    public Animator grigriAnimator;
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
    public bool isOptionOn = false;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    public DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;
    public DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;

    public AudioSource musicAS;
    public string musicPath = "nothing/nothing";


    [Header("Inkle")]
    public TextAsset firstInkJSON;
    public TextAsset nextInkJSON;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    private bool grigrinow;


    [Header("SkipMode")]
    public GameObject skipIndicator;
    private bool isSkipping;

    [Header("Tags")]
    private const string NAME_TAG = "name";
    private const string SPRITE_TAG = "sprite";
    private const string CLEAR_TAG = "clear";
    private const string TRANSITION_TAG = "trans";
    private const string TRANSBG_TAG = "transbg";
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
        audioSource = gameObject.AddComponent<AudioSource>();
        currentAudioInfo = defaultAudioInfo;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        isSkipping = false;

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
        background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + loadedState.background);

        transBoard.sprite = Resources.Load<Sprite>("bgs/" + "transbg_neutral");

        for (int i = 0; i < spritePlacement.transform.childCount; i++)
        {
            Debug.Log(loadedState.sprites[i] + " number" + i);

            if (loadedState.sprites[i] == null)
            {
                spritePlacement.transform.GetChild(i).GetComponent<Image>().color = Color.clear;
            }
            else
            {
                Debug.Log("ducks");
                spritePlacement.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                spritePlacement.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + loadedState.sprites[i]);
                spritePlacement.transform.GetChild(i).GetComponent<Image>().SetNativeSize();
                spritePlacement.transform.GetChild(i).GetComponent<Animator>().Play("sprite_white");
            }
        }

        item.GetComponent<Image>().sprite = Resources.Load<Sprite>("items/" + loadedState.item);
        if (loadedState.itemstate == "True")
        {
            item.GetComponent<Animator>().Play("item_set");
        }
        else if (loadedState.itemstate == "False")
        {
            item.GetComponent<Animator>().Play("item_default");
        }

        StartCoroutine(FadeOutMusic(loadedState.music));

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
            grigriAnimator.Play("GrigriButton_Intro");
        }
        else if (loadedState.grigristate == "False")
        {
            grigriButton.GetComponent<Button>().interactable = false;
        }

        if (loadedState.gsprite != "nothing")
        {
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

        if (canContinueToNextLine && !isMenuOn && currentStory.currentChoices.Count == 0 && MenuScript.GetInstance().mouseControl == false)
        {
            if (isSkipping || InputManager.GetInstance().GetSubmitPressed())
            {
                if (puzzleName != "nothing")
                {
                    isSkipping = false;
                    EnterPuzzle();
                }
                else
                {
                    ContinueStory();
                }
            }
        }

        if (InputManager.GetInstance().GetMenuPressed() && !isOptionOn)
        {
            MenuActivation();
        }

        if (InputManager.GetInstance().GetSkipPressed() && currentStory.currentChoices.Count == 0 && !isMenuOn)
        {
            isSkipping = !isSkipping;
        }
        skipIndicator.SetActive(isSkipping);

        typingSpeed = Resolution_Quality.GetInstance().typingspeeding / 10;
        
    }

    //Rentre dans le fichier ink
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        firstInkJSON = inkJSON;

        canTransition = null;
        displayNameText.text = " ";
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
            if(transBoard.sprite.name != "transbg_neutral")
            {
                transAnim.Play("trans_titlecard");
            }
            dialogueVariables.StartListening(currentStory);
            ContinueStory();
        }
    }

    //Exit du fichier Ink
    private IEnumerator ExitDialogueMode()
    {
        isSkipping = false;

        yield return new WaitForSeconds(0.2f);
        dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialogueText.text = " ";
        displayNameText.text = " ";
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
                transAnim.Play("trans_outro");
                yield return new WaitForSeconds(4f);
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

            if (canTransition != null)
            {
                if (isSkipping)
                {
                    Transition();
                }
                else
                {
                    canContinueToNextLine = false;
                    transAnim.Play(canTransition);
                }
            }
            else
            {
                string nextLine = currentStory.Continue();
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    //Transition appelé In Transition anim
    public void Transition()
    {
        foreach (Transform child in spritePlacement.transform)
        {
            child.GetComponent<Animator>().Play("sprite_clear");
        }

        string nextLine = currentStory.Continue();

        HandleTags(currentStory.currentTags);
        canTransition = null;
        canContinueToNextLine = true;

        displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
    }

    //Affichage de la ligne
    private IEnumerator DisplayLine(string line)
    {
        //set up de l'état du bouton grigri ici car c'est tjr lu peu importe si il y a load ou pas
        GrigriButtonHandler();

        line = line.Substring(0, line.Length - 1);

        if (!isGrigriActivated)
        {
            //line += " <sprite=\"ui_next\" index=0>";
            line += " <sprite=\"ui_next-Sheet_2\" anim=\"0, 7, 5\">";
        }

        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        HideChoices();
        canContinueToNextLine = false;
        bool isAddingRichTextTag = false;
        foreach(char letter in line.ToCharArray())
        {
            if ((InputManager.GetInstance().GetSubmitPressed() && !isGrigriActivated) || isSkipping)
            {
                dialogueText.maxVisibleCharacters = line.Length;
                if (itemset)
                {
                    item.GetComponent<Animator>().Play("item_set");
                }
                else
                {
                    item.GetComponent<Animator>().Play("item_default");
                }

                if (isSkipping)
                {
                    yield return new WaitForSeconds(0.1f);
                }

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
                else if (!isGrigriActivated)
                {
                    yield return new WaitForSeconds(typingSpeed);
                }

            }
        }

        dialogueText.maxVisibleCharacters = line.Length;
        canContinueToNextLine = true;

        if (puzzleName != "nothing" && isSkipping)
        {
            isSkipping = false;
            EnterPuzzle();
        }
        else
        {
            DisplayChoices();
        }
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
                    if (tagValue == "nothing")
                    {
                        displayNameText.text = " ";
                    }
                    else
                    {
                        displayNameText.text = tagValue;
                    }
                    break;
                case SPRITE_TAG:
                    string[] splitSprite = tagValue.Split('/');
                    GameObject imagetochange = spritePlacement.transform.GetChild(int.Parse(splitSprite[1])).gameObject;
                    if (imagetochange.GetComponent<Image>().color != Color.white)
                    {
                        imagetochange.GetComponent<Animator>().Play("sprite_on");
                    }
                    else
                    {
                        imagetochange.GetComponent<Animator>().Play("sprite_change");
                    }
                    imagetochange.transform.GetChild(0).GetComponent<Image>().sprite = imagetochange.GetComponent<Image>().sprite;
                    imagetochange.transform.GetChild(0).GetComponent<Image>().SetNativeSize();

                    imagetochange.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/" + splitSprite[0]);
                    imagetochange.GetComponent<Image>().SetNativeSize();
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
                case TRANSBG_TAG:
                    transBoard.sprite = Resources.Load<Sprite>("bgs/" + tagValue);
                    break;
                case BACKGROUND_TAG:
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgs/" + tagValue);
                    break;
                case ITEM_TAG:
                    if (tagValue == "nothing")
                    {
                        item.GetComponent<Animator>().Play("item_end");
                        itemset = false;
                    }
                    else
                    {
                        item.GetComponent<Image>().sprite = Resources.Load<Sprite>("items/" + tagValue);
                        item.GetComponent<Animator>().Play("item_start");
                        itemset = true;
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
                    musicPath = tagValue;
                    StartCoroutine(FadeOutMusic(tagValue));
                    break;
                case SOUND_TAG:
                    musicAS.PlayOneShot(Resources.Load<AudioClip>("sounds/" + tagValue));
                    break;
                case NEXTSTORY_TAG:
                    string[] splitScene = tagValue.Split('/');
                    if (splitScene[0] == "nothing")
                    {
                        nextInkJSON = null;
                    }
                    else
                    {
                        nextInkJSON = Resources.Load<TextAsset>("ink/" + splitScene[0]);
                    }

                    if (splitScene[1] == "false" && grigriLives > 0)
                    {
                        if (grigriButton.GetComponent<Button>().interactable == true)
                        {
                            grigriButton.GetComponent<Button>().interactable = false;
                            grigriAnimator.Play("GrigriButton_Outro");
                        }
                    }
                    else if (splitScene[1] == "true" && grigriLives > 0)
                    {
                        isSkipping = false;
                        grigriButton.GetComponent<Button>().interactable = true;
                        grigriAnimator.Play("GrigriButton_Intro");
                    }
                    else if (splitScene[1] == "now")
                    {
                        grigrinow = true;
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
    private IEnumerator FadeOutMusic(string music)
    {
        string[] splitMusic = music.Split('/');

        if (musicAS.clip != null && musicAS.clip.name != splitMusic[1])
        {
            if (musicAS.volume <= 0.1f)
            {
                musicAS.Stop();
            }
            else
            {
                float newVolume = musicAS.volume - (0.01f);
                if (newVolume < 0f || isSkipping || InputManager.GetInstance().GetSubmitPressed())
                {
                    newVolume = 0f;
                }
                musicAS.volume = newVolume;

                yield return new WaitForEndOfFrame();
                StartCoroutine(FadeOutMusic(music));
                yield break;
            }
        }

        if (splitMusic[0] != "nothing")
        {
            musicAS.clip = Resources.Load<AudioClip>("musics/" + splitMusic[0] + "/" + splitMusic[1]);
            musicAS.Play();
        }
        else
        {
            if (musicAS.clip.name != splitMusic[1])
            {
                musicAS.clip = null;
                musicAS.Stop();
            }
        }
        musicAS.volume = 1f;
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
        EventSystem.current.SetSelectedGameObject(null);

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More Choices were given than the UI can support. Number of choises given : " 
                + currentChoices.Count);
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            isSkipping = false;
            choices[index].SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
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
            isSkipping = false;
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    public bool wasactivated;
    //Active/Désactive le Menu Déroulant
    public void MenuActivation()
    {
        EventSystem.current.SetSelectedGameObject(null);
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
                grigriButton.GetComponent<Button>().interactable = false;
                wasactivated = true;
            }
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
        }
    }

    //Lance le mode Grigri
    public void EnterGrigriMode()
    {
        isSkipping = false;
        StartCoroutine(GrigriCoroutine());
    }
    public IEnumerator GrigriCoroutine()
    {
        MenuScript.GetInstance().mouseControl = false;
        canContinueToNextLine = false;

        transAnim.Play("trans_grigri_on");
        yield return new WaitForSeconds(1.25f);

        //changement des icônes menu

        canContinueToNextLine = true;
        isGrigriActivated = true;

        SetUIObjects();
        EnterDialogueMode(nextInkJSON);
    }

    //Sort du mode Grigri
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

        //montrer qu'on a réussi/perdu
        //changement des icônes menu

        canContinueToNextLine = true;
        isGrigriActivated = false;

        dialoguePanel.SetActive(false);
        SetUIObjects();
        EnterDialogueMode(nextInkJSON);
    }

    //Lance le puzzle
    public void EnterPuzzle()
    {
        canContinueToNextLine = false;
        puzzleGO = Instantiate(Resources.Load<GameObject>("prefabs/" + puzzleName), transform.position, transform.rotation);
        dialogueText.text = "";
    }

    //Sort du puzzle
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

    //change typing speed
    public void changedtypingspeed()
    {
        typingSpeed = Resolution_Quality.GetInstance().typingspeeding;
    }  
}
