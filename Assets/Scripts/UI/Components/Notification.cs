using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactivate", 0.5f);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
