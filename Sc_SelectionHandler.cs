using UnityEngine;

public class Sc_SelectionHandler : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    private int teamNum = 0;

    [SerializeField]
    [Range(0, 5)]
    private int pawnNum = 0;

    [SerializeField]
    private bool isDeckHandler = false;

    public int getTeamNum()
    {
        return teamNum;
    }
    public int getPawnNum() 
    {  
        return pawnNum;
    }

    private void Start()
    {
        if (isDeckHandler) { Sc_SelectionHandlersController.AddDeckHandler(gameObject); }
        else if(teamNum == 0) { Sc_SelectionHandlersController.AddT1Handler(gameObject); }
        else { Sc_SelectionHandlersController.AddT2Handler(gameObject); }
        gameObject.SetActive(false);
    }
    public void Selected()
    {
        Sc_GameController.Instance.SelectedInput(teamNum, pawnNum);
        if (Sc_GameController.Instance.GetCard().GetComponent<Sc_Card>().pawn != 0)
        {
            if (isDeckHandler) { Sc_SelectionHandlersController.RemoveDeckHandler(gameObject); }
            else if (teamNum == 0) { Sc_SelectionHandlersController.RemoveT1Handler(gameObject); }
            else { Sc_SelectionHandlersController.RemoveT2Handler(gameObject); }
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
