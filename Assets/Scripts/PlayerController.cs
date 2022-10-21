using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    Vector3 moveDir;
    Animator animator;
    CharacterController cc;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            moveDir.y = jumpForce;
        }

        moveDir.y -= gravity * Time.deltaTime;

        var moving = moveDir.x != 0 || moveDir.z != 0;

        if (moving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0f, moveDir.z)), 0.15f);
        }

        animator.SetBool("isWalking", moving);

        //print("moveDir : " + moveDir);
        cc.Move(moveDir * Time.deltaTime);
        //print("Time : " + Time.deltaTime);
    }
}
