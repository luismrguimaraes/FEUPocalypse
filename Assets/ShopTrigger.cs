using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopUI shopUI;
    private bool isShopping = false;
    private bool inZone = false;
    MainCharMovementScript mcMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mcMove = collision.GetComponent<MainCharMovementScript>();

        inZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shopUI.HideShop();
        inZone = false;
    }

    private void ActivateShop()
    {
        mcMove.SetStopMoving(true);

        shopUI.ShowShop();
    }

    private void DeactivateShop()
    {

        shopUI.HideShop();
        mcMove.SetStopMoving(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.Backspace))
        {
            inZone = false;
            DeactivateShop();
        }

        if (inZone && mcMove.IsMCFacingUp())
        {
            ActivateShop();
        }

        if (!isShopping)
        {
            return;
        }
    }
}
