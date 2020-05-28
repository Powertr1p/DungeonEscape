using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    [SerializeField] private Transform _waypointA;
    [SerializeField] private Transform _waypointB;

    private Transform _target;

    private void Start()
    {
        _target = _waypointB.transform;
    }
    
    
    protected override void Update()
    {
        if (transform.position.x == _waypointA.transform.position.x)
            _target = _waypointB.transform;
        else if (transform.position.x == _waypointB.transform.position.x)
            _target = _waypointA.transform;

        transform.position = Vector2.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
    }
}
