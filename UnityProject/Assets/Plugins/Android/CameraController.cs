using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    public RawImage rawImage;

    void Start()
    {
        webcamTexture = new WebCamTexture();
        rawImage.texture = webcamTexture;

        webcamTexture.Play();
    }
}