using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchObjects();
        }
    }

    private void SwitchObjects()
    {
        if (objectToActivate != null && objectToDeactivate != null)
        {
            bool isActive = objectToActivate.activeSelf;
            objectToActivate.SetActive(!isActive);
            objectToDeactivate.SetActive(isActive);
            Debug.Log("������������ �������� ���������: " + objectToActivate.name + " �������: " + !isActive);
        }
        else
        {
            Debug.LogError("����������, ���������, ��� ��� ������� ������������ � ����������.");
        }
    }
}