using UnityEngine;

public class CanvasFruitController : MonoBehaviour
{
    public GameObject canvasFruit;

    private const float _borderOffset = 0.05f;
    private const float _borderBottomOffset = 0.2f;
    private const float _fruitOffset = 0.02f;

    public void Generate(int number)
    {
        var rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.rect.width,
            height = rectTransform.rect.height,
            fruitWidth = (width - _borderOffset * 2f - _fruitOffset * (number - 1)) / number,
            fruitHeight = height - _borderOffset - _borderBottomOffset,
            fruitDimension = Mathf.Min(fruitWidth, fruitHeight),
            fruitXOffset = (width - number * fruitDimension - _fruitOffset * (number - 1)) / 2f,
            fruitYOffset = _borderBottomOffset + (height - _borderBottomOffset - _borderOffset - fruitDimension) / 2f;
 
        for (int i = 0; i < number; i++)
        {
            var newFruit = Instantiate(canvasFruit, gameObject.transform);
            var newFruitRectTransform = newFruit.GetComponent<RectTransform>();

            newFruitRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, fruitYOffset, fruitDimension);
            newFruitRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, fruitXOffset + i * (fruitDimension + _fruitOffset), fruitDimension);
        }
    }
}
