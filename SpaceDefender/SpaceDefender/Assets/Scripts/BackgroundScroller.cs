using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material backgrounMaterial;
    Vector2 offset;

    void Start()
    {
        backgrounMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        backgrounMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
