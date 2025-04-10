using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float BackgroundMoveSpeed = 3f; // Movement speed
    private SpriteRenderer sr;
    
    // private Material mat
    private Vector2 offset;

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //mat = GetComponent<SpriteRenderer>().material;
        offset = sr.material.mainTextureOffset;
    }


    void Update()
    {
        offset.x += BackgroundMoveSpeed * Time.deltaTime;
        sr.material.mainTextureOffset = offset ;
    }
}
