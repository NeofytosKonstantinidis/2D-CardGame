using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Transform cardSpawner;
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private int cardsToSpawn;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float distance;
    [SerializeField]
    private int direction = -1;

    private List<GameObject> cards = new List<GameObject>();
    void Start()
    {
        Sc_SkinManager.Instance.completed += (s, args) => { LoadCards(); };
        
    }

    private void LoadCards()
    {
        for (int i = 0; i < cardsToSpawn; i++)
        {
            GameObject c = Instantiate(card, cardSpawner.position + direction * distance * i * Vector3.right, cardSpawner.rotation, cardSpawner);
            c.GetComponent<Sc_Card>().randomCardData(Sc_SkinManager.Instance.getCurrentSkin());
            cards.Add(c);
        }
        AnimateCards();
    }



    private void AnimateCards()
    {
        LeanTween.move(cardSpawner.gameObject, pos.position, duration).setEaseInOutSine().setLoopPingPong();
    }
}
