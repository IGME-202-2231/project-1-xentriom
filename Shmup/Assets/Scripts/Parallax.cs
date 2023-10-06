using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)] public float scrollSpeed = 0.5f;
    private float offset = 0f;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (scrollSpeed * Time.deltaTime) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}