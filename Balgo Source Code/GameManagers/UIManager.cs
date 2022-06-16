using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public float HighScore = 0;

    public GameObject MenuCam;
    public AudioClip MenuSong, GameplaySong, Fanfare;

    public GameObject MenuUI;
    public GameObject GameplayUI;
    public GameObject LoadingUI;
    public GameObject PauseUI;

    public PlayerMovement Player;
    public MapGen mapGen;

    public TextMeshProUGUI HighScoreText;
    public void ResetHighScore()
    {
        HighScore = 0;
        dataManager.Save();
        HighScoreText.SetText("Best Time: ---");
    }
    public void LoadHighScore(float highScore)
    {
        HighScore = highScore;
        HighScoreText.SetText("Best Time: " + highScore);
    }
    public void MainMenuQuit()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        MenuUI.SetActive(false);
        LoadingUI.SetActive(true);
        mapGen.GenerateMap();
    }
    public void FinishEnteringPlaymode()
    {
        BGMEvent BGME = new BGMEvent();
        BGME.TransitionSpd = 1;
        BGME.NewSong = GameplaySong;
        EventSystem.FireEvent(BGME);

        LoadingUI.SetActive(false);
        GameplayUI.SetActive(true);
        MenuCam.SetActive(false);
        Player.transform.parent.gameObject.SetActive(true);
    }

    public GameObject PostgameUI;
    public TextMeshProUGUI FinishState, TimeText;
    public void GameOver(float FinishTime)
    {
        if(HighScore==0)
        {
            HighScore = Mathf.Infinity;
        }
        BGMEvent BGME = new BGMEvent();
        BGME.NewSong = Fanfare;
        BGME.TransitionSpd = 1;
        EventSystem.FireEvent(BGME);

        TimeText.SetText("Finish Time: " + FinishTime + " Seconds");
        if (FinishTime<HighScore)
        {
            HighScore = FinishTime;
            TimeText.text += " (New Best Time!)";
            HighScoreText.SetText("Best Time: " + HighScore);
        }

        if(FinishTime==0)
        {
            FinishState.SetText("Quitter");
            TimeText.SetText("---");
        }
        else
        {
            FinishState.SetText("Victory! :D");
        }

        mapGen.TerminateMap();
        MenuCam.SetActive(true);
        GameplayUI.SetActive(false);
        Player.transform.parent.gameObject.SetActive(false);
        PostgameUI.SetActive(true);

        dataManager.Save();
    }
    public void Continue()
    {
        BGMEvent BGME = new BGMEvent();
        BGME.NewSong = MenuSong;
        BGME.TransitionSpd = 1;
        EventSystem.FireEvent(BGME);
        PostgameUI.SetActive(false);
        MenuUI.SetActive(true);
    }

    public void Pause()
    {
        GameplayUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    public void Resume()
    {
        GameplayUI.SetActive(true);
        PauseUI.SetActive(false);
        Player.SwitchPlayerState(PlayerStates.Gameplay);
    }
    public DataManager dataManager;
    public void Quit()
    {
        dataManager.Save();
        PauseUI.SetActive(false);
        GameOver(0);
    }
}
