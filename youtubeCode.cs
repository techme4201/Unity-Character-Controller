using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnightController : MonoBehaviour
{
    float speed=4;
    float rotSpeed=80;
    float rot = 0f;
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            if(Input.GetKey(KeyCode.W))
            {
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed; 
            }
            if(Input.GetKeyUp(KeyCode.W)){
                moveDir = new Vector3(0,0,0);
                moveDir *= speed;
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);
            moveDir = new Vector3(0, 0, 1);
            moveDir *= speed;
            moveDir = transform.InverseTransformDirection(moveDir);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("running,false");
            anim.SetInteger("condition", 0);
            moveDir = new Vector3(0, 0, 0);
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
         
    }

    void GetInput()
    {
        if(controller.isGrounded)
            if(Input.GetMouseButtonDown(0))
            {
                Attacking();
            }

    }

    void Attacking()
    {
        anim.SetInteger("condition", 2);
        StartCoroutine(AttackRouting);
        anim.SetInteger("condition", 0);
    }

    IEnumerator AttackRouting()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);

    }

}
}


