using TMPro;
using UnityEngine;

public class HelpFriends : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject merciGameObject;
    GameObject cell = null;
    bool canOpen = true;

    private void OnTriggerEnter(Collider other)
    {
        print("IN " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Cell"))
        {
            cell = other.gameObject;
            text.text = "Appuyer sur E pour ouvrir la cage";
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("OUT " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Cell"))
        {
            cell = null;
            text.text = string.Empty;
            canOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use") && canOpen)
        {
            //iTween.ShakeRotation(cell, new Vector3(45, 45, 45), 1f);
            //float shakeAmount = 1.0f, delay = 0.0f, duration = 1.0f;
            //iTween.ShakeRotation(transform.gameObject, iTween.Hash("x", shakeAmount, "time", duration, "delay", delay, "easetype", iTween.EaseType.easeInOutQuint, "onupdate", "StayWithParent", "oncomplete", "StayWithParent"));
            
            iTween.ShakeScale(cell, new Vector3(145, 145, 145), 1f);
            Destroy(cell, 1.2f);

            var merci = Instantiate(merciGameObject, cell.transform.position, Quaternion.identity);
            Destroy(merci, 5f);

            text.text = string.Empty;
        }
    }
}

