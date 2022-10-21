using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    int nbCoins = 0;
    public GameObject pickupParticles;
    public GameObject mobParticles;
    bool canInstantiate = true;
    public GameObject cam1, cam2;
    public AudioClip hitSound;
    AudioSource audioSource;
    bool isInvincible = false;
    public SkinnedMeshRenderer meshRenderer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.tag = "CoinTaken";
            audioSource.PlayOneShot(hitSound);
            var part = Instantiate(pickupParticles, other.transform.position, Quaternion.identity);
            Destroy(part, 0.6f);
            Destroy(other.gameObject);
            PlayerInfo.Instance.AddCoin();
            print(nbCoins);
        }
        else if (other.gameObject.CompareTag("Cam1"))
        {
            cam1.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Cam2"))
        {
            cam2.SetActive(true);
        }
        else if (other.gameObject.name == "End")
        {
            print(PlayerInfo.Instance.GetScore());
        }
        else if (other.gameObject.CompareTag("Eau"))
        {
            StartCoroutine(nameof(ResetScene));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cam1"))
        {
            cam1.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Cam2"))
        {
            cam2.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Hurt") && !isInvincible)
        {
            isInvincible = true;
            iTween.PunchPosition(gameObject, Vector3.back * 5, 0.5f);
            StartCoroutine(nameof(ResetInvincible));
            PlayerInfo.Instance.SetHealth(-1);
            print("Aie");
        }
        else if (hit.gameObject.CompareTag("Mob") && canInstantiate)
        {
            canInstantiate = false;
            audioSource.PlayOneShot(hitSound);
            iTween.PunchScale(hit.gameObject.transform.parent.gameObject, new Vector3(50f, 50f, 50f), 0.6f);

            var part = Instantiate(pickupParticles, hit.transform.position, Quaternion.identity);
            Destroy(part, 0.6f);
            print("Mange !");
            Destroy(hit.gameObject.transform.parent.gameObject, 0.5f);

            StartCoroutine(nameof(ResetInstantiate));
        } 
    }

    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1.0f);

        int build = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(build);
    }

    IEnumerator ResetInvincible()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            meshRenderer.enabled = !meshRenderer.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        meshRenderer.enabled = true;
        isInvincible = false;
    }
}
