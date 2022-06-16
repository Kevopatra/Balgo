using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SFXArray
    {
        public string ArrayName;
        public AudioClip[] SFXs;
    }

    public Slider SFXSlider, BGMSlider;
    public List<AudioClip> BattleTracks = new List<AudioClip>();
 
    public AudioSource SFXS;
    public AudioSource BGMS1, BGMS2;
    public float VolSpd; //.2
    public float MaxBGMVol;
    bool Source2Current;
    bool ChangingBGM;
    bool Looping;
    public List<SFXArray> SFXs = new List<SFXArray>();

    public void OnEnable()
    {
        EventSystem.Register<PlaySFXEvent>(PlaySFX);
        EventSystem.Register<BGMEvent>(BGME);
    }
    public void OnDisable()
    {
        EventSystem.Unregister<PlaySFXEvent>(PlaySFX);
        EventSystem.Unregister<BGMEvent>(BGME);
    }
    public void UpdateVolumes(bool SFX)
    {
        if(SFX)
        {
            SFXS.volume = SFXSlider.value;
        }
        else
        {
            MaxBGMVol = BGMSlider.value;
            if(Source2Current)
            {
                BGMS2.volume = BGMSlider.value;
            }
            else
            {
                BGMS1.volume = BGMSlider.value;
            }
        }
    }
    public void PlaySFX(PlaySFXEvent SFXE)
    {
        foreach (SFXArray SFXA in SFXs)
        {
            if (SFXA.ArrayName == SFXE.ArrayName)
            {
                int r = Random.Range(0, SFXA.SFXs.Length);
                SFXS.PlayOneShot(SFXA.SFXs[r]);
                return;
            }
        }
    }
    public void BGME(BGMEvent BGME)
    {
        VolSpd = BGME.TransitionSpd;
        if (BGME.FightMode)
        {
            Looping = true;
            LChangeSong();
        }
        else
        {
            Looping = false;
            SwitchBGM(BGME.NewSong);
        }
    }

    public void SwitchBGM(AudioClip NewSong)
    {
        if (NewSong != null)
        {
            if (Source2Current)
            {
                if (BGMS2.clip != NewSong)
                {
                    BGMS1.clip = NewSong;
                    BGMS1.Play();
                    ChangingBGM = true;
                }
            }
            else
            {
                if (BGMS1.clip != NewSong)
                {
                    BGMS2.clip = NewSong;
                    BGMS2.Play();
                    ChangingBGM = true;
                }
            }
        }
    }
    public void LChangeSong()
    {
        AudioClip NewSong = BattleTracks[Random.Range(0, BattleTracks.Count)];
        T = 0;
        Goal = NewSong.length - 2;

        SwitchBGM(NewSong);
    }

    float Goal;
    float T;
    public void Update()
    {
        if (ChangingBGM)
        {
            if (Source2Current)
            {
                if (BGMS1.volume <= MaxBGMVol)
                {
                    BGMS1.volume += VolSpd * Time.deltaTime;
                }
                BGMS2.volume -= VolSpd * Time.deltaTime;
                if (BGMS1.volume >= MaxBGMVol && BGMS2.volume <= 0)
                {
                    BGMS2.clip = null;
                    BGMS2.Stop();
                    ChangingBGM = false;
                    Source2Current = false;
                }
            }
            else
            {
                if (BGMS2.volume <= MaxBGMVol)
                {
                    BGMS2.volume += VolSpd * Time.deltaTime;
                }
                BGMS1.volume -= VolSpd * Time.deltaTime;
                if (BGMS2.volume >= MaxBGMVol && BGMS1.volume <= 0)
                {
                    BGMS1.clip = null;
                    BGMS1.Stop();
                    ChangingBGM = false;
                    Source2Current = true;
                }
            }
        }
        if(Looping)
        {
            T += Time.deltaTime;
            if(T>=Goal)
            {
                LChangeSong();
            }
        }
    }
}
