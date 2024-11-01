using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f;

    private void Reset()
    {
        rotationSpeed = 60f;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
