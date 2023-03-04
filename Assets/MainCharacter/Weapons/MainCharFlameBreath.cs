using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainCharFlameBreath : MonoBehaviour
{
    public GameObject flameBreathPrefab;

    public float attackCD = 1f;
    private float attackTimer = 0;
    MainCharMovementScript mainCharMovement;
    public int XOffset = 5;

    private void Start()
    {
        attackTimer = 0f;
        mainCharMovement = gameObject.GetComponent<MainCharMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer < attackCD)
        {
            attackTimer += Time.deltaTime;
        }
        else if (Mathf.Abs(  mainCharMovement.GetFacingDirection().x  ) > 0.5)
        {
            //Breathe();
            attackTimer = 0;
        }
    }

    private void Breathe()
    {
        if (mainCharMovement.. > 0)
        {
            GameObject flame = Instantiate(flameBreathPrefab, transform.position + new Vector3(XOffset, 0, 0) , transform.rotation);
        }
        else
        {
            GameObject flame = Instantiate(flameBreathPrefab, transform.position + new Vector3(-XOffset, 0, 0), transform.rotation);
            flame.transform.Rotate(Vector3.up, Mathf.PI);
        }
    }
}
