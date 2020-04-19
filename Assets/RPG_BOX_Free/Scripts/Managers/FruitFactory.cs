using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FruitFactory : InventoryManager
{

    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.Space) &&
            Inventory.Count < Rows * Columns
        )
        {
            Debug.Log("Item added");
            int itemsCount = DatabaseManager.Items.Count;
            this.AddItemToInventory(DatabaseManager.Items[Random.Range(0, itemsCount)]);
        }
    }

}