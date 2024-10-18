using UnityEngine;

public class KK_PlayerModelSwitcher : MonoBehaviour
{
    public GameObject liquidModel;   // �t�̏�Ԃ̃��f��
    public GameObject gasModel;      // �C�̏�Ԃ̃��f��
    public GameObject solidModel;    // �ő̏�Ԃ̃��f��
    public GameObject slimeModel;    // �X���C����Ԃ̃��f��

    private GameObject currentModel; // ���ݕ\�����Ă��郂�f��

    void Start()
    {
        // ������Ԃ��t�̃��f���ɐݒ�
        SwitchToModel(liquidModel);
    }

    public void SwitchToModel(GameObject newModel)
    {
        Debug.Log("Switching model to: " + newModel.name);

        // ���݂̃��f��������Δ�A�N�e�B�u�ɂ���
        if (currentModel != null)
        {
            currentModel.SetActive(false);
            Debug.Log("Previous model deactivated: " + currentModel.name);
        }

        // �V�������f����ݒ肵�ăA�N�e�B�u�ɂ���
        currentModel = newModel;
        currentModel.SetActive(true);
        Debug.Log("New model activated: " + currentModel.name);
    }


}
