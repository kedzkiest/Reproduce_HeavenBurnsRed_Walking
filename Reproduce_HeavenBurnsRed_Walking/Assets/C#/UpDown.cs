using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float playerInteractionDist;
    private Vector3 origin;
    private float distToPlayer;
    private Vector3 apparentPosition;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        apparentPosition = origin + offset;
        distToPlayer = (player.transform.position - apparentPosition).magnitude;

        float depth = distToPlayer - playerInteractionDist;
        if (depth > 0) return;
        transform.position = origin + new Vector3(0, depth, 0);
    }
}
