using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachButtonStats : MonoBehaviour
{
    // Start is called before the first frame update

    public bool state;
    public int buttonNumber;
   

    public void ChangeState()
    {
        state = !state;
    }

    public bool GetState()
    {
        return state;
    }

    public int GetInt()
    {
        return buttonNumber;
    }
}
