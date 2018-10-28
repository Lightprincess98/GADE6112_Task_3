using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float panBorderThickness = 10f;
    [SerializeField]
    private Vector2 panLimit;
    [SerializeField]
    private float scrollSpeed = 20f;
    [SerializeField]
    private Vector2 panLimitExtend;
    [SerializeField]
    private float minSize = 2.7527f;
    [SerializeField]
    private float maxSize = 9.21092f;

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize += scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x + panLimitExtend.x, panLimit.x);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y + panLimitExtend.y, panLimit.y);

        transform.position = pos;
	}
}
