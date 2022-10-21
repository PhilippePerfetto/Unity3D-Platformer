using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnim : MonoBehaviour
{
    public Vector3 amount;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        var randomTime = Random.Range(time - 0.3f, time + 0.3f);

        iTween.PunchScale(gameObject, iTween.Hash(
            "amount", amount,
            "time", randomTime,
            "looptype", iTween.LoopType.loop));
    }
}
