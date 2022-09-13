using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScaleAllConsecutivly : MonoBehaviour
{
    [SerializeField] List<DoPunchScale> punchScaleList = new List<DoPunchScale>();
    [SerializeField] int order = 0;
    [SerializeField] float delay = 1f;
    // Start is called before the first frame update

    int index;
    private IEnumerator ConsecutivePunch()
    {
        index = 0;
        while (index < punchScaleList.Count)
        {
            if (index == 0)
                yield return new WaitForSeconds(order);

            punchScaleList[index].PunchIt();

            index++;

            if (index >= punchScaleList.Count)
                StopCoroutine(ConsecutivePunch());

            yield return new WaitForSeconds(delay);
        }
    }
    
    public void PunchItAll()
    {
        StartCoroutine(ConsecutivePunch());
    }
}
