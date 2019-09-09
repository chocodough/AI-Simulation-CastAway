using UnityEngine;
using System.Collections;

public class HumanFSM : MonoBehaviour {

    public enum HumanState { Wander, SendSOS,GoToBed,Sleep,GoToTop,LookForFood, GoToTarget, GoToEnd }  //사람의 상태

    public HumanState humanstate = HumanState.Wander;  // 상태를 Wander로 초기화

    public GameObject MountTop;   // 산 정상의 위치
    public GameObject Home;       // 집의 위치

    public float Helth;           // 피로도
    public float Hungry;          // 허기
    public int Flints;            // 부싯돌 개수

    public NavMeshAgent nvAgent;

    public Transform[] p = new Transform[5];

    public bool FindShip = false;         // 배를 발견했는지 확인
    public bool FoundTarget = false;      // 목표를 발견했는지 확인

    public Transform destination;     // 목표 지점 설정(p1 ~ p5)

    private float SOStime = 0; 

    void HumanAction()  // FSM 각 상태별 동작
    {
        switch(humanstate)
        {
            case HumanState.Wander:   
                GetComponent<MeshRenderer>().material.color = Color.white;     
                nvAgent.speed = 11;
                nvAgent.acceleration = 11;                                     //속도 재조정
                if (nvAgent.destination.x == GetComponent<Transform>().position.x
                    && nvAgent.destination.z == GetComponent<Transform>().position.z)  //목표 지점에 도착하면
                {
                    destination = p[Random.Range(0, 4)];
                    if(nvAgent.destination != destination.position)
                    {
                        nvAgent.destination = destination.position;
                    }  // 다음 목표 지점 설정                 
                }
                break;
            case HumanState.LookForFood:
                GetComponent<MeshRenderer>().material.color = Color.yellow;

                if (nvAgent.destination.x == GetComponent<Transform>().position.x
                    && nvAgent.destination.z == GetComponent<Transform>().position.z)  //목표 지점에 도착하면
                {
                    destination = p[Random.Range(0, 4)];
                    if (nvAgent.destination != destination.position)
                    {
                        nvAgent.destination = destination.position;
                    }                      // 다음 목표 지점 설정
                }
                break;
            case HumanState.GoToTarget:
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;

            case HumanState.GoToTop:
                nvAgent.destination = MountTop.GetComponent<Transform>().position;     
                GetComponent<MeshRenderer>().material.color = Color.blue;
                nvAgent.speed = 12;
                nvAgent.acceleration = 12;                // 속도 증가
                break;

            case HumanState.GoToBed: 
                nvAgent.destination = Home.GetComponent<Transform>().position;
                GetComponent<MeshRenderer>().material.color = Color.grey;
                break;

            case HumanState.Sleep:   
                this.GetComponent<Transform>().position = Home.GetComponent<Transform>().position;  //위치를 집으로 고정
                GetComponent<MeshRenderer>().material.color = Color.black;
                Helth += Time.deltaTime * 10;
                if (Helth > 100)                                    // 체력이 차면 Wander로 전환                    
                    humanstate = HumanState.Wander;                
                break;

            case HumanState.SendSOS:
                GetComponent<MeshRenderer>().material.color = Color.green;
                SOStime += Time.deltaTime;
                int shipcount = (int)GameObject.FindGameObjectsWithTag("ship").Length;
                if (shipcount != 0)   // 현재 배가 있는지 확인 
                {
                    if (GameObject.Find("Ship(Clone)").GetComponent<ShipControl>().GoToIsland)
                        humanstate = HumanState.GoToEnd;
                }
                if(SOStime > 10)   // 배가 신호를 확인하지 못한 경우
                {
                    SOStime = 0;
                    humanstate = HumanState.Wander;
                    nvAgent.speed = 11;
                    nvAgent.acceleration = 11;
                    GameObject.Find("SOS").GetComponent<Transform>().localScale = new Vector3(1,1,1);  //부피를 다시 줄임
                }

                break;
            case HumanState.GoToEnd:
                GetComponent<MeshRenderer>().material.color = Color.blue;
                GameObject.Find("SOS").GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);  //부피를 다시 줄임
                nvAgent.destination = GameObject.Find("EndPoint").GetComponent<Transform>().position;
                break;
            default:
                break;
        }
        return;
    }

    void CheckHumanState()   //FSM 상태간의 전환
    {
        if (humanstate == HumanState.GoToEnd)
        {
            return;
        }
        if (humanstate == HumanState.SendSOS)
        {
            return;
        }
        if(humanstate == HumanState.GoToTop
            && MountTop.GetComponent<Transform>().position.x == GetComponent<Transform>().position.x
            && MountTop.GetComponent<Transform>().position.z == GetComponent<Transform>().position.z)
        {
            FindShip = false;
            humanstate = HumanState.SendSOS;
            return;
        }
            if (FindShip)
        {
            humanstate = HumanState.GoToTop;
            return;
        }
        if ( humanstate == HumanState.Wander
            && Helth == 0)
        {
            humanstate = HumanState.GoToBed;
            return;
        }
        if (humanstate == HumanState.GoToBed
            && Home.GetComponent<Transform>().position.x == GetComponent<Transform>().position.x
            && Home.GetComponent<Transform>().position.z == GetComponent<Transform>().position.z)
        {
            humanstate = HumanState.Sleep;
            return;
        }
        if (humanstate == HumanState.Sleep)
        {
            return;
        }
        if (humanstate == HumanState.Wander
            && Hungry < 50)
        { 
            humanstate = HumanState.LookForFood;
            return;
        }
        if(humanstate == HumanState.Wander
            && FoundTarget)
        {
            humanstate = HumanState.GoToTarget;
            return;
        }
        if(humanstate == HumanState.LookForFood
            && FoundTarget)
        {
            humanstate = HumanState.GoToTarget;
            return;
        }
        
    }

    void Start() {

        for (int i = 0; i < 5; i++)
        {
            p[i] = GameObject.Find("p" + (i + 1).ToString()).GetComponent<Transform>();  //다섯 지점의 위치 저장
        }

        destination = p[Random.Range(0, 4)];  // 목표지점 초기화

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = destination.position;



    }

    void Update() {
        Hungry = Hungry - Time.deltaTime * 1;
        Helth = Helth - Time.deltaTime * 1;         //각 변수를 초당 1씩 감소시킴

        if (Helth < 0)
            Helth = 0;
        if (Hungry < 0)
            Hungry = 0;

        CheckHumanState();    // FSM 상태를 확인
        HumanAction();        // 상태별 동작 수행 
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "rabbit" && humanstate==HumanState.GoToTarget
            &&coll.gameObject == GameObject.Find("HumanSense").GetComponent<HumanSense>().target)
        {
            destination = p[Random.Range(0, 4)];
            nvAgent.destination = destination.position;
            humanstate = HumanState.Wander;
            Destroy(coll.gameObject);
            Hungry += 100;
        }
        else if (coll.gameObject.tag == "tree" && humanstate == HumanState.GoToTarget
            && coll.gameObject == GameObject.Find("HumanSense").GetComponent<HumanSense>().target)
        {
            destination = p[Random.Range(0, 4)];
            nvAgent.destination = destination.position;
            humanstate = HumanState.Wander;
            Destroy(coll.gameObject);
            Hungry += 50;   
        }
        else if(coll.gameObject.tag == "flint" && humanstate == HumanState.GoToTarget
            && coll.gameObject == GameObject.Find("HumanSense").GetComponent<HumanSense>().target)
        {
            destination = p[Random.Range(0, 4)];
            nvAgent.destination = destination.position;
            humanstate = HumanState.Wander;
            Destroy(coll.gameObject);
            Flints++;     // 부싯돌 개수 증가
        }
    }
}

