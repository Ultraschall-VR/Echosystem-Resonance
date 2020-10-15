using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    float timeCounter = 0;

    public float speed=1;
    public float width=2;
    //public float height;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        timeCounter += Time.deltaTime*speed;

        float x = Mathf.Cos (timeCounter)*width;
        float y = 1;
        float z = Mathf.Sin (timeCounter)*width;

        transform.position = transform.localPosition + new Vector3(x, y, z);
    }
}
