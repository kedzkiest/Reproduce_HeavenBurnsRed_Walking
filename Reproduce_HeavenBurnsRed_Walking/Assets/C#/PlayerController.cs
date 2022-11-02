using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private CinemachineDollyCart cart;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cart.transform.position = new Vector3(
            x: path.m_Waypoints[0].position.x,
            y: 0,
            z: path.m_Waypoints[0].position.z);

        transform.rotation = Quaternion.identity;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Rotate();
        Move();
    }

    [HideInInspector]
    public Quaternion rot;

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot = cart.transform.rotation;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot = cart.transform.rotation * Quaternion.Euler(0, 180, 0);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position = new Vector3(
            x: transform.position.x,
            y: 0,
            z: transform.position.z);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!path.m_Looped && Mathf.Abs(path.PathLength - cart.m_Position) < 0.1f)
            {
                anim.SetBool("isRunning", false);
                return;
            }

            cart.m_Position += moveSpeed * Time.deltaTime;
            anim.SetBool("isRunning", true);
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!path.m_Looped && Mathf.Abs(cart.m_Position) < 0.1f)
            {
                anim.SetBool("isRunning", false);
                return;
            }

            cart.m_Position -= moveSpeed * Time.deltaTime;
            anim.SetBool("isRunning", true);
            return;
        }

        anim.SetBool("isRunning", false);
    }
}
