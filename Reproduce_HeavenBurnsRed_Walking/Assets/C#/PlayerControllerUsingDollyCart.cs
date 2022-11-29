using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class PlayerControllerUsingDollyCart : MonoBehaviour
{
    [SerializeField] private CinemachinePath path;
    [SerializeField] private CinemachineDollyCart cart;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float howCloseCanReachPathEnd;
    private Animator anim;

    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", false);
            return;
        }

        Rotate();
        Move();
    }

    [HideInInspector]
    public Quaternion rot;

    private void Rotate()
    {
        // active character rotation using keyinput
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rot = Quaternion.Euler(0, cart.transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot = Quaternion.Euler(0, cart.transform.rotation.eulerAngles.y, 0) * Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {

        // passive character moving along the path(cart)
        transform.position = cart.transform.position + new Vector3(xOffset, 0, zOffset);

        // active character moving using keyinput
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!path.m_Looped && Mathf.Abs(path.PathLength - cart.m_Position) < howCloseCanReachPathEnd)
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
            if (!path.m_Looped && Mathf.Abs(cart.m_Position) < howCloseCanReachPathEnd)
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
