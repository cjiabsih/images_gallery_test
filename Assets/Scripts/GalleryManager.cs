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
        GameSessionData.CashedSprites = new Sprite[Constants.ImagesCount];
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
            item.SetItemId(i, OnImageButtonClick);

            if (i < visibleItemsCount)
            {
                item.LoadImage();
            }
        }
    }

    private int GetVisibleItemsCount()
    {
        var gridLayout = _scrollRect.content.GetComponent<GridLayoutGroup>();
        int itemsInColumn = Mathf.CeilToInt((_scrollRect.viewport.rect.height - gridLayout.padding.top) /
                                              (gridLayout.cellSize.y + gridLayout.spacing.y));
        return itemsInColumn * gridLayout.constraintCount;
    }

    private void SetScrollViewPosition()
    {
        _scrollRect.content.anchoredPosition = GameSessionData.ScrollViewAnchoredPosition;
    }

    private void OnImageButtonClick(int imageId)
    {
        Debug.Log(imageId);
        GameSessionData.SingleImageId = imageId;
        SceneUtils.LoadSingleImageScene();
    }
}