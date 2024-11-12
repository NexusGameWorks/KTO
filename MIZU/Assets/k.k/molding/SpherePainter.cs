using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; // Listを使用するため

public class SpherePainter : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset; // InputActionAssetの参照
    [SerializeField] private GameObject spherePrefab; // 生成するスフィアのPrefab
    [SerializeField] private Transform targetTransform; // スフィアを置く対象オブジェクトのTransform
    [SerializeField] private float sphereSpawnInterval = 0.1f; // スフィア生成間隔（インスペクターで設定可能）
    [SerializeField] private Transform parentTransform; // 結合オブジェクトの親Transform


    private InputAction drawAction;
    private InputAction endAction;
    private float timer;
    private bool isDrawing;
    private List<GameObject> spheres = new List<GameObject>(); // 生成したスフィアのリスト

    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("mol");
        drawAction = actionMap.FindAction("draw");
        endAction = actionMap.FindAction("end");

        drawAction.performed += StartDrawing;
        drawAction.canceled += StopDrawing;
        endAction.performed += EndDrawing;

        drawAction.Enable();
        endAction.Enable();
    }

    private void OnDisable()
    {
        drawAction.performed -= StartDrawing;
        drawAction.canceled -= StopDrawing;
        endAction.performed -= EndDrawing;

        drawAction.Disable();
        endAction.Disable();
    }

    private void Update()
    {
        if (isDrawing)
        {
            timer += Time.deltaTime;
            if (timer >= sphereSpawnInterval) // インスペクターで設定された生成間隔を使用
            {
                PlaceSphere();
                timer = 0f; // タイマーをリセット
            }
        }
    }

    private void StartDrawing(InputAction.CallbackContext context)
    {
        isDrawing = true;
        Debug.Log("Drawing started"); // デバッグ用
    }

    private void StopDrawing(InputAction.CallbackContext context)
    {
        isDrawing = false;
        Debug.Log("Drawing stopped"); // デバッグ用
    }

    private void EndDrawing(InputAction.CallbackContext context)
    {
        Debug.Log("End key pressed."); // endキーが押されたことを確認
        CombineSpheres(); // スフィアを1つのメッシュに結合
    }

    private void PlaceSphere()
    {
        GameObject sphere = Instantiate(spherePrefab, targetTransform.position, Quaternion.identity);
        sphere.transform.parent = parentTransform;
        spheres.Add(sphere); // 生成したスフィアをリストに追加
    }

    private void CombineSpheres()
    {
        if (spheres.Count == 0) return;

        // 新しいメッシュオブジェクトを作成
        GameObject combinedMeshObject = new GameObject("CombinedSphere");
        combinedMeshObject.transform.parent = parentTransform; // 親オブジェクトを設定

        MeshFilter meshFilter = combinedMeshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();
        MeshCollider collider = combinedMeshObject.AddComponent<MeshCollider>();
        Rigidbody rb = combinedMeshObject.AddComponent<Rigidbody>();

        // スフィアのメッシュを結合する
        CombineInstance[] combine = new CombineInstance[spheres.Count];

        for (int i = 0; i < spheres.Count; i++)
        {
            MeshFilter sphereMeshFilter = spheres[i].GetComponent<MeshFilter>();
            if (sphereMeshFilter != null)
            {
                combine[i].mesh = sphereMeshFilter.sharedMesh;
                combine[i].transform = spheres[i].transform.localToWorldMatrix;
            }
        }

        // メッシュを結合して設定
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 // インデックスフォーマットをUInt32に設定
        };

        combinedMesh.CombineMeshes(combine);
        meshFilter.mesh = combinedMesh;

        // マテリアルを設定（sharedMaterialを使用）
        meshRenderer.sharedMaterial = spherePrefab.GetComponent<MeshRenderer>().sharedMaterial;

        // 元のスフィアを削除
        foreach (GameObject sphere in spheres)
        {
            Destroy(sphere);
        }
        spheres.Clear(); // リストをクリア
    }

}
