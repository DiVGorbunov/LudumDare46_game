using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    private ItemProps item;
    private Vector2 startPosition;
    private FruitFactory fruitFactory;

    private void Start()
    {
        fruitFactory = GameObject.FindObjectOfType<FruitFactory>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        item = GetComponent<ItemProps>();
        startPosition = item.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        item.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isMouseOnBucket(eventData))
        {
            if (item.AddToBucket())
            {
                fruitFactory.RemoveItemFromInventory(item.MyPlaceInHome);
                fruitFactory.AddItemToInventory(FruitManager.Instance.GetFruitItem(FruitManager.Instance.GetRandomRemainingFruitItem()));
            }
            else
            {
                item.transform.position = startPosition;
            }
        }
        else
        {
            item.transform.position = startPosition;
        }

    }

    private bool isMouseOnBucket(PointerEventData eventData)
    {
        RaycastHit hit;
        if (Physics.Raycast(eventData.position, transform.forward, out hit, 100.0f))
        {
            if (hit.transform.name.Equals("Bucket"))
            {
                return true;
            }
        }
        return false;
    }
}