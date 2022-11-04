using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public GameObject squarePrefab;
    private string[] board = new string[]{"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", ""};
    private int freespace = 15;

    private string[] squareNames = new string[]{"square_1", "square_2", "square_3", "square_4", "square_5", "square_6", "square_7", "square_8", "square_9", "square_10", "square_11", "square_12", "square_13", "square_14", "square_15", "square_blank"};

    // Start is called before the first frame update
    void Start() {
        squarePrefab.GetComponent<SpriteRenderer>().color = new Color(256, 256, 256);
        CreateBoard();
        ScrambleBoard();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("up")) MoveUp();
        if (Input.GetKeyDown("down")) MoveDown();
        if (Input.GetKeyDown("left")) MoveLeft();
        if (Input.GetKeyDown("right")) MoveRight();
        UpdateSquares();
    }

    void UpdateSquares() {
        for (int i = 0; i < 16; ++i) {
            // Update the text of the square.
            GameObject sq = GameObject.Find(squareNames[i]);
            sq.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(board[i]);

            // Update the color of the square. Make sure the blank one is the background color, the others are white.
            if (board[i] == "") {
                sq.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            } else {
                sq.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
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
                sq.name = squareNames[num];
                num++;
            }
        }
    }

    bool CanMoveRight() {
        return freespace > 0 && freespace % 4 != 0;
    }

    bool CanMoveLeft() {
        return freespace < 15 && freespace % 4 != 3;
    }

    bool CanMoveDown() {
        return freespace > 3;
    }

    bool CanMoveUp() {
        return freespace <= 11;
    }

    void MoveRight() {
        if (CanMoveRight()) {
            string tmp = board[freespace - 1];
            board[freespace - 1] = board[freespace];
            board[freespace] = tmp;
            --freespace;
        }
    }

    void MoveLeft() {
        if (CanMoveLeft()) {
            string tmp = board[freespace + 1];
            board[freespace + 1] = board[freespace];
            board[freespace] = tmp;
            ++freespace;
        }
    }

    void MoveDown() {
        if (CanMoveDown()) {
            string tmp = board[freespace - 4];
            board[freespace - 4] = board[freespace];
            board[freespace] = tmp;
            freespace -= 4;
        }
    }

    void MoveUp() {
        if (CanMoveUp()) {
            string tmp = board[freespace + 4];
            board[freespace + 4] = board[freespace];
            board[freespace] = tmp;
            freespace += 4;
        }
    }

    void ScrambleBoard() {
        for (int i = 0; i < 1000; ++i) {
            int n = Random.Range(0, 3);
            int x = Random.Range(0, 3);
            for (int j = 0; j < x; ++j) {
                if (n == 0 && CanMoveLeft()) MoveLeft();
                if (n == 1 && CanMoveRight()) MoveRight();
                if (n == 2 && CanMoveDown()) MoveDown();
                if (n == 3 && CanMoveUp()) MoveUp();
            }
        }
    }
}
