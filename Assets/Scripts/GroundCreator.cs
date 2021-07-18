using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    
    Transform ball;
    public GameObject blockPrefab;
    public Transform lastBlock;
    Vector3 lastBlockPos;
    private float offset = 0.707f;   // her yeni blok x ve z vektörleri 0.707 birim uzaklıkta oluşacak
    Camera cam;
    
    
    void Start()
    {
        cam= Camera.main;
        ball = FindObjectOfType<BallController>().transform;
        lastBlockPos=lastBlock.position;
        InvokeRepeating("CreateGround", 0f, 1f/5);
    }
    
    private void CreateGround()
    {
        float distance = Vector3.Distance(ball.position, lastBlockPos);
        if(distance > cam.orthographicSize*2)   // perspektif yerine ortografik kamera kullanıldı
        {
            return;
        }

        var newBlock = Instantiate(blockPrefab,transform);
        int chance = Random.Range(1,11);
        if(chance>5)
        {
            //sola doğru oluşan yeni bloklar x için -0.707, z için -0.707 
            newBlock.transform.position= new Vector3(lastBlockPos.x - offset, lastBlockPos.y ,lastBlockPos.z - offset);
        }
        else
        {
            //ileri doğru gelen yeni bloklar x için -0.707, z için +0.707 
            newBlock.transform.position= new Vector3(lastBlockPos.x - offset, lastBlockPos.y ,lastBlockPos.z + offset);
        
        }
        bool randomEnabler = chance % 3 == 2;
        newBlock.transform.GetChild(0).gameObject.SetActive(randomEnabler);
        newBlock.transform.rotation=Quaternion.Euler(0,45,0);
        lastBlockPos=newBlock.transform.position;
        
    }
}
