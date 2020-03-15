using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public RawImage loadedImage;

    private Button _button;

    private int _itemId;
    private bool _isImageLoaded;
    private MaskableGraphic _maskableGraphic;

    private void Awake()
    {
        _maskableGraphic = GetComponent<MaskableGraphic>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (!_isImageLoaded)
        {
            _maskableGraphic.onCullStateChanged.AddListener(OnCullStateChange);
        }
    }

    private void OnDisable()
    {
        if (!_isImageLoaded)
        {
            _maskableGraphic.onCullStateChanged.RemoveListener(OnCullStateChange);
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnCullStateChange(bool cullState)
    {
        if (!cullState)
        {
            LoadImage();
        }
    }

    public void SetItemId(int itemId, Action<int> onButtonClick)
    {
        _itemId = itemId;

        _button.onClick.AddListener(() =>
        {
            if (_isImageLoaded)
            {
                onButtonClick(_itemId);
            }
            else
            {
                DOTween.Kill(loadingText);
                loadingText.transform.DOScale(Vector3.one * 1.25f, 0.25f)
                    .OnComplete(() => { loadingText.transform.DOScale(Vector3.one, 0.25f); });
            }
        });
    }

    public void LoadImage()
    {
        if (_isImageLoaded) return;

        if (GameSessionData.CashedTextures != null
            && GameSessionData.CashedTextures[_itemId] != null)
        {
            loadedImage.texture = GameSessionData.CashedTextures[_itemId];
            loadingText.enabled = false;
            _isImageLoaded = true;
        }
        else
        {
            StartCoroutine(LoadImageCo());
        }
    }

    private IEnumerator LoadImageCo()
    {
        UnityWebRequest request =
            UnityWebRequestTexture.GetTexture(string.Format(Constants.DownloadUrl, (_itemId + 1).ToString()));
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            GameSessionData.CashedTextures[_itemId] = DownloadHandlerTexture.GetContent(request);
            request.disposeDownloadHandlerOnDispose = false;

            loadedImage.texture = GameSessionData.CashedTextures[_itemId];
            loadingText.enabled = false;
        }

        _isImageLoaded = true;
        _maskableGraphic.onCullStateChanged.RemoveListener(OnCullStateChange);
        request.Dispose();
    }
}