
using System;
using TMPro;
using UnityEngine;

public class Sc_Card : MonoBehaviour
{
    public En_Side side;
    public En_Pawn pawn;
    public Transform currentPos;
    public bool placed = false;
    public bool flipped = false;

    [SerializeField]
    MeshRenderer frontMat;
    [SerializeField]
    MeshRenderer backMat;
    [SerializeField]
    TextMeshPro number;
    [SerializeField]
    GameObject cross;
    [SerializeField]
    GameObject lightObject;

    internal void flipCard()
    {
        Debug.Log("Card Data{\n Side: " + side + "\nPawn: " + pawn + "\n}");
    }

    public void setCardData(So_Skin skin, int team, int pawn)
    {
        frontMat.material = skin.frontMat;
        switch (team)
        {
            case 0:
                backMat.material = skin.backMatBlue;
                break;
            case 1:
                backMat.material = skin.backMatRed;
                break;
            default:
                backMat.material = skin.backMatNeutral;
                break;
        }
        number.text = pawn <= 6 ? pawn + "" : "?";
    }

    public void randomCardData(So_Skin skin)
    {
        int team = UnityEngine.Random.Range(0, Enum.GetNames(typeof(En_Side)).Length -1);
        int x = UnityEngine.Random.Range(0, 10);
        if (x > 8)
        {
            team = 2;
        }
        int pawn= 0;
        if (team > 1)
        {
            pawn = 7;
        }
        else
        {
            pawn = UnityEngine.Random.Range(1, Enum.GetNames(typeof(En_Pawn)).Length);
        }
        setCardData(skin, team, pawn);
    }

    public void EnableCross()
    {
        LeanTween.scale(cross, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEaseInBounce();
    }

    public void TurnOnLight()
    {
        lightObject.SetActive(true);
    }
}
