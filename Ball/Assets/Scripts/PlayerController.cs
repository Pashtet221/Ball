using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject focalPoint;
    private float speed = 5f;
    private float powerUpStrength = 15f;
    public bool hasPower = false;
    public GameObject powerUpIndicator;

    void Start()
    {
        focalPoint = GameObject.Find("FocalPoint");
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerUpIndicator.transform.position = transform.position + new Vector3(0,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPower = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPower = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPower)
        {
            Rigidbody enemyrb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyrb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

        }
    }
}
