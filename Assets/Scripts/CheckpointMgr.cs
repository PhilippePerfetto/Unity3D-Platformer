using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{
    Vector3 lastPoint;

    // Start is called before the first frame update
    void Start()
    {
        lastPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            lastPoint = transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
            var material = other.gameObject.GetComponent<MeshRenderer>().material;
            material.EnableKeyword("_EMISSION");
        }
    }

    public void Respawn()
    {
        print("respawn on " + gameObject.name);
        gameObject.transform.position = lastPoint;
        transform.position = lastPoint;
        PlayerInfo.Instance.SetHealth(3);
    }
}
