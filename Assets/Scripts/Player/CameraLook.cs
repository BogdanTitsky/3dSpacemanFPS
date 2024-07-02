using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float XMove;
    private float YMove;
    private float XRotation;
    private float YRotation;
    public Vector2 LockAxis;
    public float Sensivity = 40f;
    [SerializeField] private Transform follow;
    [SerializeField] private Transform player;
    

    
    void Update()
    {
        transform.position = follow.position;
        XMove = LockAxis.x * Sensivity * Time.deltaTime;
        YMove = LockAxis.y * Sensivity * Time.deltaTime;
        XRotation -= YMove;
        YRotation += XMove;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, YRotation,0);
        player.rotation =Quaternion.Euler(transform.localRotation.x, player.rotation.y, transform.localRotation.z);
    }
}
