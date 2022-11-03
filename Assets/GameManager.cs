using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public GameObject squarePrefab;
    private string[] board = new string[]{"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", ""};
    private int freespace = 15;

    // Start is called before the first frame update
    void Start() {
        squarePrefab.GetComponent<SpriteRenderer>().color = new Color(256, 256, 256);
        CreateBoard();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("up")) MoveUp();
        if (Input.GetKeyDown("down")) MoveDown();
        if (Input.GetKeyDown("left")) MoveLeft();
        if (Input.GetKeyDown("right")) MoveRight();
        for (int i = 0; i < 16; ++i) {
            GameObject sq = GameObject.Find("square_" + board[i]);
            sq.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = board[i];
            print("hello?");
        }
    }

    void CreateBoard() {
        float x = -3.8F;
        float y = 3.8F;
        int num = 0;
        for (int i = 0; i < 4; ++i) {
            for (int j = 0; j < 4; ++j) {
                TextMeshProUGUI label = squarePrefab.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                label.text = board[num];

                // Set color of empty square to background color
                if (board[num] == "") {
                    squarePrefab.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                }

                GameObject sq = Instantiate(squarePrefab, new Vector3(x + j * 2.5F, y - i * 2.5F, -1F), Quaternion.identity);
                sq.name = "square_" + board[num];
                num++;
            }
        }
    }

    void MoveRight() {
        // string tmp = board[freespace - 1];
        // board[freespace - 1] = board[freespace];
        // board[freespace] = tmp;
        // --freespace;
        board[15] = "15";
        board[14] = "";
    }

    void MoveLeft() {
    }

    void MoveUp() {
    }

    void MoveDown() {
    }
}
