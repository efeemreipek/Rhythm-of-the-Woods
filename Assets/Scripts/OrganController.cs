using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganController : MonoBehaviour
{
    [SerializeField] private List<Transform> organTransformList;

    private Transform currentOrgan;
    private int index = 0;
    private bool isGameOver = false;

    private void Start()
    {
        currentOrgan = organTransformList[index];
        currentOrgan.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (currentOrgan.GetComponent<Organ>().GetIsDead())
        {
            if(index == 4)
            {
                isGameOver = true;
                return;
            }
            currentOrgan.gameObject.SetActive(false);
            index++;
            currentOrgan = organTransformList[index];
            currentOrgan.gameObject.SetActive(true);
        }
    }

    public bool GetIsGameOver() => isGameOver;
}
