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

    // Start is called before the first frame update
    void Start()
    {
        cart.transform.position = new Vector3(
            x: path.m_Waypoints[0].position.x,
            y: 0,
            z: path.m_Waypoints[0].position.z);

        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = cart.transform.rotation;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = cart.transform.rotation * Quaternion.Euler(0, 180, 0);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cart.m_Position += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cart.m_Position -= moveSpeed * Time.deltaTime;
        }
    }
}
