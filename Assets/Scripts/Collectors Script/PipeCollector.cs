using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolders;
    private float distance = 2.5f;
    private float lastPipesX;
    private float pipeMin = -1.61f;
    private float pipeMax = 1f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
        //Randomizing Y
        for (int i= 0; i < pipeHolders.Length; i++)
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        //Getting lastPipe X coord
        lastPipesX = pipeHolders[0].transform.position.x;
        for (int i = 1; i < pipeHolders.Length; i++)
        {
            if (lastPipesX < pipeHolders[i].transform.position.x)
                lastPipesX = pipeHolders[i].transform.position.x;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            RepositionPipe(target);
        }
    }

    private void RepositionPipe(Collider2D target)
    {
        Vector3 temp = target.transform.position;
        temp.x = lastPipesX + distance;
        temp.y = Random.Range(pipeMin, pipeMax);
        target.transform.position = temp;
        lastPipesX = temp.x;
    }
}
