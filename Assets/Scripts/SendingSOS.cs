using UnityEngine;
using System.Collections;

public class SendingSOS : MonoBehaviour {
    public GameObject Human;
    // Use this for initialization
    void Start () {
        Human = GameObject.Find("Human");
        
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Transform>().rotation = Human.GetComponent<Transform>().rotation;   
        this.GetComponent<Transform>().position = Human.GetComponent<Transform>().position;   // 위치 및 방향 재조정

        if (Human.GetComponent<HumanFSM>().humanstate == HumanFSM.HumanState.SendSOS)         // SOS를 보내는 상태면
        {
            if (this.GetComponent<Transform>().localScale.x < Human.GetComponent<HumanFSM>().Flints * 20)   //현재 부싯돌 개수 X 20만큼 퍼짐
            {
                this.GetComponent<Transform>().localScale += new Vector3(1,1,1) * Time.deltaTime * 20;
                
                
            }
        }
    }
}
