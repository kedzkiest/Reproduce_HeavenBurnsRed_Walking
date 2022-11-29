using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float playerInteractionDist;
    [SerializeField] private float yOffset;
    private Vector3 origin;
    private SplineInstantiate splineInstantiate;
    private float distToPlayer;
    private Vector3 apparentPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        splineInstantiate = GameObject.FindGameObjectWithTag("SplineInstantiate").GetComponent<SplineInstantiate>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        apparentPosition = transform.position - splineInstantiate.maxPositionOffset;
        distToPlayer = (player.transform.position - apparentPosition).magnitude;
        Debug.DrawRay(apparentPosition, player.transform.position - apparentPosition);

        if(distToPlayer < playerInteractionDist)
        {
            transform.position = origin + new Vector3(0, -yOffset, 0);
        }
        else
        {
            transform.position = origin;
        }
    }
}
