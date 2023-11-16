using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Sc_CardSharer : MonoBehaviour
{
    [SerializeField]
    private List<Cl_CardInputRow> BlueCardHolders = new List<Cl_CardInputRow>();

    [SerializeField]
    private List<Cl_CardInputRow> RedCardHolders = new List<Cl_CardInputRow>();

    [SerializeField]
    private Transform StartCardPosition;

    [SerializeField]
    private float animationTime = .5f;
    [SerializeField]
    private float animationCooldown = .2f;

    [SerializeField]
    private Transform BlueSideTrashPosition;
    [SerializeField]
    private Transform RedSideTrashPosition;

    public GameObject lastCard;

    public GameObject getLastCard()
    {
        GameObject temp = lastCard;
        lastCard = null;
        return temp;
    }
    public void shareCards(List<GameObject> cards)
    {
        StartCoroutine(waitForShuffling(cards));
    }

    IEnumerator waitForShuffling(List<GameObject> cards)
    {
        //(cards.Count) * .17f
        yield return new WaitForSeconds(.5f);
        int cardCounter = cards.Count-1;
        GameObject card;
        for (int i = 0; i < BlueCardHolders.Count; i++)
        {
            for (int j = 0; j < BlueCardHolders[i].cardHolders.Count - 1; j++)
            {
                card = cards[cardCounter];
                card.GetComponent<Sc_Card>().currentPos = BlueCardHolders[i].cardHolders[j].Position;
                BlueCardHolders[i].cardHolders[j].setCard(card);
                StartCoroutine(animateCard(card, BlueCardHolders[i].cardHolders[j].Position, cards.Count - cardCounter));
                cardCounter--;
                card = cards[cardCounter];
                card.GetComponent<Sc_Card>().currentPos = RedCardHolders[i].cardHolders[j].Position;
                RedCardHolders[i].cardHolders[j].setCard(card);
                StartCoroutine(animateCard(card, RedCardHolders[i].cardHolders[j].Position, cards.Count - cardCounter));
                cardCounter--;
            }
        }
        card = cards[cardCounter];
        card.GetComponent<Sc_Card>().currentPos = StartCardPosition;
        card.GetComponent<Sc_Card>().flipCard();
        lastCard = cards[0];
        StartCoroutine(animateCard(card, StartCardPosition, cards.Count - cardCounter, true));
    }

    IEnumerator animateCard(GameObject card, Transform t, float cooldown)
    {
        yield return new WaitForSeconds(cooldown * animationCooldown);
        LeanTween.rotate(card, t.rotation.eulerAngles, animationTime).setEaseInSine();
        LeanTween.move(card, t.position + new Vector3(0, .01f, 0), animationTime).setEaseInSine();
    }
    IEnumerator animateCard(GameObject card, Transform t, float cooldown, bool last)
    {
        yield return new WaitForSeconds(cooldown * animationCooldown);
        LeanTween.rotate(card, t.rotation.eulerAngles, animationTime).setEaseInSine();

        LeanTween.move(card, t.position + new Vector3(0, .01f, 0), animationTime).setEaseInSine().setOnComplete(() => { Sc_GameController.Instance.SharingCompleted(card); });
    }
    public void flipCard(GameObject card)
    {
        Debug.Log("Flipped " + card.name);
        float currY = card.transform.position.y;
        LeanTween.moveLocalY(card, currY + 1f, .25f);
        LeanTween.rotateZ(card, 180, .25f).setDelay(.25f);
        LeanTween.moveLocalY(card, currY, .25f).setDelay(.5f);
    }

    public Transform checkIfAvailable(int team, int pawn)
    {
        if (team == 2)
        {
            return null;
        }
        List<Cl_CardInputRow> CHolders = team == 0 ? BlueCardHolders : RedCardHolders;
        foreach (Cl_CardInputRow cardHolderRow in CHolders)
        {
            if((int)cardHolderRow.firstObject == pawn)
            {
                return cardHolderRow.cardHolders[0].Position;
            }
        }
        return null;
    }

    public bool checkCompletance(int team, int pawn)
    {
        if (team == 2)
        {
            return false;
        }
        List<Cl_CardInputRow> CHolders = team == 0 ? BlueCardHolders : RedCardHolders;
        foreach (Cl_CardInputRow cardHolderRow in CHolders)
        {
            if ((int)cardHolderRow.firstObject == pawn)
            {
                return cardHolderRow.objectsCount>=4;
            }
        }
        return false;
    }
    public void MoveTrashCard(GameObject card,  int team)
    {
        LeanTween.move(card, team == 0 ? BlueSideTrashPosition.position : RedSideTrashPosition.position, 1f);
    }
    public int CheckForWinner()
    {
        int blueScore = GetBlueScore();
        int redScore = GetRedScore();
        Debug.Log("Blue: "+ blueScore + "Red: "+ redScore);
        if (blueScore == 24 || redScore == 24)
        {
            return blueScore > redScore ? 1 : 2;
        }
        return 0;
    }
    public int GetBlueScore()
    {
        return BlueCardHolders.Sum(x => x.objectsCount);
    }

    public int GetRedScore()
    {
        return RedCardHolders.Sum(x => x.objectsCount);
    }

    public Transform findCard(int team, int pawn)
    {
        List<Cl_CardInputRow> CHolders = team == 0 ? BlueCardHolders : RedCardHolders;
        return CHolders[pawn].cardHolders[0].Position;
    }

    public void moveCardToLocation(GameObject card, Transform location)
    {
        moveCardTo(card, location);
        card.GetComponent<Sc_Card>().TurnOnLight();
        int round = Sc_GameController.Instance.getPlayerRound();
        List<Cl_CardInputRow> CHolders = round == 0 ? BlueCardHolders : RedCardHolders;
        int column = 0;
        foreach (Cl_CardInputRow cRow in CHolders)
        {
            if (cRow.findElementPos(location) > -1) { break; }
            column++;
        }
        CHolders[column].objectsCount++;
        if (CHolders[column].objectsCount >= 4)
        {
            if (round == 0)
                Sc_SelectionHandlersController.RemoveT1PlacedHandler(round, column);
            else
                Sc_SelectionHandlersController.RemoveT2PlacedHandler(round, column);
        }
        if (CHolders[column].firstObject == 0)
        {
            CHolders[column].firstObject = card.GetComponent<Sc_Card>().pawn;
        }
        List <Cl_CardHolder> cHolders = CHolders[column].cardHolders;
        GameObject c;
        GameObject temp = card;
        for (int i = 0; i < cHolders.Count; i++)
        {
            c = cHolders[i].getCard();
            cHolders[i].setCard(temp);
            if (i == cHolders.Count-1) 
            {
                StartCoroutine(WaitForNextCard(temp));
                break;
            }
            temp = c;
            moveCardTo(c, cHolders[i+1].Position);
        }
    }

    IEnumerator WaitForNextCard(GameObject c)
    {
        yield return new WaitForSeconds(.5f);
        Sc_GameController.Instance.getNextCard(c);
    }

    private void moveCardTo(GameObject card, Transform location)
    {
        LeanTween.move(card, location.position, .25f);
        LeanTween.rotate(card, new Vector3(location.rotation.eulerAngles.x, location.rotation.eulerAngles.y, card.transform.rotation.eulerAngles.z), .25f);
    }

}
