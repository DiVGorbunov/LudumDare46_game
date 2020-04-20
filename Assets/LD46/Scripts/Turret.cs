using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public Transform WeaponSocket;
    public Transform PlayerSocket;
    public FruitBucket FruitBucket;
    private WeaponController controller;

    public float minXRotation = -40;
    public float maxXRotation = 30;

    private float lastXRotaion = 0.0f, lastYRotation = 0.0f;

    public void AddRotation(float xRot, float yRot)
    {
        lastXRotaion = Mathf.Clamp(lastXRotaion+xRot, minXRotation, maxXRotation);
        lastYRotation += yRot;
    }
    public void UpdateRotation()
    {
        gameObject.transform.localEulerAngles = new Vector3(lastXRotaion, lastYRotation, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        var weaponPrefab = Instantiate<GameObject>(WeaponPrefab, WeaponSocket);
        var cartLauncher = weaponPrefab.GetComponentInChildren<CartLauncherWeaponController>();
        cartLauncher.RegisterFruitBucket(FruitBucket);

        controller = weaponPrefab.GetComponent<WeaponController>();
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
        UpdateRotation();
    }
}
