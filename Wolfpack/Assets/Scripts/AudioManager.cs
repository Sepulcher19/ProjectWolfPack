using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    FMOD.Studio.EventInstance gameMusic;

    float parameterNumber = 0;

    public bool inBattle = false;


    bool firstAudio = false;
    bool secondAudio = false;
    bool thirdAudio = false;
    void Start()
    {
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");

        if (inBattle)
        {
            gameMusic.start();
        }
    }

    public void FirstAudioChange()
    {
        if (!firstAudio)
        {
            gameMusic.setParameterByName("Instruments", 1);
            firstAudio = true;
        }
    }

    public void SecondAudioChange()
    {
        if (!secondAudio)
        {
            gameMusic.setParameterByName("Instruments", 2);
            secondAudio = true;
        }
    }

    public void ThirdAudioChange()
    {
        if (!thirdAudio)
        {
            gameMusic.setParameterByName("Instruments", 3);
            thirdAudio = true;
        }
    }
}
