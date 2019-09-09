using UnityEngine;
using System.Collections;

public class LookingForShip : MonoBehaviour
{
    public GameObject Human;
   

    void Start()
    {
        Human = GameObject.Find("Human");  //사람 객체를 가져옴
    }

    void OnTriggerEnter(Collider coll)  //배를 발견하면
    {
        if (coll.tag == "ship")
        {
            Human.GetComponent<HumanFSM>().FindShip = true;
            print("there`s a ship");
        }
    }

    
    void Update()
    {
        this.GetComponent<Transform>().position = Human.GetComponent<Transform>().position;
        this.GetComponent<Transform>().rotation = Human.GetComponent<Transform>().rotation;  //위치 및 방향 재조정
    }
}
