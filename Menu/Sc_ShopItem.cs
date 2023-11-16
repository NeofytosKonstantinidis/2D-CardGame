using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sc_ShopItem : MonoBehaviour
{
    [SerializeField]
    Image cardIcon;
    [SerializeField]
    TMP_Text cost;
    [SerializeField]
    TMP_Text oldCost;
    [SerializeField]
    GameObject oldCostPanel;
    [SerializeField]
    Button saleButton;
    [SerializeField]
    TMP_Text buttonText;
    [SerializeField]
    GameObject onSaleText;

    [SerializeField]
    Color ownedColor;
    [SerializeField]
    Color ownedHoveredColor;
    [SerializeField]
    Color ownedDisabledColor;
    [SerializeField]
    Color disabledTextColor;

    float itemCost { get; set; }
    private bool owned = false;
    private bool equipped = false;
    private bool commingSoon = false;
    private int itemId;
    private Sc_Shop shop;
    public void SetData(int id, Sprite cardImage, float cost, float saleCost, bool owned, bool equipped, bool commingSoon, Sc_Shop Shop)
    {
        itemId = id;
        shop = Shop;
        cardIcon.sprite = cardImage;
        if(saleCost < cost)
        {
            itemCost = saleCost;
            this.cost.text = (int)saleCost + "";
            oldCost.text = (int)cost + "";
            oldCostPanel.SetActive(true);
            onSaleText.SetActive(true);
        }
        else
        {
            this.cost.text = (int)cost + "";
        }
        this.owned = owned;
        this.equipped = equipped;
        this.commingSoon = commingSoon;

        checkAvailabilityAndOwnership();
    }
    public void Bought()
    {
        owned = true;
    }
    public void Equipped()
    {
        equipped = true;
    }
    public void checkAvailabilityAndOwnership()
    {
        saleButton.interactable = true;
        saleButton.onClick.AddListener(() => { shop.Buy(itemId, gameObject); });
        if (itemCost > Cl_GameData.coins && !owned)
        {
            saleButton.interactable = false;
            buttonText.color = disabledTextColor;
        }
        if (owned)
        {
            saleButton.onClick.RemoveAllListeners();
            ColorBlock cb = saleButton.colors;
            cb.normalColor = ownedColor;
            cb.selectedColor = ownedHoveredColor;
            cb.pressedColor = ownedHoveredColor;
            cb.disabledColor = ownedDisabledColor;
            if (equipped)
            {
                buttonText.text = "Equipped";
                saleButton.interactable = false;
            }
            else
            {
                saleButton.onClick.AddListener(() => { shop.Equip(itemId, gameObject); });
                buttonText.text = "Equip";
                saleButton.interactable = true;
            }
        }
        if (commingSoon)
        {
            saleButton.interactable = false;
            buttonText.text = "Comming Soon";
            cost.text = "??";
        }
    }
}
