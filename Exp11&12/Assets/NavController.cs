using UnityEngine;
using UnityEngine.AI;

public class NavController : MonoBehaviour {
    private NavMeshAgent _agent;
    
    private void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                _agent.SetDestination(new Vector3(hit.point.x, 0.5f, hit.point.z));
            }
        }        
    }
}
