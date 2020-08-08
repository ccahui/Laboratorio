using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    calculatingTarget,
    preparingToAttack,
    attacking,
    afterAttack,
};

[RequireComponent(typeof(Collider))]
public class DoomBall : MonoBehaviour
{
    [SerializeField] Transform target;
    public Vector3 spawnPoint;
    Vector3 lokOnTarget;
    public BallState state;
    public float speed = 10.0f;
    public int reactionTime = 1;
    private Collider collider;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BallState.calculatingTarget:
                state = BallState.preparingToAttack;
                StartCoroutine("PrepareToAttack");
                break;
            case BallState.preparingToAttack:
                break;
            case BallState.attacking:
                if(Vector3.Distance(transform.position,lokOnTarget) > float.Epsilon)
                {
                    transform.position = Vector3.MoveTowards(transform.position,lokOnTarget,Time.deltaTime*speed);
                } else
                {
                    state = BallState.calculatingTarget;
                }
                break;
            case BallState.afterAttack:

                break;
        }
    }

    IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(reactionTime);
        state = BallState.attacking;
        lokOnTarget = target.position + Vector3.Normalize(target.position-transform.position)*2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Doing dolor");
            transform.position = spawnPoint;
            state = BallState.calculatingTarget;
        }
    }
}
