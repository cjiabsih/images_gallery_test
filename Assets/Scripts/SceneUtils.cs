using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public static class SceneUtils
    {
        private const string GallerySceneName = "Gallery";
        private const string SingleImageSceneName = "SingleImage";

        public static void LoadGalleryScene()
        {
            SceneManager.LoadScene(GallerySceneName);
        }

        public static void LoadSingleImageScene()
        {
            SceneManager.LoadScene(SingleImageSceneName);
        }
    }
}