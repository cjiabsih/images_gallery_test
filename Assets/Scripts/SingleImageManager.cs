using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(RawImage), typeof(RectTransform))]
    public class SingleImageManager : MonoBehaviour
    {
        public CanvasScaler singleImageCanvasScaler;
        public DeviceOrientationManager orientationManager;
        [Space] public Texture2D testTexture;

        private RawImage _singleImage;
        private int _textureWidth, _textureHeight;
        private Camera _mainCamera;

        private void Awake()
        {
            _singleImage = GetComponent<RawImage>();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            SetTexture();
            GetComponent<RectTransform>().sizeDelta = new Vector2(_textureWidth, _textureHeight);

            ChangeCanvasScaler(DeviceOrientation.Portrait);
        }

        private void OnEnable()
        {
            orientationManager.onOrientationChanged.AddListener(ChangeCanvasScaler);
        }

        private void OnDisable()
        {
            orientationManager.onOrientationChanged.RemoveListener(ChangeCanvasScaler);
        }

        private void SetTexture()
        {
            SetTexture(GameSessionData.SingleImageId < 0
                ? testTexture
                : GameSessionData.CashedTextures[GameSessionData.SingleImageId]);
        }

        private void SetTexture(Texture2D texture)
        {
            _textureWidth = texture.width;
            _textureHeight = texture.height;

            _singleImage.texture = texture;
        }

        private void ChangeCanvasScaler(DeviceOrientation orientation)
        {
            Debug.Log("Change canvas " + orientation);
            if (orientation == DeviceOrientation.Unknown ||
                orientation == DeviceOrientation.Portrait ||
                orientation == DeviceOrientation.PortraitUpsideDown)
            {
                Debug.Log($"ref resolution " + new Vector2(_textureWidth, _textureWidth / _mainCamera.aspect));
                singleImageCanvasScaler.referenceResolution =
                    new Vector2(_textureWidth, _textureWidth / _mainCamera.aspect);
                singleImageCanvasScaler.matchWidthOrHeight = 0f;
            }
            else if (orientation == DeviceOrientation.LandscapeLeft ||
                     orientation == DeviceOrientation.LandscapeRight)
            {
                Debug.Log($"ref resolution " + new Vector2(_textureHeight * _mainCamera.aspect, _textureHeight));
                singleImageCanvasScaler.referenceResolution =
                    new Vector2(_textureHeight * _mainCamera.aspect, _textureHeight);
                singleImageCanvasScaler.matchWidthOrHeight = 1f;
            }
        }

        public void OnBackButtonClick()
        {
            SceneUtils.LoadGalleryScene();
        }
    }
}