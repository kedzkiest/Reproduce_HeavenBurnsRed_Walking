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
        transform.position = cart.transform.position;

        // active character moving using keyinput
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