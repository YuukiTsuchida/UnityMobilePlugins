using UnityEngine;
using System.Collections;

public class ClipbordTest : MonoBehaviour 
{
    public void OnPushButton(string text)
    {
        NativeSystem.UniClipbord.Copy(text);
    }
}
