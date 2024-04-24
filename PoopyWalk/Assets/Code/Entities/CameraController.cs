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

    public float zoomSpeed = 5f;

    void Update()
    {
        if (timer.levelOneCountdown <= 1)
        {

            float targetSize = 5f;
            mc.orthographicSize = Mathf.Lerp(mc.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);

            camera.transform.position = new Vector3(player.position.x, 1.0f, -10.0f);
        }
        else
        {

            float targetSize = 10f;
            mc.orthographicSize = Mathf.Lerp(mc.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);

            camera.transform.position = new Vector3(player.position.x, camera.transform.position.y, -10.0f);
        }
    }
}
