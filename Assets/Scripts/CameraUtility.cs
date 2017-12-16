using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtility : MonoBehaviour 
{
    public event EventHandler CameraMoved;
    public event EventHandler CameraStopped;

    public void MoveCamera(Vector3 pos)
    {
        var handler = CameraMoved;
        if(handler != null)
        {
            handler(this, null);
        }
        StartCoroutine(MoveCameraRoutine(pos));
    }

    private IEnumerator MoveCameraRoutine(Vector3 pos)
    {
        var currentPos = transform.position;
        pos.z = currentPos.z;
        var direction = (pos - currentPos).normalized;
        var distance = Vector3.Distance(currentPos, pos);
        while(distance > 0.5f)
        {
            yield return null;
            transform.Translate(direction * 40f * Time.deltaTime);
            distance = Vector3.Distance(transform.position, pos);
        }
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        var handler = CameraStopped;
        if(handler != null)
        {
            handler(this, null);
        }
    }
}
