using UnityEngine;

[System.Serializable]
public class Cl_CardHolder
{
    public Transform Position;
    public GameObject Card;


    public void setCard(GameObject Card)
    {
        this.Card = Card;
    }

    public GameObject getCard() { return Card; }
}
