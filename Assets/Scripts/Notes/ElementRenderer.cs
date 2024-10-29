using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRenderer : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int num = 0;

    private Renderer rd;
    private float alpha = 0f;

    private KeyCode[] keys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K };

    private void Awake()
    {
        rd = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!(rd.material.color.a <= 0f))
        {
            rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, alpha < 0f ? 0f : alpha);
        }

        if (Input.GetKeyDown(keys[num - 1]))
        {
            ColorChange();
        }
        alpha -= speed * Time.deltaTime;
    }

    private void ColorChange()
    {
        alpha = 0.3f;
        rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, alpha);
    }
}
