using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Transform _Lookat;
    public Rigidbody[] _CutsRB;
    public MeshCollider[] _CutsMC;
    public float _Mass = 0.5f;
    public float _CutMass = 0.2f;
    private Rigidbody _Rigidbody;
    private SphereCollider _SphereCollider;
    void Start()
    {
        _CutsRB = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _CutsRB)
        {
            rb.isKinematic = true;
            rb.mass = _CutMass;
        }
        _CutsMC = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in _CutsMC)
            mc.isTrigger = true;
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.mass = _Mass;
        _SphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (GameController.Instance.gcFruitNinja._Katana != null)
        {
            _Lookat.LookAt(GameController.Instance.gcFruitNinja._Katana.transform, GameController.Instance.gcFruitNinja._Katana.transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, _Lookat.rotation, Time.deltaTime * 6);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<KatanaBlade>())
            ChopFruit();
    }

    public void ChopFruit()
    {
        foreach (Rigidbody rb in _CutsRB)
        {
            rb.isKinematic = false;
            rb.velocity = _Rigidbody.velocity;
            rb.AddForce(rb.transform.right * 40);
        }
        _Rigidbody.isKinematic = true;
        GameController.Instance.gcFruitNinja._Score += 1;
        Destroy(_SphereCollider);
        foreach (MeshCollider mc in _CutsMC)
            mc.isTrigger = false;
        Destroy(this);
    }
}
