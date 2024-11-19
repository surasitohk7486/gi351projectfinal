using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject flashlight;

    //public AudioSource turnOn;
    //public AudioSource turnOff;

    public bool on;
    public bool off;

    private void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }

    private void Update()
    {
        if(off && Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(true);
            //turnOn.Play();
            off = false;
            on = true;
        }
        else if(on && Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(false);
            //turnOff.Play();
            off = true;
            on = false;
        }
    }
}
