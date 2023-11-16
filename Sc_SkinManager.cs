using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_SkinManager : MonoBehaviour
{
    public List<So_Skin> Skins { private set; get; }

    private int selectedSkin = 0;

    public static Sc_SkinManager Instance;

    public bool readyToUse = false;

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
        So_Skin[] s = Resources.LoadAll<So_Skin>("Skins/Card");
        Skins = s.ToList();
        StartCoroutine(LoadCards());
    }

    IEnumerator LoadCards()
    {
        yield return new WaitForSeconds(.2f);
        completed.Invoke(this, EventArgs.Empty);
    }

    public EventHandler completed;
    public void setCurrentSkin(int skin)
    {
        selectedSkin = skin;
    }

    public So_Skin getCurrentSkin()
    {
        return Skins[Cl_GameData.currentSkin];
    }
    
    public void DestroySkinManager()
    {
        Destroy(this);
    }

    
}
