using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePieceBuildOrder : MonoBehaviour
{
    public List<OnePieceRaftModule> buildOrder;

    void checkIfCanBuild()
    {
        if (GameController.Instance.gcInventory.Barrels < buildOrder[0].neededBarrels) return;
        if (GameController.Instance.gcInventory.Logs < buildOrder[0].neededLogs) return;
        if (GameController.Instance.gcInventory.Planks < buildOrder[0].neededPlanks) return;
        buildOrder[0].gameObject.SetActive(true);
        buildOrder.RemoveAt(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( buildOrder.Count > 0)
        {
            checkIfCanBuild();
        }
    }
}
