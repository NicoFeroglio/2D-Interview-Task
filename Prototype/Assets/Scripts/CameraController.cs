using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private uint speed;
    private Transform _target;
    private event Action OnUpdate = delegate {  };

    public bool CanFollow {
        set {
            if (value)
                OnUpdate += FollowTarget;
            else
                OnUpdate -= FollowTarget;
        }
    }
    

    public void SetTarget(Transform target)
    {
        _target = target;
        CanFollow = true;
    }

    private void FollowTarget()
    {
        Vector3 nextPos = Vector3.Lerp(transform.position, _target.position, speed * Time.deltaTime);
        nextPos.z = transform.position.z;
        transform.position = nextPos;
    }
    
    private void Update()
    {
        OnUpdate();
    }
}
