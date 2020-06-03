using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraZoom : MonoBehaviour
{
    // Start is called before the first frame update
    CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        CinemachineTransposer transposer = this.virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 currentOffset = transposer.m_FollowOffset;
        float newOffset = currentOffset.z + Input.mouseScrollDelta.y;
        print("New Offset " + newOffset);
        transposer.m_FollowOffset = new Vector3(currentOffset.x, currentOffset.y, Mathf.Clamp(currentOffset.z + Input.mouseScrollDelta.y, -50f, -5f));
    }
}
