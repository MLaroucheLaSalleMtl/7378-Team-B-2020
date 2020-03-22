using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPanelcontrol : MonoBehaviour
{
    public GameObject homeCanves;
    public AudioClip[] audios;
    public Slider TotalVolumnProgressBar;
    public Slider MusicVolumnProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float baseVolumn = TotalVolumnProgressBar.value;
        homeCanves.GetComponent<AudioSource>().volume = MusicVolumnProgressBar.value * baseVolumn;
    }

    public void Music1BtnClick(int chose) 
    {
        homeCanves.GetComponent<AudioSource>().clip = audios[chose];
        homeCanves.GetComponent<AudioSource>().Play();
    }
}
