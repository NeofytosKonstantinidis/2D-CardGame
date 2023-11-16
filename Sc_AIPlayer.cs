using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AIPlayer
{
    public void playTurn(GameObject card)
    {
        List<GameObject> handlers = Sc_SelectionHandlersController.GetT2Handlers();
        if (card.GetComponent<Sc_Card>().pawn == 0)
            handlers = Sc_SelectionHandlersController.GetT2PlacedHandlers();
        handlers[Random.Range(0, handlers.Count-1)].GetComponent<Sc_SelectionHandler>().Selected();
    }
}
