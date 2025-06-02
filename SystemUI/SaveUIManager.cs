using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button returnButton;
    public Button NewGameButton;
    public Button CancelButton;
    [Header("Scroll Control")]
    public GameObject ConfirmPanel;
    public Button ConfirmButton;
    public GameObject SaveSlotPrefab;
    public GameObject target;
    private Dictionary<string, DateTime> saveData;
    private int mid_position;
    void Start()
    {
        InitialSaveData();
        returnButton.onClick.AddListener(() =>
        { SuperController.Instance.CloseSystem(true); });
        ConfirmButton.onClick.AddListener(() =>
        {
            OnSelectedConfirm();
            ConfirmButton.interactable = false;
        });
        NewGameButton.onClick.AddListener(() =>
        {
            SlotSelected(-1);
        });
        CancelButton.onClick.AddListener(() =>
        {
            ConfirmPanel.SetActive(false);
        });
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SuperController.Instance.CloseSystem(true);
        }
    }
    void OnEnable()
    {
        ConfirmButton.interactable = true;
        CancelButton.interactable = true;
        ConfirmPanel.SetActive(false);
        InitialSaveData();
    }

    private void InitialSaveData()
    {
        saveData = FindObjectOfType<SuperController>().QueryData();
        int i = 0;
        foreach (var item in saveData)
        {
            var cur_slot = Instantiate(SaveSlotPrefab, target.transform);
            cur_slot.GetComponentInChildren<Text>().text = item.Value.ToString();

            int slotIndex = i;

            if (saveData == null || saveData.Count == 0)
            {
                Debug.LogWarning("No save data found.");
                return;
            }

            cur_slot.GetComponent<Button>().onClick.AddListener(() =>
            {
                SlotSelected(slotIndex); // 使用副本
            });
            i++;
        }
    }

    private void SlotSelected(int slotPosition)
    {
        ConfirmPanel.SetActive(true);
        ConfirmButton.interactable = true;
        CancelButton.interactable = true;
        mid_position = slotPosition;
    }
    public void OnSelectedConfirm()
    {
        SuperController superController = SuperController.Instance;
        if (superController != null)
        {
            superController.LoadPlayerData(mid_position);
            superController.LoadNewScene("HomeScene", 2.0f);
        }
    }
}
