using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeClickedState : MonoBehaviour
{

    public bool isPressed;
    public GameObject[] componentButtons;
    public bool[] pressedComponents;
    public Sprite clicked, declicked;
    public AudioSource clickSound, declickSound;
    public int numberOfButtonsPressed = 0;
    public List<int> pressedButtonsInt = new List<int>();

    private void Start()
    {
        for (int i = 0; i < pressedComponents.Length; i++)
        {
            pressedComponents[i] = false;
        }
        foreach (GameObject sprite in componentButtons)
        {
            sprite.GetComponent<Image>().sprite = declicked;
        }
    }
    public void Clicked(int number)
    {
            if (pressedComponents[number] == false)
            {
                componentButtons[number].GetComponent<Image>().sprite = clicked;
                componentButtons[number].GetComponent<EachButtonStats>().state = true;
                clickSound.Play();
                pressedComponents[number] = true;
                numberOfButtonsPressed++;
            }
            else
            {
                componentButtons[number].GetComponent<Image>().sprite = declicked;
                componentButtons[number].GetComponent<EachButtonStats>().state = false;
                declickSound.Play();
                pressedComponents[number] = false;
                numberOfButtonsPressed--;
            }

            if (numberOfButtonsPressed == 2)
            {
                pressedButtonsInt.Clear();
                foreach (GameObject button in componentButtons)
                {
                    if (button.GetComponent<EachButtonStats>().GetState() == true)
                    {
                        pressedButtonsInt.Add(button.GetComponent<EachButtonStats>().GetInt());
                    }
                }
                FindObjectOfType<ChooseObjectManager>().CheckIfCorrectAnswer(pressedButtonsInt);
            }

    }

    public void UnclickEverything()
    {
        for (int i = 0; i < pressedComponents.Length; i++)
        {
            componentButtons[i].GetComponent<EachButtonStats>().state = false;
            componentButtons[i].GetComponent<Image>().sprite = declicked;
            numberOfButtonsPressed = 0;
            pressedComponents[i] = false;

        }
    }
 
}
