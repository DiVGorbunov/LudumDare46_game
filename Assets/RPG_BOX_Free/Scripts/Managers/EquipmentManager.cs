using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour 
{
    Transform EqSlotsParent;//the place when slots is gathered

    Dictionary<GearMainType, CustomItemAndGo> EquipmentInventory = new Dictionary<GearMainType, CustomItemAndGo>();
    Dictionary<GearMainType, Transform> EquipmentSlots = new Dictionary<GearMainType, Transform>();

    GameObject ItemGoPrefab;

    InventoryManager AccInv;


    void Start () 
	{
        if (ItemGoPrefab == null) //get the prefab
        {
            ItemGoPrefab = Resources.Load<GameObject>("Prefabs/ItemInventoryGO");
        }

        if (EqSlotsParent == null)//get the transform
        {
            EqSlotsParent = transform.Find("EquipmentSlots");
        }

        for (int i = 0; i < EqSlotsParent.childCount; i++)//add all the slots in EqSlotsParent
        {
            GearMainType gearType = EqSlotsParent.GetChild(i).GetComponent<EqSlotProps>().GearType;
            if (EquipmentSlots.ContainsKey(gearType) == false)
            {
                EquipmentSlots.Add(gearType, EqSlotsParent.GetChild(i));
            }
        }

        if (GameObject.Find("InventoryWindow") != null)
        {
            AccInv = GameObject.Find("InventoryWindow").GetComponent<InventoryManager>();
        }




    }

    public void RemoveItemFromEquipmentInventory(GearMainType WhichType)
    {
        //codes for removing item from the equipment inventory and sending it back to the main inventory
        ItemProps AccIP = EquipmentInventory[WhichType].TheGameObject.GetComponent<ItemProps>();
        AccIP.DestroyItem();
        EquipmentInventory.Remove(WhichType);
    }



}
