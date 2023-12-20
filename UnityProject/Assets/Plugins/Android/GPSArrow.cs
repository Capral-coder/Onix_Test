using UnityEngine;
using UnityEngine.UI;

public class GPSArrow : MonoBehaviour
{

    public InputField inputFieldOne;
    public InputField inputFieldTow;

    public double targetLatitude = 48.516157;
    public double targetLongitude = 32.258206;

    public float updateInterval = 1.0f;

    private double currentLatitude = 0.0;
    private double currentLongitude = 0.0;

    private float lastUpdateTime = 0.0f;

    void Start()
    {
        inputFieldOne = GameObject.Find("InputFieldOne").GetComponent<InputField>();
        inputFieldTow = GameObject.Find("InputFieldTow").GetComponent<InputField>();
        Input.location.Start(); 
    }


    void Update()
    {
        double targetLatitude;
        double targetLongitude;

        if (double.TryParse(inputFieldOne.text.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out targetLatitude)
            && double.TryParse(inputFieldTow.text.Replace(",", "."), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out targetLongitude))
        {
            float currentTime = Time.time;

            if (currentTime - lastUpdateTime >= updateInterval)
            {
                currentLatitude = Input.location.lastData.latitude;
                currentLongitude = Input.location.lastData.longitude;

                lastUpdateTime = currentTime;
            }

            Vector3 targetDirection = new Vector3(
                (float)(targetLongitude - currentLongitude),
                0f,
                (float)(targetLatitude - currentLatitude)
            ).normalized;

            float phoneRotation = Input.compass.trueHeading;

            float angle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up) - phoneRotation;

            transform.rotation = Quaternion.Euler(0, -angle, 0);

        }
        else
        {
            Debug.LogError("Невозможно преобразовать введенный текст в double.");
        }
    }
}
