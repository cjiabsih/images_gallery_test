using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public static class SceneUtils
    {
        private const string GallerySceneName = "Gallery";
        private const string SingleImageSceneName = "SingleImage";

        public static void LoadGalleryScene()
        {
            OrientationUtils.SetGalleryOrientation();
            SceneManager.LoadScene(GallerySceneName);
        }

        public static void LoadSingleImageScene()
        {
            OrientationUtils.SetSingleImageOrientation();
            SceneManager.LoadScene(SingleImageSceneName);
        }
    }
}