using UnityEngine;
using System.Collections;

public class JetEngine : MonoBehaviour {

    public float speed = 0.5f;

	// Use this for initialization
	void Start () {
        this.speed = 0.5f + Random.value;

        var meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);

        var trailRenderer = this.GetComponent<TrailRenderer>();
        trailRenderer.material.color = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update () {
        //現在の回転量を取得する
        var currentRotation = this.transform.localRotation;

        //上下左右の回転軸（左右矢印キーで動く）の値を取得する
        //Quarternionは掛け算をすると回転量が変わる
        var horizontalRotation = Quaternion.Inverse(Quaternion.AngleAxis(Input.GetAxis("Horizontal"), Vector3.forward));
        var verticalRotation = Quaternion.AngleAxis(Input.GetAxis("Vertical"), Vector3.right);

        var nextRotation = currentRotation * horizontalRotation * verticalRotation;

        //計算後の回転量を設定する
        this.transform.localRotation = nextRotation;

        //前進～
        this.transform.Translate(Vector3.forward * this.speed);
    }
}
