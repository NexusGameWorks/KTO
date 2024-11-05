using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; // List���g�p���邽��

public class SpherePainter : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset; // InputActionAsset�̎Q��
    [SerializeField] private GameObject spherePrefab; // ��������X�t�B�A��Prefab
    [SerializeField] private Transform targetTransform; // �X�t�B�A��u���ΏۃI�u�W�F�N�g��Transform
    [SerializeField] private float sphereSpawnInterval = 0.1f; // �X�t�B�A�����Ԋu�i�C���X�y�N�^�[�Őݒ�\�j
    [SerializeField] private Transform parentTransform; // �����I�u�W�F�N�g�̐eTransform


    private InputAction drawAction;
    private InputAction endAction;
    private float timer;
    private bool isDrawing;
    private List<GameObject> spheres = new List<GameObject>(); // ���������X�t�B�A�̃��X�g

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
            if (timer >= sphereSpawnInterval) // �C���X�y�N�^�[�Őݒ肳�ꂽ�����Ԋu���g�p
            {
                PlaceSphere();
                timer = 0f; // �^�C�}�[�����Z�b�g
            }
        }
    }

    private void StartDrawing(InputAction.CallbackContext context)
    {
        isDrawing = true;
        Debug.Log("Drawing started"); // �f�o�b�O�p
    }

    private void StopDrawing(InputAction.CallbackContext context)
    {
        isDrawing = false;
        Debug.Log("Drawing stopped"); // �f�o�b�O�p
    }

    private void EndDrawing(InputAction.CallbackContext context)
    {
        Debug.Log("End key pressed."); // end�L�[�������ꂽ���Ƃ��m�F
        CombineSpheres(); // �X�t�B�A��1�̃��b�V���Ɍ���
    }

    private void PlaceSphere()
    {
        GameObject sphere = Instantiate(spherePrefab, targetTransform.position, Quaternion.identity);
        spheres.Add(sphere); // ���������X�t�B�A�����X�g�ɒǉ�
    }

    private void CombineSpheres()
    {
        if (spheres.Count == 0) return;

        // �V�������b�V���I�u�W�F�N�g���쐬
        GameObject combinedMeshObject = new GameObject("CombinedSphere");
        combinedMeshObject.transform.parent = parentTransform; // �e�I�u�W�F�N�g��ݒ�

        MeshFilter meshFilter = combinedMeshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedMeshObject.AddComponent<MeshRenderer>();

        // �X�t�B�A�̃��b�V������������
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

        // ���b�V�����������Đݒ�
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 // �C���f�b�N�X�t�H�[�}�b�g��UInt32�ɐݒ�
        };

        combinedMesh.CombineMeshes(combine);
        meshFilter.mesh = combinedMesh;

        // �}�e���A����ݒ�isharedMaterial���g�p�j
        meshRenderer.sharedMaterial = spherePrefab.GetComponent<MeshRenderer>().sharedMaterial;

        // ���̃X�t�B�A���폜
        foreach (GameObject sphere in spheres)
        {
            Destroy(sphere);
        }
        spheres.Clear(); // ���X�g���N���A
    }

}
