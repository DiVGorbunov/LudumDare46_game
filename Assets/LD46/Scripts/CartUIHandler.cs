using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartUIHandler : MonoBehaviour
{
    GameObject FruitManager = null;
    PlayerInputHandler inputHandler;
    // Start is called before the first frame update
    void Start()
    {
        FruitManager = GameObject.Find("FruitManager");
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();

        SetActiveCart(false);
    }

    void SetActiveCart(bool isActive)
    {
        //FruitManager.SetActive(isActive);
        FruitManager.GetComponent<Canvas>().enabled = isActive;
        Cursor.visible = isActive;

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TurretPlayerInputHandler.instance.CanProcessInput() && Input.GetButtonDown(GameConstants.k_ButtonNameShowCart))
        {
            SetActiveCart(true);
        }

        if (TurretPlayerInputHandler.instance.CanProcessInput() && Input.GetButtonUp(GameConstants.k_ButtonNameShowCart))
        {
            SetActiveCart(false);
        }
    }
}
