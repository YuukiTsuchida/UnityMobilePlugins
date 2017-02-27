using UnityEngine;
using System.Runtime.InteropServices;

namespace NativeSystem
{
    public static class UniClipbord 
    {
#if !UNITY_EDITOR && UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _Copy(string text);
#endif
        public static void Copy(string text)
        {
#if !UNITY_EDITOR && UNITY_IOS
            _Copy(text);
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

        // クリップボードから文字列を取り出す
        public static string Paste()
        {
#if !UNITY_EDITOR && UNITY_IOS
#elif !UNITY_EDITOR && UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject clipboardManager = activity.Call<AndroidJavaObject>("getSystemService","clipboard");
            AndroidJavaObject item = clipboardManager.Call<AndroidJavaObject>("getPrimaryClip").Call<AndroidJavaObject>("getItemAt", 0);
            return item.Call<AndroidJavaObject>("getText").Call<string>("toString");
#else
            return GUIUtility.systemCopyBuffer;
#endif
        }
    }
}
