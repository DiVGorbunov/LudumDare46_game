using UnityEngine;

public class FruitFactory : InventoryManager
{
    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.Space) &&
            Inventory.Count < Rows * Columns
        )
        {
            var fruit = FruitManager.Instance.GetRandomFruit();
            AddItemToInventory(FruitManager.Instance.GetFruitItem(fruit));
        }
    }
}