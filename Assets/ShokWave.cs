using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShokWave : MonoBehaviour
{

    public GameObject shockwave;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject prefab = Instantiate(shockwave, new Vector3(collision.gameObject.transform.position.x, -8.1f, 0), Quaternion.identity);
            Destroy(prefab, 2f);
        }
    }
}
