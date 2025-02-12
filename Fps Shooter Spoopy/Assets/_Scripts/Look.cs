using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float mouseSen = 100f;
    public Transform player;
    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Låser vår mus till skärmen, och så den inte syns. 
    }
    void Update()
    {
        //Hämtar vår axis från input manager under project settings
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Vi använder inte rotate för att vi ska kunna stoppa rotationen från att gå för långt (max 90 grader)
        player.Rotate(Vector3.up * mouseX);
    }
}
