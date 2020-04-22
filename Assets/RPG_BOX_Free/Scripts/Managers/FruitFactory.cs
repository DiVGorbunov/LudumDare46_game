using UnityEngine;

public class FruitFactory : InventoryManager
{

    override protected void SetupStartSet()
    {
        int countToAdd = (Rows * Columns) - Inventory.Count;
        for (int i = 0; i < countToAdd; i++)
        {
            var fruit = FruitManager.Instance.GetRandomRemainingFruitItem();
            AddItemToInventory(FruitManager.Instance.GetFruitItem(fruit));
        }
    }
    private void Update()
    {
        if (
            Input.GetKeyDown(KeyCode.Space) &&
            Inventory.Count < Rows * Columns
        )
        {
            int countToAdd = (Rows * Columns) - Inventory.Count;
            for (int i = 0; i < countToAdd; i++)
            {
                var fruit = FruitManager.Instance.GetRandomRemainingFruitItem();
                AddItemToInventory(FruitManager.Instance.GetFruitItem(fruit));
            }
            
        }
    }
}