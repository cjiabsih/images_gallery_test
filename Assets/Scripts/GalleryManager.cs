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
        for (int i = 0; i < Constants.ImagesCount; i++)
        {
            var item = Instantiate(itemPrefab, _scrollRect.content, false);
            item.SetItem(i);
        }
    }


    private void SetScrollViewPosition()
    {
        _scrollRect.content.anchoredPosition = GameSessionData.ScrollViewAnchoredPosition;
    }
}