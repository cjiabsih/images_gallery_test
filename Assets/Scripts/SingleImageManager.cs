using System.Collections;
using System.Collections.Generic;
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
        private Texture2D _currentTexture;

        private void Awake()
        {
            _singleImage = GetComponent<RawImage>();
            _mainCamera = Camera.main;
           
            _currentTexture = GetTexture();
            GetComponent<RectTransform>().sizeDelta = new Vector2(_textureWidth, _textureHeight);
            
            ChangeCanvasOrientation(Input.deviceOrientation);
            orientationManager.onOrientationChanged.AddListener(ChangeCanvasOrientation);
        }

        private void Start()
        {
            SetTexture(_currentTexture);
        }

        private Texture2D GetTexture()
        {
            Texture2D texture = GameSessionData.SingleImageId < 0
                ? testTexture
                : GameSessionData.CashedTextures[GameSessionData.SingleImageId];
            _textureWidth = texture.width;
            _textureHeight = texture.height;

            return texture;
        }

        private void SetTexture(Texture2D texture)
        {
            _singleImage.texture = texture;

            _singleImage.DOFade(1f, 0.1f);
        }

        private void ChangeCanvasOrientation(DeviceOrientation orientation)
        {
            if (orientation == DeviceOrientation.Unknown ||
                orientation == DeviceOrientation.Portrait ||
                orientation == DeviceOrientation.PortraitUpsideDown)
            {
                singleImageCanvasScaler.referenceResolution =
                    new Vector2(_textureWidth, _textureWidth / _mainCamera.aspect);
                singleImageCanvasScaler.matchWidthOrHeight = 0f;

                Screen.orientation = ScreenOrientation.Portrait;
            }
            else if (orientation == DeviceOrientation.LandscapeLeft ||
                     orientation == DeviceOrientation.LandscapeRight)
            {
                singleImageCanvasScaler.referenceResolution =
                    new Vector2(_textureHeight / _mainCamera.aspect, _textureHeight);
                singleImageCanvasScaler.matchWidthOrHeight = 1f;

                Screen.orientation = (ScreenOrientation) orientation;
            }
        }

        public void OnBackButtonClick()
        {
            orientationManager.onOrientationChanged.RemoveListener(ChangeCanvasOrientation);

            Screen.orientation = OrientationUtils.GalleryScreenOrientation;
            StartCoroutine(LoadSceneWhenOrientationChanged(ScreenOrientation.Portrait));
        }
        
        private IEnumerator LoadSceneWhenOrientationChanged(ScreenOrientation orientation)
        {
            bool isReady = Screen.orientation == orientation;
            while (!isReady)
            {
                isReady = Screen.orientation == orientation;
                yield return null;
            }

            yield return null;
            SceneUtils.LoadGalleryScene();
        }
    }
}