using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    //public VariableJoystick variableJoystick;
    public DynamicJoystick js;
    public Rigidbody rb;
    [SerializeField] Animator animator;
   




    public void FixedUpdate()
    {
        Vector3 addedPos = new Vector3(js.Horizontal * speed * Time.deltaTime, 0, js.Vertical * speed * Time.deltaTime);
        transform.position += addedPos;
        direction = Vector3.forward * js.Vertical + Vector3.right * js.Horizontal;
        

        
        Rotate();
    }

    Vector3 direction;

    public void Rotate()
    {
        if (js.isTouched)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }

    }
    public void RotateSnap()
    {
        animator.SetBool("isTurning", false);
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public void StartMove()
    {
        animator.SetBool("isTurning", true);
    }
}