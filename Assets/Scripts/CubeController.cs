using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public HandleControlls handleControlls;
    public ICreatorUnit unitCreator;
    public float speed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + handleControlls.deltaPosition.x * speed,
            transform.position.y,
            transform.position.z + +handleControlls.deltaPosition.y * speed);
    }
}
