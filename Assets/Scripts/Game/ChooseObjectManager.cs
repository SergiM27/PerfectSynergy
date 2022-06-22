using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ChooseObjectManager : MonoBehaviour
{
    public List <GameObject> objectsToCreate;
    public GameObject[] components;
    public GameObject[] objectHolders;
    public GameObject[] clickedSprite;
    public GameObject[] componentsFromObject;
    public GameObject[] completedGameObjects;
    public int indexOfDesiredObject1, indexOfDesiredObject2;
    public GameObject lightCompleted;
    public GameObject desiredObjectAssistant;
    public GameObject assistant;
    public Sprite greenLight, greyLight, declicked, empty, assistant1, assistant2;
    private int index;

    public GameObject ChooseRandomObject(int index, GameObject objectChosen)
    {
        PutConcreteObjects(objectChosen);
        PutComponents();
        RandomizeHolders();
        return objectsToCreate[index];
    }

    public void RandomizeHolders()
    {
        int indexOfCurrentObjectToRandom = 0;
        foreach (GameObject holder in objectHolders)
        {
            int rnd = Random.Range(0, objectHolders.Length);
            Sprite tSprite = objectHolders[rnd].GetComponent<Image>().sprite;
            objectHolders[rnd].GetComponent<Image>().sprite = holder.gameObject.GetComponent<Image>().sprite;
            holder.gameObject.GetComponent<Image>().sprite = tSprite;

            int indexToSave = clickedSprite[indexOfCurrentObjectToRandom].GetComponent<EachButtonStats>().buttonNumber;
            clickedSprite[indexOfCurrentObjectToRandom].GetComponent<EachButtonStats>().buttonNumber = clickedSprite[rnd].GetComponent<EachButtonStats>().buttonNumber;
            clickedSprite[rnd].GetComponent<EachButtonStats>().buttonNumber = indexToSave;
            indexOfCurrentObjectToRandom++;
        }
    }

    public void PutConcreteObjects(GameObject objectChosen)
    {
        componentsFromObject = null;
        componentsFromObject = objectChosen.GetComponent<ComponentsObject>().components;
        indexOfDesiredObject1 = 0;
        indexOfDesiredObject2 = 0;
        while(indexOfDesiredObject1 == indexOfDesiredObject2) 
        {
            for (int i = 0; i < componentsFromObject.Length;)
            {
                index = Random.Range(0, objectHolders.Length);
                objectHolders[index].GetComponent<Image>().sprite = componentsFromObject[i].GetComponent<Image>().sprite;
                if (i == 0)
                {
                    indexOfDesiredObject1 = index;
                }
                if (i == 1)
                {
                    indexOfDesiredObject2 = index;
                }
                i++;
            }
        }

        for (int i = 0; i < objectHolders.Length; i++)
        {
            if (i != indexOfDesiredObject1 && i != indexOfDesiredObject2)
            {
                if (objectHolders[i].GetComponent<Image>().sprite.name == "Empty")
                {
                    objectHolders[i].GetComponent<Image>().sprite = components[Random.Range(0, objectHolders.Length)].GetComponent<Image>().sprite;
                }
            }
        }


    }

    public void PutComponents()
    {
        RandomizeComponents(objectHolders);
    }


    public void RandomizeComponents(GameObject[] objectHoldersList)
    {

        bool needToRepeat = false;

        for (int i = 0; i < objectHoldersList.Length; i++)
        {
            for (int j = 0; j < objectHoldersList.Length; j++)
            {
                if (j != i)
                {
                    if (i != indexOfDesiredObject1 && i != indexOfDesiredObject2)
                    {
                        //Debug.Log(i + " " + j);
                        if (objectHoldersList[i].GetComponent<Image>().sprite.name == objectHoldersList[j].GetComponent<Image>().sprite.name)
                        {
                            objectHoldersList[i].GetComponent<Image>().sprite = components[Random.Range(0, components.Length)].GetComponent<Image>().sprite;
                            needToRepeat = true;
                        }
                    }
                }
            }

        }

        if (needToRepeat == true)
        {
            RandomizeComponents(objectHoldersList);
        }
    }

    public void CheckIfCorrectAnswer(List<int> numbers)
    {
        if ((numbers[0] == indexOfDesiredObject1 || numbers[0] == indexOfDesiredObject2) && (numbers[1] == indexOfDesiredObject1 || numbers[1] == indexOfDesiredObject2))
        {
            lightCompleted.GetComponent<Image>().sprite = greenLight;
            GameManager.numberOfItemsDone++;
            GameObject.Find("Timer_SecondsText").GetComponent<Timer>().time = 5;
            GameObject.Find("Timer_SecondsText").GetComponent<Timer>().startTimer = false;
            if (GameObject.Find("DesiredObject").GetComponent<Image>().sprite.name != "Sandstorm")
            {
                GameObject.Find("ObjectCompleted").GetComponent<AudioSource>().Play();
                Invoke("NewCorrectAnswer", 1.0f);
            }
            else
            {
                GameObject.Find("Darude").GetComponent<AudioSource>().Play();
                Invoke("NewCorrectAnswer", 3.0f);
            }
            foreach (GameObject eventTrigger in clickedSprite)
            {
                eventTrigger.GetComponent<Button>().enabled = false;
            }
            desiredObjectAssistant.GetComponent<Image>().sprite = GameObject.Find("DesiredObject").GetComponent<Image>().sprite;
            assistant.GetComponent<Image>().sprite = assistant2;
            FindObjectOfType<DialogueManager>().NiceJobSentence();
            ResetHolders();
        }
    }

    public void ResetHolders()
    {
        int indexReset = 0;
        foreach (GameObject holder in clickedSprite)
        {
            holder.GetComponent<EachButtonStats>().buttonNumber = indexReset;
            indexReset++;
        }
    }

    public void NewCorrectAnswer()
    {
        FindObjectOfType<ChangeClickedState>().UnclickEverything();
        for (int i = 0; i < completedGameObjects.Length; i++)
        {
            if (completedGameObjects[i].GetComponent<Image>().sprite.name == "Empty")
            {
                completedGameObjects[i].GetComponent<Image>().sprite = GameObject.Find("DesiredObject").GetComponent<Image>().sprite;
                break;
            }
        }
        if (GameManager.numberOfItemsDone == 10)
        {
            FindObjectOfType<DialogueManager>().YouWin();
        }
        else
        {
            lightCompleted.GetComponent<Image>().sprite = greyLight;
            FindObjectOfType<DialogueManager>().StartDialogue();
        }
        desiredObjectAssistant.GetComponent<Image>().sprite = empty;
        assistant.GetComponent<Image>().sprite = assistant1;

    }
}