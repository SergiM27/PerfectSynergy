using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScientistAndBoy : MonoBehaviour
{
    public float blinkTimeBoy;
    public float blinkTimeScientist;
    public GameObject scientistEyes, boyEyes;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BlinkBoy", blinkTimeBoy, blinkTimeBoy);
        InvokeRepeating("BlinkScientist", blinkTimeScientist, blinkTimeScientist);

    }

    void BlinkBoy()
    {
        boyEyes.SetActive(true);
        Invoke("BlinkBoyOpen", 0.5f);
    }

    void BlinkBoyOpen()
    {
        boyEyes.SetActive(false);
    }

    void BlinkScientist()
    {
        scientistEyes.SetActive(true);
        Invoke("BlinkScientistOpen", 0.5f);
    }

    void BlinkScientistOpen()
    {
        scientistEyes.SetActive(false);
    }
}
