using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cameraContainer;
    public float cameraSpeed, offsetX, offsetY;
    private Vector3 collisionPosition;
    private bool cameraMove = false;

    void Start()
    {
        cameraContainer = GameObject.Find("CameraContainer");
        mainCamera = cameraContainer.GetComponentInChildren<Camera>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            offsetX = collision.GetComponent<BoxCollider2D>().offset.x;
            offsetY = collision.GetComponent<BoxCollider2D>().offset.y;
            collisionPosition = new Vector3(collision.transform.position.x + offsetX, collision.transform.position.y + offsetY, -10);
            cameraMove = true;
        }

    }

    void FixedUpdate()
    {
        if (cameraMove)
        {
            cameraContainer.transform.position = Vector3.Lerp(cameraContainer.transform.position, collisionPosition, Time.deltaTime * cameraSpeed);
            if (collisionPosition == cameraContainer.transform.position) cameraMove = false;
        }
    }

    public void CameraShake(float seconds)
    {
        if(seconds <= 0) mainCamera.GetComponent<Animator>().SetTrigger("shake");
        else
        {
            mainCamera.GetComponent<Animator>().SetBool("shake_b", true);
            Invoke("CameraIdle", seconds);
        }
    }
    private void CameraIdle()
    {
        mainCamera.GetComponent<Animator>().SetBool("shake_b", false);
    }


}
