using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharCollectiblesScript : MonoBehaviour
{
    public GameObject light2d;
    public float fullLightDuration = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GameObject.FindGameObjectWithTag("Light");
    }

    IEnumerator ReenableLight()
    {
        yield return new WaitForSeconds(fullLightDuration);// Wait a bit
        light2d.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
        {
            if (collision.gameObject.name == "FullVisionDrop(Clone)")
            {
                light2d.SetActive(false);
                StopCoroutine("ReenableLight");
                StartCoroutine("ReenableLight");
            }
            else if (collision.gameObject.name == "InvuDrop(Clone)")
            {

            }
            Destroy(collision.gameObject);
        }
    }
}
