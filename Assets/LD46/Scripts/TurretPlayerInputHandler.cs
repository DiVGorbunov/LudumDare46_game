using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlayerInputHandler : PlayerInputHandler
{
    public static TurretPlayerInputHandler instance = null;
    override protected void Start()
    {
        base.Start();
        instance = this;
    }



    public override bool CanProcessInput()
    {
        if (Cursor.lockState == CursorLockMode.None && Cursor.visible) return false;
        return base.CanProcessInput();
    }
}
