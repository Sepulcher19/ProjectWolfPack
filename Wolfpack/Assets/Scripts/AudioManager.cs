using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    FMOD.Studio.EventInstance gameMusic;

    float parameterNumber = 0;

    public bool inBattle = false;
    void Start()
    {
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");

        if (inBattle)
        {
            gameMusic.start();
        }
    }

    public void IncreaseParameter()
    {
        parameterNumber += 1;
        gameMusic.setParameterByName("Instruments", parameterNumber);
    }
}
