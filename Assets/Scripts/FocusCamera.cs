using UnityEngine;
using System.Collections;

public class FocusCamera : MonoBehaviour
{

    [SerializeField] float speed = 3;
    public Transform focus;        //Public variable to store a reference to the player game object


    private Vector3 originalOffset;            //Private variable to store the offset distance between the player and camera
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    public void AddToOffset(Vector3 offsetToAdd)
    {
        offset += offsetToAdd;
    }

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        originalOffset = transform.position - focus.position;
        offset = originalOffset;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 targetPos = new Vector3(focus.position.x, focus.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed);
        // if (offset != originalOffset)
        // {
            // offset = Vector3.MoveTowards(offset, originalOffset, Time.deltaTime * speed);
        // }
    }
}
