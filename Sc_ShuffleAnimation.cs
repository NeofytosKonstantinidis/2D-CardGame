using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Sc_ShuffleAnimation
{
    public static List<DelayObjects> delObjects = new List<DelayObjects>();
    private static Action completedAction;

    public class DelayObjects
    {
        public GameObject card;
        public GameObject otherCard;
        public float delay;

        public DelayObjects(GameObject card,  GameObject otherCard, float delay)
        {
            this.card = card;
            this.otherCard = otherCard;
            this.delay = delay;
        }
    }
    private static bool onCooldown = false;

    public static void addToWaitList(GameObject card, GameObject otherCard, float delay)
    {
        delObjects.Add(new DelayObjects(card, otherCard, delay));
        if (onCooldown) { return;}
        onCooldown = true;
        doShuffle(card, otherCard.transform.position.y, 1, delay);
        doShuffle(otherCard, card.transform.position.y, -1, delay);
    }
    public static void doShuffle(GameObject card, float positionToGet, int side, float delay)
    {
        float posX = card.transform.position.x;
        LeanTween.moveX(card, posX + (10.1f * side), .05f).setDelay(.05f)
            .setOnComplete(x =>
            {
                LeanTween.moveY(card, positionToGet, .02f)
                .setOnComplete(y => { LeanTween.moveX(card, posX, .05f).setOnComplete(()=> { callNext(side); }); });
            });

        //.setEase(LeanTweenType.easeOutSine)
    }

    public static void SendAction(Action action)
    {
        completedAction = action;
    }

    public static void callNext(int side)
    {
        if (side != 1) return;
        delObjects.RemoveAt(0);
        if (delObjects.Count == 0) { completedAction.Invoke(); return; }
        doShuffle(delObjects[0].card, delObjects[0].otherCard.transform.position.y, 1, delObjects[0].delay);
        doShuffle(delObjects[0].otherCard, delObjects[0].card.transform.position.y, -1, delObjects[0].delay);
    }

    public static void ClearData()
    {
        onCooldown = false;
        delObjects.Clear();
    }
}
