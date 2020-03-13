using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Item itemPrefab;
    private ScrollRect _scrollRect;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        if (Constants.ShouldCasheSprites)
        {
            GameSessionData.CashedSprites = new Sprite[Constants.ImagesCount];
        }
    }

    private void Start()
    {
        InstantiateItems();
        SetScrollViewPosition();
    }

    private void InstantiateItems()
    {
        int visibleItemsCount = GetVisibleItemsCount();
        for (int i = 0; i < Constants.ImagesCount; i++)
        {
            var item = Instantiate(itemPrefab, _scrollRect.content, false);
            item.SetItemId(i);

            if (i < visibleItemsCount)
            {
                item.LoadImage();
            }
        }
    }

    private int GetVisibleItemsCount()
    {
        var gridLayout = _scrollRect.content.GetComponent<GridLayoutGroup>();
        float itemsCount = (_scrollRect.viewport.rect.height - gridLayout.padding.top) /
                           (gridLayout.cellSize.y + gridLayout.spacing.y);
        return Mathf.CeilToInt(itemsCount);
    }

    private void SetScrollViewPosition()
    {
        _scrollRect.content.anchoredPosition = GameSessionData.ScrollViewAnchoredPosition;
    }
}