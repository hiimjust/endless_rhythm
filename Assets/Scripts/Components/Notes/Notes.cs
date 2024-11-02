using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private float noteSpeed;

    private void Start()
    {
        noteSpeed = GameManager.Instance.noteSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.play)
        {
            NoteMovement();
        }
    }

    private void NoteMovement()
    {
        transform.position -= transform.forward * noteSpeed * Time.deltaTime;
    }
}
