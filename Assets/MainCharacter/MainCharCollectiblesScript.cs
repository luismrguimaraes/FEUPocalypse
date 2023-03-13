using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharCollectiblesScript : MonoBehaviour
{
    private GameObject light2d;
    private GameObject logicManager;
    public float fullLightDuration = 10f;
    public float moveSpeedBoostDuration = 10f;
    public float moveSpeedBoostAmount = 1.8f;

    public AudioSource lightSwitchSfx;
    public AudioSource coinSoundEffect;

    // Start is called before the first frame update
    public void OnSceneTransitionStart()
    {
        light2d = GameObject.FindGameObjectWithTag("Light");
        logicManager = GameObject.FindGameObjectWithTag("LogicManager");

        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player Projectiles and Attacks"));
    }

    IEnumerator ReenableLight()
    {
        yield return new WaitForSeconds(fullLightDuration); // Wait
        if (light2d)
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
            else if (collision.gameObject.name == "SpinningCoin(Clone)")
            {
                logicManager.GetComponent<LogicScript>().GainCoins(collision.gameObject.GetComponent<CoinScript>().amount);
                coinSoundEffect.Play();
            }
            Destroy(collision.gameObject);
        }
    }
}
