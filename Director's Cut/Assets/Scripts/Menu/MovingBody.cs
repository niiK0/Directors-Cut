using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBody : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] Transform character;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private Transform targetPoint;

    private void Start()
    {
        targetPoint = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(character.position, targetPoint.position, moveSpeed * Time.deltaTime);

        character.position = newPosition;

        if(Vector3.Distance(targetPoint.position, character.position) <= 0.5f)
        {
            if(targetPoint == pos1)
            {
                targetPoint = pos2;
            }
            else
            {
                targetPoint = pos1;
            }

            character.rotation = targetPoint.rotation;
        }
    }
}
