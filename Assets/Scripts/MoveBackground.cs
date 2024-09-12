using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    private float lenght;
    private float startPos;
    private GameObject Camera;
    [SerializeField] private float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Virtual Camera");
        startPos = transform.position.x;
        lenght = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (Camera.transform.position.x * (1 - parallaxEffect));
        float distance = (Camera.transform.position.x * parallaxEffect);
        Vector3 target = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (temp < startPos - lenght)
        {
            startPos -= lenght;
        }

    }
}