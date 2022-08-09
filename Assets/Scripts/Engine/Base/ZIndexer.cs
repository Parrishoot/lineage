using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndexer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "ZIndexer";
        SetZIndex();
    }

    private void Update()
    {
        SetZIndex();
    }

    private void SetZIndex()
    {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 100);
    }
}
