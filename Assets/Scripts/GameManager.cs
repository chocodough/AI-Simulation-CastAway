using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Transform[] Treepoints; 
    public Transform[] Grasspoints; 
    public Transform[] Flintpoints; 
    public Transform[] Shippoints;   
    public Transform[] Rabbitpoints; // 각 객체의 생성 지점

    public GameObject TreePrefab; 
    public GameObject GrassPrefab;
    public GameObject FlintPrefab; 
    public GameObject ShipPrefab; 
    public GameObject RabbitPrefab;  // 각 객체의 프리팹

    public float TreecreateTime;
    public float GrasscreateTime;
    public float FlintcreateTime;
    public float ShipcreateTime;
    public float RabbitcreateTime;  // 각 객체의 생성시간

    public int maxtree;  
    public int maxgrass;
    public int maxflint;
    public int maxship;
    public int maxrabbit;         //각 객체의 최대 개수

    public bool isgameover = false;  


	// Use this for initialization
	void Start () {
        Treepoints = GameObject.Find("TreeSpawnPoint").GetComponentsInChildren<Transform>();
        Grasspoints = GameObject.Find("GrassSpawnPoint").GetComponentsInChildren<Transform>();
        Flintpoints = GameObject.Find("FlintSpawnPoint").GetComponentsInChildren<Transform>();
        Shippoints = GameObject.Find("ShipSpawnPoint").GetComponentsInChildren<Transform>();
        Rabbitpoints = GameObject.Find("RabbitSpawnPoint").GetComponentsInChildren<Transform>();    //생성지점 전달

        StartCoroutine(this.CreateTree());
        StartCoroutine(this.CreateGrass());
        StartCoroutine(this.CreateFlint());
        StartCoroutine(this.CreateShip());
        StartCoroutine(this.CreateRabbit());  //객체별 코루틴 함수 실행
    }
	

	IEnumerator CreateTree() {
	    if(Treepoints.Length > 0)
        {
            while(!isgameover)
            {
                int treecount = (int)GameObject.FindGameObjectsWithTag("tree").Length; // 현재 나무의 개수

                if (treecount < maxtree)   //현재 나무의 개수가 최대 나무개수보다 적으면
                {
                    yield return new WaitForSeconds(TreecreateTime);   //생성시간만큼 대기

                    int idx = Random.Range(1, Treepoints.Length);

                    bool ThereIsAlreadyOne = false;   //생성하려는 위치에 이미 해당 객체가 존재하는지 확인

                    Collider[] colls = Physics.OverlapSphere(Treepoints[idx].position, 1 / 4);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.tag == "tree")
                            ThereIsAlreadyOne = true;
                    }    
                    if(!ThereIsAlreadyOne)   //이미 존재할 경우에는 생성하지 않음
                        Instantiate(TreePrefab, Treepoints[idx].position, Treepoints[idx].rotation);
                }
                else
                    yield return null;
            }
        }
	}

    IEnumerator CreateGrass()
    {
        if (Grasspoints.Length > 0)
        {
            while (!isgameover)
            {
                int grasscount = (int)GameObject.FindGameObjectsWithTag("grass").Length;

                if (grasscount < maxgrass)
                {
                    yield return new WaitForSeconds(GrasscreateTime);

                    int idx = Random.Range(1, Grasspoints.Length);

                    bool ThereIsAlreadyOne = false;
                    Collider[] colls = Physics.OverlapSphere(Grasspoints[idx].position, 1 / 4);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.tag == "grass")
                            ThereIsAlreadyOne = true;
                    }   
                    if (!ThereIsAlreadyOne)
                        Instantiate(GrassPrefab, Grasspoints[idx].position, Grasspoints[idx].rotation);
                }
                else
                    yield return null;
            }
        }
    }

    IEnumerator CreateFlint()
    {
        if (Flintpoints.Length > 0)
        {
            while (!isgameover)
            {
                int flintcount = (int)GameObject.FindGameObjectsWithTag("flint").Length;

                if (flintcount < maxflint)
                {
                    yield return new WaitForSeconds(FlintcreateTime);

                    int idx = Random.Range(1, Flintpoints.Length);


                    bool ThereIsAlreadyOne = false;
                    Collider[] colls = Physics.OverlapSphere(Flintpoints[idx].position, 1 / 4);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.tag == "flint")
                            ThereIsAlreadyOne = true;
                    }   
                    if(!ThereIsAlreadyOne)
                    Instantiate(FlintPrefab, Flintpoints[idx].position, Flintpoints[idx].rotation);
                }
                else
                    yield return null;
            }
        }
    }


    IEnumerator CreateShip()
    {
        if (Shippoints.Length > 0)
        {
            while (!isgameover)
            {
                int shipcount = (int)GameObject.FindGameObjectsWithTag("ship").Length;


                if (shipcount < maxship)
                {
                    yield return new WaitForSeconds(ShipcreateTime);

                    int idx = Random.Range(1, Shippoints.Length);

                    bool ThereIsAlreadyOne = false;
                    Collider[] colls = Physics.OverlapSphere(Shippoints[idx].position, 1 / 4);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.tag == "ship")
                            ThereIsAlreadyOne = true;
                    }  
                    if (!ThereIsAlreadyOne)
                        Instantiate(ShipPrefab, Shippoints[idx].position, Shippoints[idx].rotation);
                }
                else
                    yield return null;
            }
        }
    }


    IEnumerator CreateRabbit()
    {
        if (Rabbitpoints.Length > 0)
        {
            while (!isgameover)
            {
                int rabbitcount = (int)GameObject.FindGameObjectsWithTag("rabbit").Length;

                if (rabbitcount < maxrabbit)
                {
                    yield return new WaitForSeconds(RabbitcreateTime);

                    int idx = Random.Range(1, Rabbitpoints.Length);

                    bool ThereIsAlreadyOne = false;
                    Collider[] colls = Physics.OverlapSphere(Rabbitpoints[idx].position, 1 / 4);
                    foreach (Collider coll in colls)
                    {
                        if (coll.gameObject.tag == "rabbit")
                            ThereIsAlreadyOne = true;
                    }   
                    if (!ThereIsAlreadyOne)
                        Instantiate(RabbitPrefab, Rabbitpoints[idx].position, Rabbitpoints[idx].rotation);
                }
                else
                    yield return null;
            }
        }
    }
}
