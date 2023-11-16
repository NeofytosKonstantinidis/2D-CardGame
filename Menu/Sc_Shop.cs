using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Transform cardsHolder;
    
    private List<GameObject> cardList = new List<GameObject>();

    public void Buy(int itemId, GameObject cardItem)
    {

    }

    public void Equip(int id, GameObject cardItem)
    {

    }
    private void Start()
    {
        Sc_SkinManager.Instance.completed += (s, args) => { CreateCards(); };
    }
    public void CreateCards()
    {
        //List<int> skinsList = Cl_GameData.skinsOwned;
        List<int> skinsList = new List<int>() { 0};
        int currSkin = Cl_GameData.currentSkin;
        foreach (So_Skin cardSkin in Sc_SkinManager.Instance.Skins)
        {
            GameObject card = Instantiate(cardPrefab, cardsHolder);
            int id = cardSkin.id;
            bool  owned = (currSkin == id);
            card.GetComponent<Sc_ShopItem>().SetData(id, cardSkin.shopImage, cardSkin.cost, cardSkin.saleCost, skinsList.Contains(id), owned, cardSkin.commingSoon, this);
            cardList.Add(card);
        }
    }

    public void ClearList() 
    { 
        foreach (GameObject card in cardList)
        {
            Destroy(card);
        }
        cardList = new List<GameObject>();
    }
}
