using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI scientificText;
    public string[] sentences;
    public GameObject[] eventTriggers;
    public string inGameSentence;
    public string completeObjectSentence;
    public string gameOverSentence;
    public string winSentence;
    public int index;
    public int objectIndex;
    public float typingSpeedStart;
    public float typingSpeedInGame;
    public bool gameStarted, ableToSkip;
    public GameObject objectToCreate, chosenGameObject;
    public GameObject gameOverMenu;
    public TextMeshProUGUI gameOverMenuRetryText;
    public AudioSource youWinAudio, youLoseAudio, alarmAudio;
    public Sprite redLight, youWinMenu;

    private GameObject objectText;

    public GameObject gameObjectShown;


    // Start is called before the first frame update
    void Start()
    {
        ableToSkip = false;
        foreach(GameObject eventTrigger in eventTriggers)
        {
            eventTrigger.GetComponent<Button>().enabled = false;
        }
        objectText = GameObject.Find("ObjectText");
        gameStarted = false;
        scientificText.text = "";
        Invoke("StartDialogue", 1.0f);
    }

    public void StartDialogue()
    {
        StartCoroutine(Type());
        ableToSkip = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameStarted == false && index < sentences.Length - 1 && ableToSkip && GameManager.isLevelRepeat)
        {
            StopAllCoroutines();
            NextSentence();
        }
    }

    IEnumerator Type()
    {
        scientificText.text = "";
        if (GameManager.gameOver == false)
        {
            if (gameStarted == false)
            {
                foreach (char letter in sentences[index].ToCharArray())
                {
                    scientificText.text += letter;
                    yield return new WaitForSeconds(typingSpeedStart);
                }

                if (index >= sentences.Length - 1)
                {
                    yield return new WaitForSeconds(1.0f);
                    //Que empieze la partida
                    gameStarted = true;
                    NextSentence();
                }
                else
                {
                    yield return new WaitForSeconds(2.0f);
                    NextSentence();
                }
            }
            else if (gameStarted == true)
            {
                foreach (char letter in inGameSentence.ToCharArray())
                {
                    scientificText.text += letter;
                    yield return new WaitForSeconds(typingSpeedInGame);
                }
                StartCoroutine(NewObjectToList());
            }

        }

    }

    IEnumerator NewObjectToList()
    {
        objectIndex = Random.Range(0, GameObject.Find("Objects").GetComponent<ChooseObjectManager>().objectsToCreate.Count);
        objectToCreate = GameObject.Find("Objects").GetComponent<ChooseObjectManager>().objectsToCreate[objectIndex];
        chosenGameObject = GameObject.Find("Objects").GetComponent<ChooseObjectManager>().ChooseRandomObject(objectIndex, objectToCreate);
        gameObjectShown.GetComponent<Image>().sprite = chosenGameObject.GetComponent<Image>().sprite;
        objectText.GetComponent<TextMeshProUGUI>().text = chosenGameObject.GetComponent<Image>().name;
        foreach (char letter in chosenGameObject.name.ToCharArray())
        {
            scientificText.text += letter;
            yield return new WaitForSeconds(typingSpeedInGame);
        }
        foreach (GameObject eventTrigger in eventTriggers)
        {
            eventTrigger.GetComponent<Button>().enabled = true;
        }
        FindObjectOfType<ChooseObjectManager>().objectsToCreate.RemoveAt(objectIndex);
        GameObject.Find("Timer_SecondsText").GetComponent<Timer>().startTimer = true;
    }

    public void GameOverTimer()
    {
        StartCoroutine(GameOver());

    }

    IEnumerator GameOver()
    {
        alarmAudio.Play();
        scientificText.text = "";
        GameObject.Find("Light").GetComponent<Image>().sprite = redLight;
        foreach (char letter in gameOverSentence.ToCharArray())
        {
            scientificText.text += letter;
            yield return new WaitForSeconds(typingSpeedStart);
        }
        yield return new WaitForSeconds(1.0f);
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<Animator>().SetBool("GameOver", true);
        youLoseAudio.Play();
    }
    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            scientificText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            scientificText.text = "";
            StartCoroutine(Type());
        }
    }
    public void YouWin()
    {
        StartCoroutine(GameCompleted());

    }
    IEnumerator GameCompleted()
    {
        scientificText.text = "";
        GameObject.Find("MainSong").GetComponent<AudioSource>().volume = 0;
        Invoke("MainSongBack", 2.5f);
        youWinAudio.Play();
        foreach (char letter in winSentence.ToCharArray())
        {
            scientificText.text += letter;
            yield return new WaitForSeconds(typingSpeedStart);
        }
        foreach (GameObject eventTrigger in eventTriggers)
        {
            eventTrigger.GetComponent<Button>().enabled = false;
        }
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<Image>().sprite = youWinMenu;
        gameOverMenu.GetComponent<Animator>().SetBool("GameOver", true);
        gameOverMenuRetryText.text = "Replay";
        FindObjectOfType<Timer>().enabled = false;
        
    }

    public void MainSongBack()
    {
        GameObject.Find("MainSong").GetComponent<AudioSource>().volume = 0.5f;
    }

    public void NiceJobSentence()
    {
        StartCoroutine(NiceJob());
    }

    IEnumerator NiceJob()
    {
        scientificText.text="";
        foreach (char letter in completeObjectSentence.ToCharArray())
        {
            scientificText.text += letter;
            yield return new WaitForSeconds(typingSpeedStart);
        }
    }

}
