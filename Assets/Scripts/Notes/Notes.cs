using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 8.0f;
    [SerializeField] private bool start;

    private void Start()
    {
        noteSpeed = GameManager.Instance.noteSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }
        if (start)
        {
            NoteMovement();
        }
    }

    private void NoteMovement()
    {
        transform.position -= transform.forward * noteSpeed * Time.deltaTime;
    }
}
