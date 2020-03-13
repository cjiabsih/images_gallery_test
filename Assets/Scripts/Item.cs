using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public Image loadedImage;

    private int _itemId;
    private bool _isImageLoaded;
    private MaskableGraphic _maskableGraphic;

    private void Awake()
    {
        _maskableGraphic = GetComponent<MaskableGraphic>();
    }

    private void OnCullStateChange(bool cullState)
    {
        Debug.Log($"Cull state for {_itemId} changed to {cullState}");
        if (Constants.ShouldCasheSprites && GameSessionData.CashedSprites != null
                                         && GameSessionData.CashedSprites[_itemId] != null)
        {
            loadedImage.sprite = GameSessionData.CashedSprites[_itemId];
            loadingText.enabled = false;
        }
        else
        {
            if (!cullState && !_isImageLoaded)
            {
                StartCoroutine(LoadImageCo());
            }
        }
    }

    public void SetItem(int itemId)
    {
        _itemId = itemId;
        _maskableGraphic.onCullStateChanged.AddListener(OnCullStateChange);
    }

    private IEnumerator LoadImageCo()
    {
        // UnityWebRequest request = UnityWebRequestTexture.GetTexture(string.Format(ImagesData.DownloadUrl, (_itemId + 1).ToString()));
        // yield return request.SendWebRequest();
        // if (request.isNetworkError || request.isHttpError)
        // {
        //     Debug.Log(request.error);
        // }
        // else
        // {
        //     Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        //     var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        //     loadedImage.sprite = sprite;
        //
        //     loadingText.enabled = false;
        // }
        yield return null;
        Debug.Log($"Load {_itemId}");
        // if (Constants.ShouldCasheSprites)
        // {
        //     GameSessionData.CashedSprites[_itemId] = sprite;
        // }
        _isImageLoaded = true;
        _maskableGraphic.onCullStateChanged.RemoveListener(OnCullStateChange);
    }

    private void OnDisable()
    {
        _maskableGraphic.onCullStateChanged.RemoveAllListeners();
    }
}