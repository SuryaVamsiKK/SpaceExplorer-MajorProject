﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudoPlayer : MonoBehaviour
{
    public bool update;
    public Transform target;
    public Transform cameraa;
    public bool toFollowCam = true;
    public float camfollowSpeed = 10f;

    RaycastHit hit;
    public LayerMask mask;

    private void Start()
    {

    }

    public void Update()
    {
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,  -transform.parent.localEulerAngles.y, transform.localEulerAngles.z);

        transform.position = target.position;

        //if (transform.parent.parent != null)
        //{
        //    if (Physics.Raycast(transform.position, -(transform.position - transform.parent.parent.position).normalized, out hit, 2f, mask))
        //    {
        //        transform.up = hit.normal;
        //    }
        //}
        //else
        //{
        //    if (Physics.Raycast(transform.position, -(transform.position - transform.parent.position).normalized, out hit, 2f, mask))
        //    {
        //        transform.up = hit.normal;
        //    }
        //}
        if (transform.parent.parent != null)
        {
            transform.up = (transform.position - transform.parent.parent.position).normalized;
        }
        lookAt();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            transform.position = target.position;
            if (transform.parent.parent != null)
            {
                transform.up = (transform.position - transform.parent.parent.position).normalized;
            }
        }
    }

    public void lookAt()
    {
        Vector3 playerLocal = target.InverseTransformPoint(cameraa.position);
        playerLocal = new Vector3(playerLocal.x, 0, playerLocal.z);
        Vector3 updatedForward = ((target.forward + target.position) - target.TransformPoint(playerLocal)).normalized;
        Quaternion targetRot = Quaternion.LookRotation(updatedForward, target.up);

        if (toFollowCam)
        {
            targetRot = Quaternion.Inverse(target.parent.rotation) * targetRot;
            target.localRotation = Quaternion.Lerp(target.localRotation, targetRot, Time.deltaTime * camfollowSpeed);
            //target.forward = Vector3.Lerp(target.forward, updatedForward, Time.deltaTime * camfollowSpeed);
        }
    }    
}
