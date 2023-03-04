using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharCollectiblesScript : MonoBehaviour
{
    public GameObject light2d;
    public float fullLightDuration = 2.5f;
    public float moveSpeedBoostDuration = 2.5f;
    public float moveSpeedBoostAmount = 1.8f;

    public AudioSource lightSwitchSfx;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GameObject.FindGameObjectWithTag("Light");
    }

    IEnumerator ReenableLight()
    {
        yield return new WaitForSeconds(fullLightDuration); // Wait
        light2d.SetActive(true);
    }

    IEnumerator RemoveMoveSpeedBoost()
    {
        yield return new WaitForSeconds(moveSpeedBoostDuration); // Wait
        gameObject.GetComponent<MainCharMovementScript>().moveSpeed /= moveSpeedBoostAmount;
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
                lightSwitchSfx.Play();
            }
            else if (collision.gameObject.name == "MoveSpeedBoostDrop(Clone)")
            {
                gameObject.GetComponent<MainCharMovementScript>().moveSpeed *= moveSpeedBoostAmount;
                StartCoroutine("RemoveMoveSpeedBoost");

            }
            Destroy(collision.gameObject);
        }
    }
}
