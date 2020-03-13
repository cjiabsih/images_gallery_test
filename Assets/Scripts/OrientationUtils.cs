using UnityEngine;

namespace DefaultNamespace
{
    public static class OrientationUtils
    {
        public static void SetGalleryOrientation()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public static void SetSingleImageOrientation()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;

            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}