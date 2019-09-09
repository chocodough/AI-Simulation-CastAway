using UnityEngine;
using System.Collections;

public class HumanSense : MonoBehaviour
{
    public GameObject Human;
    public GameObject target;  //타겟(토끼, 과일나무, 부싯돌)

	void Start () {
        Human = GameObject.Find("Human");  // 사람 객체를 가져옴
    }
	
	
	void Update () {
        this.GetComponent<Transform>().position = Human.GetComponent<Transform>().position;
        this.GetComponent<Transform>().rotation = Human.GetComponent<Transform>().rotation;  // 위치 및 방향 재조정

        if (Human.GetComponent<HumanFSM>().humanstate == HumanFSM.HumanState.GoToTarget)  //타겟을 쫓는 중이라면
        {
            if (target == null)  //타겟이 없어졌으면
            {
                Human.GetComponent<HumanFSM>().humanstate = HumanFSM.HumanState.Wander;
                return;
            }
            Human.GetComponent<NavMeshAgent>().destination = target.GetComponent<Transform>().position;   // 네비게이션에 타겟 위치 전달
            Debug.DrawLine(target.GetComponent<Transform>().position, GetComponent<Transform>().position);  //타겟으로 직선을 그림
        }
    }

    void OnTriggerEnter(Collider coll)  // 타겟에 따른 FSM 상태 전환
    {
        if (coll.tag == "tree" || coll.tag == "rabbit" || coll.tag == "flint") 
        {
          if(coll.tag == "rabbit")
            { 
                if (Human.GetComponent<HumanFSM>().humanstate == HumanFSM.HumanState.LookForFood)
                {
                    target = coll.gameObject;
                    Human.GetComponent<HumanFSM>().humanstate = HumanFSM.HumanState.GoToTarget;                         
                }
                
            }

            else if (coll.tag == "tree")
            {
                if (Human.GetComponent<HumanFSM>().humanstate == HumanFSM.HumanState.LookForFood)
                {
                    target = coll.gameObject;
                    Human.GetComponent<HumanFSM>().humanstate = HumanFSM.HumanState.GoToTarget;
                }

            }

            else if (coll.tag == "flint")
            {
                if (Human.GetComponent<HumanFSM>().humanstate == HumanFSM.HumanState.Wander)
                {
                    target = coll.gameObject;
                    Human.GetComponent<HumanFSM>().humanstate = HumanFSM.HumanState.GoToTarget;
                }

            }

        }
    }

 
}
