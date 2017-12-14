using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtility : MonoBehaviour 
{
    public void MoveCamera(Vector3 pos)
    {
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
    }
}
