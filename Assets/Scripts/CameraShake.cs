using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;


public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    public float defaultShakeDuration = 0.5f;
    float shakeDuration = 0;
    public float shakeMagnitude = 0.1f;
    public Vector3 resetPosition = new Vector3(0, 0, -10);

    [Button]
    public void TestShake()
    {
        StartShake();
    }

    private void Start()
    {
        cameraTransform = transform;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            // Generate random shake offset
            Vector3 shakeOffset = new Vector3(Random.Range(-1f, 1f), 0, 0) * shakeMagnitude + resetPosition;

            // Apply the shake offset to the camera position
            cameraTransform.localPosition = shakeOffset;

            // Reduce shake duration over time
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset camera position when shaking is done
            cameraTransform.localPosition = resetPosition;
        }
    }

    // Call this method to start the camera shake
    public void StartShake()
    {
        shakeDuration = defaultShakeDuration;
    }
}
