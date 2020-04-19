using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayerInputHandler : PlayerInputHandler
{
    override protected void Start()
    {
        base.Start();
    }



    public override bool CanProcessInput()
    {
        if (Cursor.lockState == CursorLockMode.None && Cursor.visible) return false;
        return base.CanProcessInput();
    }
}
