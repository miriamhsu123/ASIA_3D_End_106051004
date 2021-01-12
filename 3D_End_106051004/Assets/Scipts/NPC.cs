using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC 資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialogue;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.2f;

    public enum NPCState
    {
        FirstDialogue, Missioning, Finish
    }

    [Header("NPC 狀態")]
    public NPCState state = NPCState.FirstDialogue;

    /// <summary>
    /// 玩家是否進入感應區
    /// </summary>
    private bool playerInArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "JunkoChan")
        {
            playerInArea = true;
            StartCoroutine(Dialogue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "JunkoChan")
        {
            playerInArea = false;
            StopDialogue();
        }
    }

    private void StopDialogue()
    {
        dialogue.SetActive(false);    // 關閉對話框
        StopAllCoroutines();        // 停止所有協程
    }

    /// <summary>
    /// 開始對話
    /// </summary>
    private IEnumerator Dialogue()
    {
        /**
        // print(data.dialogA);            // 取得字串全部資料
        // print(data.dialogA[0]);         // 取得字串局部資料：語法 [編號]

        // for 迴圈：重複處理相同程式
        //for (int i = 0; i < 10; i++)
        //{
        //    print("我是迴圈：" + i);
        //}

        //for (int apple = 1; apple < 100; apple++)
        //{
        //    print("迴圈：" + apple);
        //}
        */

        // 顯示對話框
        dialogue.SetActive(true);
        // 清空文字
        textContent.text = "";
        // 對話者名稱 指定為 此物件的名稱
        textName.text = name;

        // 要說的對話
        string dialogueString = data.dialogueB;

        // 判斷 NPC 狀態 來顯示對應的 對話內容
        switch (state)
        {
            case NPCState.FirstDialogue:
                dialogueString = data.dialogueA;
                break;
            case NPCState.Missioning:
                dialogueString = data.dialogueB;
                break;
            case NPCState.Finish:
                dialogueString = data.dialogueC;
                break;
        }

        // 字串的長度 dialogA.Length
        for (int i = 0; i < dialogueString.Length; i++)
        {
            // print(data.dialogA[i]);
            // 文字 串聯 
            textContent.text += dialogueString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }
}
