using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void Back()
    {
        mainMenu.SetActive(true); ;
        optionMenu.SetActive(false);
    }
}
