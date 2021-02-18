using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedOfSpin = 1f;

    private void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }

}
