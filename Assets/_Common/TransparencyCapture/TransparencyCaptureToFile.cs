using System.Collections;
using UnityEngine;

public class TransparencyCaptureToFile:MonoBehaviour
{
	int captureCount = 1;
	public KeyCode captureHotkey = KeyCode.C;

    public IEnumerator capture()
    {

        yield return new WaitForEndOfFrame();
        //After Unity4,you have to do this function after WaitForEndOfFrame in Coroutine
        //Or you will get the error:"ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame"
		captureCount++;
		zzTransparencyCapture.captureScreenshot("Screenshot" + captureCount.ToString() + ".png");
    }

    public void Update()
    {
		if (Input.GetKeyDown(captureHotkey))
            StartCoroutine(capture());
    }
}