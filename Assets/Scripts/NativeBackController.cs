using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class NativeBackController : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_IOS
            GetComponent<NativeBackController>().enabled = false;
#endif
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<SingleImageManager>().OnBackButtonClick();
                // SceneUtils.LoadGalleryScene();
            }
        }
    }
}