using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_MenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject soloBackground;
    [SerializeField]
    GameObject duoBackground;
    [SerializeField]
    GameObject shopBackground;
    [SerializeField]
    GameObject howtoplayBackground;

    [SerializeField]
    GameObject shopUI;

    [SerializeField]
    GameObject howToUI;

    [SerializeField]
    GameObject gameLoading;
    [SerializeField]
    GameObject gameLoadingBar;
    [SerializeField]
    TMP_Text coinsText;

    private void Start()
    {
        coinsText.text = Cl_GameData.coins+"";
    }

    public void SoloClicked()
    {
        DisableAllElements();
        soloBackground.SetActive(true);
        Cl_GameData.isLocalMulti=false;
        LeanTween.scale(soloBackground, new Vector3(30, 30, 30), .5f).setEaseInQuint().setOnComplete(EnableLoading);
    }

    public void DuoClicked()
    {
        duoBackground.SetActive(true);
        Cl_GameData.isLocalMulti = true;
        LeanTween.scale(duoBackground, new Vector3(30, 30, 30), .5f).setEaseInQuint().setOnComplete(EnableLoading);
    }

    private void EnableLoading()
    {
        gameLoading.SetActive(true);
        LeanTween.scaleX(gameLoadingBar, 1f, 1f).setEaseInQuint().setOnComplete(()=> SceneManager.LoadScene("Game"));
    }

    public void ShopClicked()
    {
        DisableAllElements();
        shopBackground.SetActive(true);
        LeanTween.scale(shopBackground, new Vector3(30, 30, 30), .5f).setEaseInQuint().setOnComplete(EnableShopUI);
    }

    private void EnableShopUI()
    {
        shopUI.SetActive(true);
        LeanTween.value(shopUI, 0f, 1f, .5f).setOnUpdate((float value) => { shopUI.GetComponent<CanvasGroup>().alpha = value; }) ;
    }

    public void HowtoplayClicked()
    {
        DisableAllElements();
        howtoplayBackground.SetActive(true);
        LeanTween.scale(howtoplayBackground, new Vector3(45, 45, 45), .5f).setEaseInQuint().setOnComplete(EnableHowToUI);
    }

    private void EnableHowToUI()
    {
        howToUI.SetActive(true);
        LeanTween.value(howToUI, 0f, 1f, .5f).setOnUpdate((float value) => { howToUI.GetComponent<CanvasGroup>().alpha = value; });
    }

    public void BackFromShop()
    {
        LeanTween.value(shopUI, 1f, 0f, .5f).setOnUpdate((float value) => { shopUI.GetComponent<CanvasGroup>().alpha = value; }).setOnComplete(() =>
        {
            shopUI.SetActive(false);
            LeanTween.scale(shopBackground, new Vector3(1, 1, 1), .5f).setEaseOutQuint().setOnComplete(()=> { shopBackground.SetActive(false); });
        });
    }

    public void BackFromHowTo()
    {
        LeanTween.value(howToUI, 1f, 0f, .5f).setOnUpdate((float value) => { howToUI.GetComponent<CanvasGroup>().alpha = value; }).setOnComplete(() =>
        {
            howToUI.SetActive(false);
            LeanTween.scale(howtoplayBackground, new Vector3(1, 1, 1), .5f).setEaseOutQuint().setOnComplete(() => { howtoplayBackground.SetActive(false); });
        });
    }

    private void DisableAllElements()
    {
        soloBackground.SetActive(false);
        duoBackground.SetActive(false);
        shopBackground.SetActive(false);
        gameLoading.SetActive(false);
        howtoplayBackground.SetActive(false);
        gameLoadingBar.transform.localScale = new Vector3(0, 1, 1);
    }
}
