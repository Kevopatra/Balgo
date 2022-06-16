using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{ 
    public static void PlaySFX(string ArrayName)
    {
        PlaySFXEvent PSFXE = new PlaySFXEvent();
        PSFXE.ArrayName = ArrayName;
        EventSystem.FireEvent(PSFXE);
    }
}
