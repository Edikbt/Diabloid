using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;

    private void Start()
    {
        _navMesh.SetDestination(new Vector3(10, 0, 10));
    }
}
