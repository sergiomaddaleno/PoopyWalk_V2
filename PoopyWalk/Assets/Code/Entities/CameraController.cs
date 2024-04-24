using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TimeScript timer;
    public Transform player;
    public Transform camera;
    public Camera mc;
    // public Transform transform;
    // Update is called once per frame

    public float lerpSpeed = 0.0001f; // Adjust the speed of the lerp

    void Update()
    {
        if (timer.levelOneCountdown <= 1)
        {
            mc.orthographicSize = 5;
            Vector3 targetpos = new Vector3(player.position.x, 1, -10.0f);

            camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, lerpSpeed);
        }
        else
        {
            camera.transform.position = new Vector3(player.position.x,camera.position.y, -10.0f);

        }
    }
}
