using UnityEngine;

public class FruitBucket : InventoryManager
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("dsfdsf");
            this.removeAllFruits();
        }
    }
    public override bool AddItemToInventory(FruitItem ItemToAdd)//bool to check can this item be added to the inventory or no
    {
        int TheIndexOfMe = TakeIndexOfPos();
        if (TheIndexOfMe > PositionsAndOccupation.Count)//in case the index is more than the slots numbers
        {
            ErrorMessageText.instance.ShowMessage("No Enough space in inventory");
            return false;
        }
        GameObject NewItem = GameObject.Instantiate(ItemGoPrefab, ItemsParent);
        NewItem.transform.position = PositionsAndOccupation[TheIndexOfMe].aPos;
        ItemProps AccIP = NewItem.GetComponent<ItemProps>();
        AccIP.TakeInfo(ItemToAdd, TheIndexOfMe, ItemHome.FruitBucket);

        CustomItemAndGo ItemAndGo = new CustomItemAndGo(ItemToAdd, NewItem);
        if (Inventory.ContainsKey(TheIndexOfMe))
        {
            Inventory[TheIndexOfMe] = ItemAndGo;
        }
        else
        {
            Inventory.Add(TheIndexOfMe, ItemAndGo);
        }
        return true;

    }

    public void removeAllFruits()
    {
        int inventorySize = Rows * Columns;
        for (int i = 0; i < inventorySize; i++)
        {
            RemoveItemFromInventory(i);
        }
    }
}