using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameWorldUI_MovingObject : MonoBehaviour
{
    public float destroyDelay;

    public Vector3 movingVector;

    public float fadeAwaySpeed;

    public CanvasGroup CanvasGroup;

    public void StartMovement()
    {
        StartCoroutine(MovementCo());
    }

    private IEnumerator MovementCo()
    {
        float time = 0;

        while(time < destroyDelay)
        {
            time += Time.deltaTime;
            CanvasGroup.alpha -= fadeAwaySpeed * Time.deltaTime;
            transform.Translate(movingVector * Time.deltaTime);
            yield return null;
        }

        Destroy(this.gameObject);
    }

    private void Start()
    {
        StartMovement();
    }
}

