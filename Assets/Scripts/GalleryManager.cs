using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public Item itemPrefab;
    private ScrollRect _scrollRect;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        
        _scrollRect = GetComponent<ScrollRect>();
        if (GameSessionData.CashedTextures == null)
        {
            GameSessionData.CashedTextures = new Texture2D[Constants.ImagesCount];
        }
    }

    private void Start()
    {
        SetScrollViewPosition();
        InstantiateItems();
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

        float availableHeight = _scrollRect.viewport.rect.height - gridLayout.padding.top +
                                Mathf.Max(_scrollRect.content.anchoredPosition.y, 0);
        float itemHeight = gridLayout.cellSize.y + gridLayout.spacing.y;
        int itemsInColumn = Mathf.CeilToInt(availableHeight / itemHeight);

        return itemsInColumn * gridLayout.constraintCount;
    }

    private void SetScrollViewPosition()
    {
        _scrollRect.content.anchoredPosition = GameSessionData.ScrollViewAnchoredPosition;
    }

    private void OnImageButtonClick(int imageId)
    {
        GameSessionData.SingleImageId = imageId;
        GameSessionData.ScrollViewAnchoredPosition = _scrollRect.content.anchoredPosition;

        SceneUtils.LoadSingleImageScene();
    }
}