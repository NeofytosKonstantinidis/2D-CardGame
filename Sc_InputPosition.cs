using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Sc_InputPosition : MonoBehaviour
{
    private Vector3 screenPosition;
    [SerializeField]
    private LayerMask layerMask;
    private Ray ray;
    private Collider hitDataCollider;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.GamePlay.Click.performed += clickedCollider;
        playerControls.GamePlay.Click.Enable();
        playerControls.GamePlay.PositionOfPointer.Enable();
    }
    private void Update()
    {
        //movePointer();
    }

    public void movePointer()
    {
        screenPosition = Mouse.current.position.ReadValue();
        ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 200, layerMask))
        {
            if (hitDataCollider == hitData.collider) { return; }
            hitDataCollider = hitData.collider;
            LeanTween.scale(hitDataCollider.gameObject, new Vector3(1.1f, 1.1f, 1.1f), .5f).setEaseInOutSine().setLoopPingPong();
        }
        else
        {
            if (hitDataCollider != null)
            {
                LeanTween.cancel(hitDataCollider.gameObject);
                hitDataCollider.transform.localScale = new Vector3(1, 1, 1);
                hitDataCollider = null;
            }
        }
    }

    
    public void clickedCollider(CallbackContext ctx)
    {
        Vector2 pos = playerControls.GamePlay.PositionOfPointer.ReadValue<Vector2>();
        Debug.Log(pos);
        ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out RaycastHit hitData, 200, layerMask))
        {
            hitDataCollider = hitData.collider;
            hitDataCollider.GetComponent<Sc_SelectionHandler>().Selected();
            hitDataCollider.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
