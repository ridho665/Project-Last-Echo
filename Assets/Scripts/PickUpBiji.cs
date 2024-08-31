using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBiji : MonoBehaviour
{

    public GameObject Biji;
    // Start is called before the first frame update
    void Start()
    {
        Biji.SetActive(false);
        
    }

    // Update is called once per frame



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0)) // Menggunakan Input.GetMouseButtonDown(0) untuk klik kiri mouse

                this.gameObject.SetActive(false);
            Biji.SetActive(true);
        }

    }
    void Update()
    {

    }
}
