using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InventoryManager))]
//[RequireComponent(typeof(PlayerManager))]
//[RequireComponent(typeof(WeatherManager))]
//[RequireComponent(typeof(ImageManagers))]
[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour {
    //public static InventoryManager Inventory { get; private set; }
    //public static PlayerManager Player { get; private set; }
    //public static WeatherManager Weather { get; private set; }
    //public static ImageManagers Image { get; private set; }
    public static AudioManager Audio { get; private set; }

    private List<IGameManager> _startSequence1;

	void Awake(){
        //Player = GetComponent<PlayerManager>();
        //Inventory = GetComponent<InventoryManager>();
        //Weather = GetComponent<WeatherManager>();
        //Image = GetComponent<ImageManagers>();
        Audio = GetComponent<AudioManager>();

        _startSequence1 = new List<IGameManager> ();
        //_startSequence1.Add(Player);
        //_startSequence1.Add(Inventory);
        //_startSequence1.Add(Weather);
        //_startSequence1.Add(Image);
        _startSequence1.Add(Audio);

        StartCoroutine (StartupManagers());
	}
		

	private IEnumerator StartupManagers(){

        //NetworkService network = new NetworkService();
		foreach (IGameManager manager in _startSequence1) {
            manager.startUp();
            //manager.startUp(network);
        }
        


		yield return null;

        int numModule = _startSequence1.Count;
		int numReady = 0;

        while (numReady < numModule)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence1)
            {
                if (manager.status == ManagerStatus.started)
                {
                    numReady++;
                }
            }

         

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModule);
            }

            yield return null;
        }

        Debug.Log("All managers started up");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
