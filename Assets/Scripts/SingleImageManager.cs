using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SingleImageManager : MonoBehaviour
    {
        private Image _singleImage;
        public Sprite testSprite;

        private void Awake()
        {
            _singleImage = GetComponent<Image>();
        }

        private void Start()
        {
            // _singleImage.sprite = GameSessionData.CashedSprites[GameSessionData.SingleImageId];
            _singleImage.sprite = testSprite;
            _singleImage.DOFade(1f, 0.1f);
        }

        public void OnBackButtonClick()
        {
            SceneUtils.LoadGalleryScene();
        }
    }
}