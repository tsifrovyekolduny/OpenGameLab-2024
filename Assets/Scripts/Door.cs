using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsEnter;
    public string From;
    public string To;
    public float OpeningSpeed = 100f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SetToHingeJointTarget(float targetPosition, bool fast = false)
    {
        HingeJoint hingeJoint = transform.GetComponentInChildren<HingeJoint>();
        JointSpring jointSpring = hingeJoint.spring;
        if (fast)
        {
            jointSpring.spring = 10000;
        }
        else
        {
            jointSpring.spring = OpeningSpeed;
        }

        jointSpring.targetPosition = targetPosition;
        hingeJoint.spring = jointSpring;
    }
}
