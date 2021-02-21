using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public Gear[] gears;
    public Slot[] initialSlots;
    public Slot[] finalSlots;
    public Text nuggetText;

    private void Update()
    {
        CheckSpinning();
    }

    void CheckSpinning()
    {
        bool allFinal = true;
        foreach (var item in gears)
        {
            if (item.currentMode != Gear.Mode.FINAL_SLOT)
                allFinal = false;
        }

        if (!allFinal)
        {
            nuggetText.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM";
        }
        else
        {
            nuggetText.text = "YAY, PARABÉNS. TASK CONCLUÍDA!";
            foreach (var item in gears)
            {
                float spinDirection;
                if (item.selectedPin.counterclockwise) spinDirection = 1f;
                else spinDirection = -1f;
                item.transform.Rotate(0, 0, spinDirection * 20 * Time.deltaTime);
            }
        }
    }

    public void ResetPos()
    {
        for (int i = 0; i < gears.Length; i++)
        {
            gears[i].selectedPin = initialSlots[i];
            gears[i].Slot(initialSlots[i]);
            finalSlots[i].occupied = false;
        }
    }
}
