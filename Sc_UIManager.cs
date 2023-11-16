using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text statusText;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private TMP_Text wonText;
    [SerializeField]
    private TMP_Text blueScoreText;
    [SerializeField]
    private TMP_Text redScoreText;
    [SerializeField]
    private TMP_Text leadText;

    private int blueScore = 0;
    private int redScore = 0;
    private int gamesPlayed = 0;


    public void setPlayerPlaying(int p)
    {
        statusText.text = p == 0 ? "<color=#5d8cdb>PLAYER 1 </color> PLAYING" : "<color=#fd3535>PLAYER 2 </color> PLAYING";
    }

    public void gameEnded(int type, int from)
    {
        statusText.text = "<color=#b35ebf>GAME ENDED</color>";
        Debug.Log(type + "won,  lost because of " + from);
        int tempBlueScore = Sc_GameController.Instance.GetScore(0);
        int tempRedScore = Sc_GameController.Instance.GetScore(1);
        blueScore = Cl_GameData.currentBlueScore;
        redScore = Cl_GameData.currentRedScore;
        if(tempBlueScore> tempRedScore && from==0)
        {
            blueScore = blueScore + tempBlueScore - tempRedScore;
        }
        else if (tempBlueScore < tempRedScore && type == 0)
        {
            redScore = redScore + tempRedScore - tempBlueScore;
        }
        else
        {
            if(type == 1)
            {
                blueScore = blueScore + 2;
            }
            else
            {
                redScore = redScore + 2;
            }
        }
        Cl_GameData.gamesPlayed++;
        gamesPlayed = Cl_GameData.gamesPlayed;
        wonText.text = type == 0 ? "<color=#5d8cdb>PLAYER 1</color> WON" : "<color=#fd3535>PLAYER 2</color> WON";
        blueScoreText.text = blueScore.ToString();
        redScoreText.text = redScore.ToString();
        leadText.text = blueScore > redScore ? "<color=#5d8cdb>PLAYER 1</color> HAS THE LEAD" : "<color=#fd3535>PLAYER 2</color> HAS THE LEAD";
        if (blueScore == redScore) leadText.text = "IT'S A <color=#b35ebf>TIE</color>";
        leadText.text = leadText.text + " AFTER <color=#b35ebf>"+" " + gamesPlayed + "</color>"+" GAMES";
        gameOverScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        if (!Sc_GameController.Instance.isLocalMulti)
        {
            Cl_GameData.coins += blueScore;
            Cl_GameData.lastScore = blueScore;
            Cl_GameData.highestScore = Cl_GameData.highestScore <= blueScore ? blueScore : Cl_GameData.highestScore;
        }
        Cl_GameData.resetData();

        Sc_SelectionHandlersController.ClearLists();
        Sc_GameController.Instance.DestroyGame();
        SceneManager.LoadScene("Menu");
    }

    public void Rematch()
    {
        Cl_GameData.currentBlueScore = blueScore;
        Cl_GameData.currentRedScore = redScore;
        Sc_SelectionHandlersController.ClearLists();
        Sc_GameController.Instance.DestroyGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
