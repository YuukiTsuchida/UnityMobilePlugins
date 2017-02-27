using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClipbordTest : MonoBehaviour 
{
    public void OnPushCopyButton(string text)
    {
        NativeSystem.UniClipbord.Copy(text);
    }

    public void OnPushPasteButton(Text text)
    {
        text.text = NativeSystem.UniClipbord.Paste();
    }
}
