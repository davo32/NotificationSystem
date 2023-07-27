using NotificationSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationGeneratorMulti : MonoBehaviour
{
    [SerializeField] NotificationContainer NotifyContainer;

    [SerializeField] private List<TMP_InputField> Titles = new List<TMP_InputField>();
    [SerializeField] private List<TMP_InputField> Infos = new List<TMP_InputField>();
    [SerializeField] private List<TMP_Dropdown> Types = new List<TMP_Dropdown>();

    [SerializeField] private Button GenerateBtn;
    [SerializeField] private Button ClearBtn;
    readonly List<string> m_options = new List<string> { "None", "Quest", "Friend Request", "LevelProgression", "New Message" };
    int SubmitCount = 0;

    bool CheckFields(int i)
    {
        string _title = Titles[i].text;
        string _info = Infos[i].text;

        return (string.IsNullOrWhiteSpace(_title)) && (string.IsNullOrWhiteSpace(_info) && (Types[i].value == 0));
    }

    void ClearFields(int i)
    {
        Titles[i].text = "";
        Infos[i].text = "";
        Types[i].value = 0;
        Types[i].ClearOptions();
        Types[i].AddOptions(m_options);
    }

    void SendGeneration(int i)
    {
        NotificationEnum typeData = NotificationEnum.None;

        switch (Types[i].value)
        {
            case 0:
                {
                    Debug.Log("[ERROR] - Empty Type");
                    break;
                }
            case 1:
                {
                    typeData = NotificationEnum.Quest;
                    break;
                }
            case 2:
                {
                    typeData = NotificationEnum.FriendsRequest;
                    break;
                }
            case 3:
                {
                    typeData = NotificationEnum.LevelProgression;
                    break;
                }
            case 4:
                {
                    typeData = NotificationEnum.NewMessage;
                    break;
                }
        }

        if (CheckFields(i))
        {
            Debug.Log("[ERROR] - All Fields were not filled out!");
            ClearFields(i);
        }
        else if (!CheckFields(i))
        {
            NotifyContainer.CreateNotificationData(Titles[i].text, Infos[i].text, typeData);
            ClearFields(i);
        }
    }

    void RunBatch()
    {
        for (int i = 0; i < Titles.Count; i++)
        {
            SendGeneration(i);
        }
    }

    void RunPurge()
    {
        for (int i = 0; i < Titles.Count; i++)
        {
            ClearFields(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBtn.onClick.AddListener(RunBatch);
        ClearBtn.onClick.AddListener(RunPurge);

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Titles.Count; i++)
        {
            if (CheckFields(i))
            {
                GenerateBtn.interactable = false;
                break;
            }
            else
            {
                GenerateBtn.interactable = true;
                break;
            }
        }
    }
}
