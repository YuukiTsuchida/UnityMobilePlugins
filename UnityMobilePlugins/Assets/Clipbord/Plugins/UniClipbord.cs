using UnityEngine;

namespace NativeSystem
{
    public static class UniClipbord 
    {
        public static void Copy(string text)
        {
    #if !UNITY_EDITOR && UNITY_IOS
    #elif !UNITY_EDITOR && UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                        {
                            AndroidJavaObject clipboardManager = activity.Call<AndroidJavaObject>("getSystemService","clipboard");
                            AndroidJavaClass clipDataClass = new AndroidJavaClass("android.content.ClipData");
                            AndroidJavaObject clipData = clipDataClass.CallStatic<AndroidJavaObject>("newPlainText","simple text", text);
                            clipboardManager.Call("setPrimaryClip", clipData);
                        }));
    #else
            GUIUtility.systemCopyBuffer = text;
    #endif
        }
    }
}
