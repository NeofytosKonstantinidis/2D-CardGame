using System;
using System.Collections.Generic;
using UnityEngine;


public static class Sc_SelectionHandlersController
{
    private static List<GameObject> t1selectionHandlers = new List<GameObject>();
    private static List<GameObject> t1PlacedHandlers = new List<GameObject>();
    private static List<GameObject> t2selectionHandlers = new List<GameObject>();
    private static List<GameObject> t2PlacedHandlers = new List<GameObject>();
    private static List<GameObject> deckHandlers = new List<GameObject>();

    public static void AddT1Handler(GameObject handler)
    {
        t1selectionHandlers.Add(handler);
    }
    public static void AddT2Handler(GameObject handler)
    {
        t2selectionHandlers.Add(handler);
    }

    public static List<GameObject> GetT2Handlers()
    {
        return t2selectionHandlers;
    }
    public static List<GameObject> GetT2PlacedHandlers()
    {
        return t2PlacedHandlers;
    }

    public static void AddDeckHandler(GameObject handler)
    {
        deckHandlers.Add(handler);
    }


    public static void disableT1Handlers()
    {
        foreach (GameObject handler in t1selectionHandlers)
        {
            handler.SetActive(false);
        }
        foreach (GameObject handler in t1PlacedHandlers)
        {
            handler.SetActive(false);
        }
    }

    public static void EnableT1Handlers()
    {
        foreach (GameObject handler in t1selectionHandlers)
        {
            handler.SetActive(true);
        }
    }

    public static void EnableT1PlacedHandlers()
    {
        foreach (GameObject handler in t1PlacedHandlers)
        {
            handler.SetActive(true);
        }
    }

    public static void disableT2Handlers()
    {
        foreach (GameObject handler in t2selectionHandlers)
        {
            handler.SetActive(false);
        }
        foreach (GameObject handler in t2PlacedHandlers)
        {
            handler.SetActive(false);
        }
    }

    public static void EnableT2Handlers()
    {
        foreach (GameObject handler in t2selectionHandlers)
        {
            handler.SetActive(true);
        }
    }

    public static void EnableT2PlacedHandlers()
    {
        foreach (GameObject handler in t2PlacedHandlers)
        {
            handler.SetActive(true);
        }
    }

    public static void disableDeckHandlers()
    {
        foreach (GameObject handler in deckHandlers)
        {
            handler.SetActive(false);
        }
    }

    public static void EnableDeckHandlers()
    {
        foreach (GameObject handler in deckHandlers)
        {
            handler.SetActive(true);
        }
    }

    public static void RemoveDeckHandler(GameObject gameObject)
    {
        deckHandlers.Remove(gameObject);
    }

    public static void RemoveT1Handler(GameObject gameObject)
    {
        t1selectionHandlers.Remove(gameObject);
        t1PlacedHandlers.Add(gameObject);
    }

    public static void RemoveT1PlacedHandler(int round, int column)
    {
        GameObject g = t1PlacedHandlers.Find(x => x.GetComponent<Sc_SelectionHandler>().getTeamNum() == round && (x.GetComponent<Sc_SelectionHandler>().getPawnNum() == column));
        t1PlacedHandlers.Remove(g);
        g.GetComponent<Sc_SelectionHandler>().Destroy();
    }

    public static void RemoveT2Handler(GameObject gameObject)
    {
        t2selectionHandlers.Remove(gameObject);
        t2PlacedHandlers.Add(gameObject);
    }

    public static void RemoveT2PlacedHandler(int round, int column)
    {
        GameObject g = t2PlacedHandlers.Find(x => x.GetComponent<Sc_SelectionHandler>().getTeamNum() == round && (x.GetComponent<Sc_SelectionHandler>().getPawnNum() == column));
        t2PlacedHandlers.Remove(g);
        g.GetComponent<Sc_SelectionHandler>().Destroy();

    }

    public static void ClearLists()
    {
        t1selectionHandlers.Clear();
        t1PlacedHandlers.Clear();
        t2selectionHandlers.Clear();
        t2PlacedHandlers.Clear();
        deckHandlers.Clear();
    }
}
