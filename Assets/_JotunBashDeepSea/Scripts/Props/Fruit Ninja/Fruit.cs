using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Rigidbody[] _CutsRB;
    public MeshCollider[] _CutsMC;
    private Rigidbody _Rigidbody;
    private SphereCollider _SphereCollider;
    void Start()
    {
        _CutsRB = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _CutsRB)
            rb.isKinematic = true;
        _CutsMC = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in _CutsMC)
            mc.isTrigger = true;
        _Rigidbody = GetComponent<Rigidbody>();
        _SphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        transform.LookAt(GameController.Instance.gcFruitNinja._Katana.transform,Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapons")
        {
            foreach (Rigidbody rb in _CutsRB)
            {
                rb.isKinematic = false;
                rb.velocity = _Rigidbody.velocity;
            }
            _Rigidbody.isKinematic = true;
            GameController.Instance.gcFruitNinja._Score += 1;
            Destroy(_SphereCollider);
            foreach (MeshCollider mc in _CutsMC)
                mc.isTrigger = false;
            Destroy(this);
        }
    }
}
