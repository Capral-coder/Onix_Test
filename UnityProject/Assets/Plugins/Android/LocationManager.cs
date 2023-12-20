
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public Text lastLatitudeText;
    public Text lastLongitudeText;

    private float lastLatitude = 0f;
    private float lastLongitude = 0f;

    private void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location services not enabled on device");
            return;
        }
        Input.location.Start();
        Input.location.Start(1f, 1f);
    }

    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            float distance = Vector2.Distance(new Vector2(lastLatitude, lastLongitude),
                                              new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude));

            lastLatitude = Input.location.lastData.latitude;
            lastLongitude = Input.location.lastData.longitude;
        }

        lastLatitudeText.text = lastLatitude.ToString();
        lastLongitudeText.text = lastLongitude.ToString();
    }

    private void OnDestroy()
    {
        Input.location.Stop();
    }
}