using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ScreenShot : MonoBehaviour
{
    [SerializeField]
    private string path;

    [SerializeField]
    [Range(1, 5)]
    private int size = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            string tempPath = path + "screenshot ";
            tempPath = tempPath + System.Guid.NewGuid().ToString() + ".png";
            ScreenCapture.CaptureScreenshot(tempPath, size);
            Debug.Log("Snapped Screenshot and saved at "+ tempPath);
        }
    }
}
