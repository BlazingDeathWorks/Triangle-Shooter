using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer instruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeout());
    }
    public IEnumerator fadeout() {
        for (float i = 1; i > 0; i -= 0.01f)
        {
            instruction.color = new Color(255, 255, 255, i);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
