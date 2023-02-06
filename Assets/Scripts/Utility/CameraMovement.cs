using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [Header("Camera Bounds")]
    [Tooltip("These are the boundaries of where the camera can move in worldspace")]
    public float MaxXBoundary = 5f, MinXBoundary = -5f, MaxYBoundary = 5f, MinYBoundary = -5f;
    [Tooltip("Check this if you want the camera to have boundaries")]
    public bool clampCamera = true;

    [Space]
    [Header("Object to Follow")]
    [Tooltip("This is the character in which the camera will be following")]
    public Transform PlayerCharacter;

    [Space]
    [Header("Speed")]
    [Tooltip("This is the speed at which the camera follows the player, the smaller the value, the faster the movement")]
    public float smoothSpeed = 0.125f; //The bigger the value, the faster the smooth


    private Vector3 Velocity = Vector3.zero;
    private Camera camera;

    void Start()
    {
        if (PlayerCharacter == null)
        {
            PlayerCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        }

        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 point = camera.WorldToViewportPoint(PlayerCharacter.position);//Grab where the player is
        Vector3 delta = PlayerCharacter.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //Vector between the two
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref Velocity, smoothSpeed);

        Transform camerATransform = camera.gameObject.transform;

        if (clampCamera)
        {
            if (camerATransform.position.x > MaxXBoundary || camerATransform.position.x < MinXBoundary || camerATransform.position.y > MaxYBoundary || camerATransform.position.y < MinYBoundary)
            {
                float x = Mathf.Clamp(PlayerCharacter.position.x, MinXBoundary, MaxXBoundary);
                float y = Mathf.Clamp(PlayerCharacter.position.y, MinYBoundary, MaxYBoundary);
                transform.position = new Vector3(x, y, gameObject.transform.position.z);
            }
        }
    }
}
