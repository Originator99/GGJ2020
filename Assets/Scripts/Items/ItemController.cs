using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour {
    private void Start() {
        StartCoroutine(MoveTowardsUI());
    }

    private IEnumerator MoveTowardsUI() {
        transform.Find("Trail").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.DOMove(GameObject.FindGameObjectWithTag("Player").transform.position, 1f).OnComplete(delegate() {
            Destroy(gameObject);
        });
    }
}
