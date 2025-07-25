using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMask7 : MonoBehaviour
{
    private int layerNumber = 7;
    private int layerMask;
    private RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << layerNumber; // Bitwise left shift operator to represent layer number by a single bit in the 32-bit integer   
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Ray hit " + raycastHit.transform.gameObject.layer);
        }
    }
}