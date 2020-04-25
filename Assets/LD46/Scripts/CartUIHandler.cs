using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartUIHandler : MonoBehaviour
{
    GameObject FruitManager = null;
    PlayerInputHandler inputHandler;
    public static bool isCurrentlyActive = false;
    // Start is called before the first frame update
    void Start()
    {
        FruitManager = GameObject.Find("FruitManager");
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();

        SetActiveCart(false);
    }

    void ToggleActive()
    {
        SetActiveCart(!isCurrentlyActive);
    }

    void SetActiveCart(bool isActive)
    {
        //FruitManager.SetActive(isActive);
        FruitManager.GetComponent<Canvas>().enabled = isActive;
        

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        isCurrentlyActive = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!GameMenuManager.isCurrentlyActive) && Input.GetButtonDown(GameConstants.k_ButtonNameShowCart))
        {
            ToggleActive();
            //SetActiveCart(true);
        }

        /*if (Input.GetButtonUp(GameConstants.k_ButtonNameShowCart))
        {
            SetActiveCart(false);
        }*/
    }
}
