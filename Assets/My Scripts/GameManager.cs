using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private MLInput.Controller controller;
    private int kill;
    public Text killCount;

    private Transform gates;
    private int totalGates;

    private float timer;

    private  float rate = 20f;
    
    [SerializeField] private float min = 10f;
    [SerializeField] private float max = 30f;

    [SerializeField] private int maxGates = 2;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        //MLInput.OnControllerButtonUp += OnButtonUp;
        controller = MLInput.GetController(MLInput.Hand.Left);
        kill = 0;
        gates = Environment.instance.transform.GetChild(3);//0:Mapper 1:MapperNode 2:Origin 3:Gates

        totalGates = gates.childCount;
        
        //0:cylinder 1:Generattor 2:Hole 3:Collider
        
        foreach (Transform gate in gates)
        {
            gate.GetChild(3).gameObject.SetActive(false);//Colliderの消去

        }

        gates.GetChild(0).GetChild(0).gameObject.SetActive(true);
        gates.GetChild(0).GetChild(1).gameObject.SetActive(true);
        gates.GetChild(0).GetChild(2).gameObject.SetActive(true);
        
    }


 void OnDestroy() {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        //MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

void OnButtonDown(byte controllerId, MLInput.Controller.Button button) 
{
        if (button == MLInput.Controller.Button.HomeTap) 
        {
            Application.Quit();
        }
        else if(button == MLInput.Controller.Button.Bumper)
        {

        }
}

public void Kill()
{
    kill += 1;
}

int RefKill()
{
    return(kill);
}

private void DestroyGates()
{
    foreach (Transform gate in gates)
        {
            gate.GetChild(0).gameObject.SetActive(false);
            gate.GetChild(1).gameObject.SetActive(false);
            gate.GetChild(2).gameObject.SetActive(false);
        }
}

private void Reset()
{
    DestroyGates();
    List<int> numbers = new List<int>();

    int holeNum = Random.Range(1,maxGates + 1);
    int count = 0;

    Debug.Log(holeNum);

    for(int i = 0; i < gates.childCount; i++)
        {
            numbers.Add(i);
        }

    while (count < holeNum) {
 
            int index = Random.Range(0, numbers.Count);
            int num = numbers[index];

            gates.GetChild(num).GetChild(0).gameObject.SetActive(true);
            gates.GetChild(num).GetChild(1).gameObject.SetActive(true);
            gates.GetChild(num).GetChild(2).gameObject.SetActive(true);
            
            numbers.RemoveAt(index);
            count += 1;
        }
}


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        killCount.text = "Score : " + kill;

        if(timer > rate)
        {
            Reset();
            rate = Random.Range(min,max);
            timer = 0;
        }
    }
}
