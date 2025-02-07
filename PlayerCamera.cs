using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float speedCamera = 20f;
    [SerializeField] private float speedScrollCamera = 60;

    float mouse_x = 0, mouse_y = 0;
    Vector3 curR;
    private Vector3 smV = Vector3.zero; // до какой скорости нужно снижать скорость
    private float vel = 0.2f; // время задержки поворота камеры

    private void Update()
    {
        //
        float directionX = Input.GetAxis("Horizontal");
        float directionZ = Input.GetAxis("Vertical");
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X") * 5;
            float y = Input.GetAxis("Mouse Y") * 5;
            mouse_x += y; mouse_y += x;
            mouse_x = Mathf.Clamp(mouse_x, -90, 40);

            Vector3 new_rot = new(mouse_x, mouse_y, 0);
            curR = Vector3.SmoothDamp(curR, new_rot, ref smV, vel);
            transform.localEulerAngles = curR;
        }
        Vector3 nn;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            nn = speedCamera * Time.deltaTime * transform.TransformDirection(directionX, 0, directionZ) * 1.5f;
        }
        else
            nn = speedCamera * Time.deltaTime * transform.TransformDirection(directionX, 0, directionZ);

        transform.position += new Vector3(nn.x, 0, nn.z);

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0 && Camera.main.fieldOfView > 35)
            Camera.main.fieldOfView -= Time.deltaTime
                                  * speedScrollCamera;

        if (mw < 0 && Camera.main.fieldOfView < 60)
            Camera.main.fieldOfView += Time.deltaTime
                                  * speedScrollCamera;
    }
}
