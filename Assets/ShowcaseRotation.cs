using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseRotation : MonoBehaviour
{
    private float val = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0, val += Time.deltaTime *25);
    }
}
