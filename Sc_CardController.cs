using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardController : MonoBehaviour
{
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Transform cardSpawn;

    private Sc_CardSharer cardSharer;


    private List<GameObject> cards = new List<GameObject>();

    public void CreateCards()
    {
        int counter = 1;
        for (int i = 0; i < Enum.GetNames(typeof(En_Side)).Length - 1; i++)
        {
            for (int j = 0; j < Enum.GetNames(typeof(En_Pawn)).Length; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    GameObject cardObj = Instantiate(card, new Vector3(cardSpawn.position.x, cardSpawn.position.y + (0.09f * counter), cardSpawn.position.z), cardSpawn.rotation, cardSpawn);
                    cardObj.name = "Card_" + counter;
                    Sc_Card cardScript = cardObj.GetComponent<Sc_Card>();
                    cardScript.setCardData(Sc_SkinManager.Instance.getCurrentSkin(), i, j+1);
                    cardScript.side = (En_Side)i;
                    cardScript.pawn = (En_Pawn)j + 1;
                    cards.Add(cardObj);
                    counter++;
                }
            }
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject cardObj = Instantiate(card, new Vector3(cardSpawn.position.x, cardSpawn.position.y + (0.09f * counter), cardSpawn.position.z), cardSpawn.rotation, cardSpawn);
            Sc_Card cardScript = cardObj.GetComponent<Sc_Card>();
            cardObj.name = "Card_" + counter;
            cardScript.side = (En_Side)2;
            cardScript.setCardData(Sc_SkinManager.Instance.getCurrentSkin(), 3, 7);
            cards.Add(cardObj);
            counter++;
        }
    }

    public void ShuffleCards()
    {
        cards = Sc_Shuffle.ShuffleList(cards, () => { ShareCards(); });
    }

    public void ShareCards()
    {
        cardSharer.shareCards(cards);
    }

    private void Start()
    {
        cardSharer = GetComponent<Sc_CardSharer>();
        Sc_SkinManager.Instance.completed += (s, args) => { createCards(); };
    }

    public void createCards()
    {
        CreateCards();
        ShuffleCards();
    }

    public Transform checkSelection(int team, int pawn)
    {
        Transform t = cardSharer.checkIfAvailable(team, pawn);
        return t;

    }

    public bool checkCompletance(int team, int pawn)
    {
        return cardSharer.checkCompletance(team, pawn);
    }

    public int CheckForWinner()
    {
        return cardSharer.CheckForWinner();
    }

    public int GetScore(int team)
    {
        return team == 0 ? cardSharer.GetBlueScore(): cardSharer.GetRedScore();
    }
    public void moveTrashCard(GameObject card, int team)
    {
        cardSharer.MoveTrashCard(card, team);
    }
    public void setSelection(GameObject card, Transform location)
    {
        cardSharer.moveCardToLocation(card, location);
    }

    public void setSelection(GameObject card, int team, int pawn)
    {
        Transform location = cardSharer.findCard(team, pawn);
        cardSharer.moveCardToLocation(card, location);
    }

    public void flipCard(GameObject card)
    {
        cardSharer.flipCard(card);
    }

    public GameObject getLastCard()
    {
        return cardSharer.getLastCard();
    }
}
