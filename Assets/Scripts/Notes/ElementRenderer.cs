using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementRenderer : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private int num = 0;

    private Renderer rd;
    private float alpha = 0f;
    private float materialAlpha;

    private void Awake()
    {
        rd = GetComponent<Renderer>();
        materialAlpha = rd.material.color.a;
    }

    private void Start()
    {
        Debug.Log(materialAlpha);
    }

    private void Update()
    {
        if (!(rd.material.color.a <= 0f))
        {
            rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, alpha < 0f ? 0f : alpha);
        }

        if (num == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                ColorChange();
            }
        }
        if (num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ColorChange();
            }
        }
        if (num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ColorChange();
            }
        }
        if (num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ColorChange();
            }
        }
        alpha -= speed * Time.deltaTime;
    }

    private void ColorChange()
    {
        alpha = 0.3f;
        rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, alpha);
    }
}
