using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cl_CardInputRow
{
    public int objectsCount = 0;
    public En_Pawn firstObject;
    public List<Cl_CardHolder> cardHolders = new List<Cl_CardHolder>();

    public Cl_CardHolder findElement(Transform pos)
    {
        return cardHolders.Find(c => c.Position == pos);
    }

    public int findElementPos(Transform pos)
    {
        return cardHolders.FindIndex(c => c.Position == pos);
    }
}
