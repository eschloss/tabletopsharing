using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] puzzles;
	public GameObject decalPrefab;
    public GameObject table;
	public int currentPuzzle;
	public Vector2[] startingContainersMin;
	public Vector2[] startingContainersMax;
	public GameObject[] currentPieces;
	public float LEEWAY = .02f;
    
    // Start is called before the first frame update
    void Start()
    {
		currentPuzzle = 0;
		Puzzle puzzle = puzzles[currentPuzzle].GetComponent<Puzzle>();
		SetupNewPuzzle(puzzle);
    }
	
	void SetupNewPuzzle(Puzzle puzzle) {
		currentPieces = new GameObject[puzzle.length * puzzle.width];
		for (int x = 0; x < puzzle.length; x++) {
            for (int y = 0; y < puzzle.width; y++) {

                var puzzleObj = Instantiate(decalPrefab);
                puzzleObj.transform.parent = table.transform;
                SpriteRenderer mo_spriteRenderer = puzzleObj.GetComponent<SpriteRenderer>();
                mo_spriteRenderer.color = Color.white;
				mo_spriteRenderer.drawMode = SpriteDrawMode.Sliced;
				mo_spriteRenderer.sprite = puzzle.pieces[x*puzzle.length + y];
                puzzleObj.name = "puzzle_" + (x * puzzle.length + y);

				int containerInt =  Random.Range(0, startingContainersMin.Length);

            	puzzleObj.transform.localPosition = new Vector3(Random.Range(startingContainersMin[containerInt].x, startingContainersMax[containerInt].x),
                												Random.Range(startingContainersMin[containerInt].y, startingContainersMax[containerInt].y), -1); 
            	
				float pixelratio = (float) puzzle.pixelwidth / puzzle.pixelheight;
				float piecescale_x = 1f / (3f * puzzle.length);
				float tableratio = table.transform.localScale.x / table.transform.localScale.y;
				puzzleObj.transform.localScale = new Vector3(piecescale_x, piecescale_x * tableratio / pixelratio, 1); 

				currentPieces[x * puzzle.length + y] = puzzleObj;
			}
		}
	
	}

    // Update is called once per frame
    void Update()
    {
		if (currentPuzzle < puzzles.Length) {
            Puzzle puzzle = puzzles[currentPuzzle].GetComponent<Puzzle>();
            GameObject currentPiece = currentPieces[0];
            GameObject nextPiece;
            bool success = true;
            for (int x = 1; x < currentPieces.Length; x++) {
                    nextPiece = currentPieces[x];

                    var diff = nextPiece.transform.localPosition - currentPiece.transform.localPosition;
                    var correctDiffX = 0f;
                    var correctDiffY = 0f;
                    if (x % puzzle.length != 0) {
                        correctDiffX = currentPiece.transform.localScale.x;			
                        correctDiffY = 0f;
                    } else {
                        correctDiffX = -(puzzle.length - 1) * currentPiece.transform.localScale.x;			
                        correctDiffY = currentPiece.transform.localScale.y;
                    }
        
                    if (Mathf.Abs(diff.x - correctDiffX) > LEEWAY || Mathf.Abs(-diff.y - correctDiffY) > LEEWAY) {
                        success = false;
                        break;
                    }				

                    currentPiece = nextPiece;
            }
            if (success) {
                Debug.Log("Success");
                /* Move onto the next puzzle.
				 * TODO Log how many times a puzzle piece was shared btwn users
				 * TODO log the times between one user first touching a piece and the time the 
				 * next user first takes the piece.
				 */
				
				foreach(GameObject obj in currentPieces) {
                    if (obj.GetComponent<Linked>().obj != null) {
                    	Destroy(obj.GetComponent<Linked>().obj);
					}
					Destroy(obj);
				}

				currentPuzzle += 1;
                if (currentPuzzle < puzzles.Length) {
                    puzzle = puzzles[currentPuzzle].GetComponent<Puzzle>();
                    SetupNewPuzzle(puzzle);
				}
            }
		} else {
			/* Test is over */	
		}

    }
}
