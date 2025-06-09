using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        transform.LookAt(mainCamera.transform);
        transform.rotation= Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}
