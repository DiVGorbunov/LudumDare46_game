using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public Transform WeaponSocket;
    public Transform PlayerSocket;
    private WeaponController controller;

    public float minXRotation = -40;
    public float maxXRotation = 30;

    private float lastXRotaion = 0.0f, lastYRotation = 0.0f;

    public void addRotation(float xRot, float yRot)
    {
        lastXRotaion = Mathf.Clamp(lastXRotaion+xRot, minXRotation, maxXRotation);
        lastYRotation += yRot;
    }
    public void updateRotation()
    {
        gameObject.transform.localEulerAngles = new Vector3(lastXRotaion, lastYRotation, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = Instantiate<GameObject>(WeaponPrefab, WeaponSocket).GetComponent<WeaponController>();
        controller.owner = this.gameObject;
    }

    public WeaponController GetWeapon()
    {
        return controller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        updateRotation();
    }
}
