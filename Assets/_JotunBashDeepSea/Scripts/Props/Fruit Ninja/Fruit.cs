using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Transform _Lookat;
    public Rigidbody[] _CutsRB;
    public MeshCollider[] _CutsMC;
    public GameObject particleExplosionPrefab;
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
            rb.interpolation = RigidbodyInterpolation.None;
        }
        _CutsMC = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in _CutsMC)
            mc.isTrigger = true;
        _SphereCollider = GetComponent<SphereCollider>();
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.mass = _Mass;
        _Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (GameController.Instance.gcFruitNinja._Katana.Count > 0)
        {
            _Lookat.LookAt(GameController.Instance.gcFruitNinja._Katana[0].transform, GameController.Instance.gcFruitNinja._Katana[0].transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, _Lookat.rotation, Time.fixedDeltaTime * 6);
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
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            //rb.velocity = _Rigidbody.velocity;
            rb.AddForce(rb.transform.right * 40);
        }
        GameController.Instance.gcNarrator.TellNow("FruitNinja");
        _Rigidbody.interpolation = RigidbodyInterpolation.None;
        _Rigidbody.isKinematic = true;
        GameController.Instance.gcFruitNinja._Score += 1;
        Destroy(_SphereCollider);
        foreach (MeshCollider mc in _CutsMC)
            mc.isTrigger = false;
        Destroy(this);
    }
}
