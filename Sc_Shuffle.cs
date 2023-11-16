using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public static class Sc_Shuffle
{
    public static List<GameObject> ShuffleList(List<GameObject> list, Action completedAction)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sc_ShuffleAnimation.SendAction(completedAction);
            GameObject temp = list[i];
            int rand = UnityEngine.Random.Range(i, list.Count);
            if (rand == i) continue;
            Debug.Log(list[i].name + " FROM " + list[i].transform.position.y + " TO " + list[rand].transform.position.y + " AND REVERSE "+ list[rand].name);
            Sc_ShuffleAnimation.addToWaitList(list[i], list[rand], i);
            list[i] = list[rand];
            list[rand] = temp;
        }
        string listItems = "";
        for (int i = 0; i < list.Count; i++)
        {
            listItems = listItems + " " + list[i].name;
        }
        Debug.Log(listItems);
        return list;
    }
}
