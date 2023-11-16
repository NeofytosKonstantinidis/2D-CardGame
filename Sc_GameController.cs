using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_GameController : MonoBehaviour
{
    private int playerTurn = -1;
    public bool isLocalMulti { get; set; } = true;
    private bool isDeckEnabled = false;
    private GameObject card;

    [SerializeField]
    private Sc_CardController cardController;
    [SerializeField]
    private Sc_UIManager UIManager;
    private Sc_AIPlayer AIPlayer;

    public static Sc_GameController Instance;

    private int type;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isLocalMulti = Cl_GameData.isLocalMulti;
        if (!isLocalMulti) { AIPlayer = new Sc_AIPlayer(); }
    }

    public GameObject GetCard() { return card; }
    public int getPlayerRound()
    {
        return playerTurn;
    }

    public void SharingCompleted(GameObject card)
    {
        this.card = card;
        playerTurn = (int)card.GetComponent<Sc_Card>().side;
        if (playerTurn == 2)
        {
            playerTurn = Random.Range(0, 1);
        }
        UIManager.setPlayerPlaying(playerTurn);
        cardController.flipCard(card);
        StartCoroutine(waitForNextRound());
    }

    
    public void getNextCard(GameObject card)
    {

        this.card = card;
        cardController.flipCard(card);
        int p = (int)card.GetComponent<Sc_Card>().side;
        int res = cardController.CheckForWinner();
        if (res > 0)
        {
            EndGame(res-1, 0);
            return;
        }
        if (cardController.checkCompletance(p, (int)card.GetComponent<Sc_Card>().pawn))
        {
            card.GetComponent<Sc_Card>().EnableCross();
            cardController.moveTrashCard(card, (int)card.GetComponent<Sc_Card>().side);
            GameObject temp = cardController.getLastCard();
            if (temp != null)
            {
                ChangeTurn();
                getNextCard(temp);
            }
            else
            {
                EndGame((playerTurn + 1) % 2, 1);
            }
            return;
        }
        if (p != playerTurn && p!=2)
        {
            ChangeTurn();
        }
        UIManager.setPlayerPlaying(playerTurn);
        Transform t = cardController.checkSelection(p, (int)card.GetComponent<Sc_Card>().pawn);
        if (t != null)
        {
            StartCoroutine(waitForAutoPlay(t));
            return;
        }
        StartCoroutine(waitForNextRound());
    }

    private void EndGame(int type, int from)
    {
        Debug.Log(type + " won.");
        UIManager.gameEnded(type, from);
    }

    public void DestroyGame()
    {
        Sc_SkinManager.Instance.DestroySkinManager();
        Sc_ShuffleAnimation.ClearData();
        Destroy(this);
    }
    IEnumerator waitForNextRound()
    {
        yield return new WaitForSeconds(1f);
        EnablePlayerInputs(playerTurn);
    }
    IEnumerator waitForAutoPlay(Transform t)
    {
        yield return new WaitForSeconds(1f);
        cardController.setSelection(card, t); ;
    }
    public void SelectedInput(int team, int pawn)
    {
        if (playerTurn == team) 
        {
            Sc_SelectionHandlersController.disableT2Handlers();
            Sc_SelectionHandlersController.disableT1Handlers();
            cardController.setSelection(card, team, pawn);
        }
    }

    public int GetScore(int team)
    {
        return cardController.GetScore(team);
    }

    public void SelectedInput()
    {
        if (isDeckEnabled)
        {

        }
    }

    public void ChangeTurn()
    {
        playerTurn = (playerTurn + 1) % 2;
        Debug.Log("Changing to player: "+ playerTurn);
    }

    public void EnablePlayerInputs(int player)
    {
        Debug.Log("Enabled Inputs");
        switch (player)
        {
            case 0:
                Sc_SelectionHandlersController.EnableT1Handlers();
                Sc_SelectionHandlersController.disableT2Handlers();
                if(card.GetComponent<Sc_Card>().pawn == 0)
                {
                    Sc_SelectionHandlersController.EnableT1PlacedHandlers();
                }
                break;
            case 1:
                if (isLocalMulti)
                {
                    Sc_SelectionHandlersController.EnableT2Handlers();
                    if (card.GetComponent<Sc_Card>().pawn == 0)
                    {
                        Sc_SelectionHandlersController.EnableT2PlacedHandlers();
                    }
                }
                else
                {
                    AIPlayer.playTurn(card);
                }
                Sc_SelectionHandlersController.disableT1Handlers();
                break;

        }
    }
}
