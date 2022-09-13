using UnityEngine;
using TMPro;
public class DoorMath : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] float amountFloat;
    [SerializeField] MathDoorParticleColorChanger colorChanger;
    public bool isUsed = false;
    [SerializeField] TMP_Text text;
    IDoDoorMath doMath;
    [SerializeField] DoMathType mathType;
    string tempString;
    DoubleDoorManager doorManager;

    public int Amount { get => (int)amountFloat; set => amountFloat = value; }
    public DoMathType MathType { get => mathType; set => mathType = value; }
    public float AmountFloat { get => amountFloat; set => amountFloat = value; }
    private void Awake()
    {
        AmountFloat = (float)amount;
        Init();

        doorManager = GetComponentInParent<DoubleDoorManager>();
    }

    public void Init()
    {
        if (text != null)
        {
            switch (MathType)
            {
                case DoMathType.Addition:
                    tempString = "+";
                    break;
                case DoMathType.Subtraction:
                    tempString = "-";
                    break;
                case DoMathType.Divisition:
                    tempString = "÷";
                    break;
                case DoMathType.Multiply:
                    tempString = "x";
                    break;
                default:
                    tempString = "";
                    break;
            }
            text.text = AmountFloat.ToString("0.0") + tempString;
            colorChanger.ChangeColor(MathType);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed && other.CompareTag("Player"))
        {
            ChooseProcess(other);
            if (doorManager != null)
                doorManager.DisableOtherDoor(this);
        }
    }

    private void ChooseProcess(Collider other)
    {
        doMath = other.GetComponent<IDoDoorMath>();

        if (doMath != null)
        {
            switch (MathType)
            {
                case DoMathType.Addition:
                    doMath.UseNewValue(Amount, MathType);
                    break;
                case DoMathType.Subtraction:
                    doMath.UseNewValue(Amount, MathType);
                    break;
                case DoMathType.Divisition:
                    doMath.UseNewValue(Amount, MathType);
                    break;
                case DoMathType.Multiply:
                    doMath.UseNewValue(Amount, MathType);
                    break;
                default:
                    break;
            }
            
            isUsed = true;
        }
    }

    public int Division(int value)
    {
        return Amount / value;
    }

    public int Subtraction(int value)
    {
        return Mathf.Clamp(Amount - value, 0, int.MaxValue);
    }

    public int Addition(int value)
    {
        return Amount + value;
    }

    public int Multiply(int value)
    {
        return Amount * value;
    }
}

public enum DoMathType
{
    Addition,
    Subtraction,
    Divisition,
    Multiply
}

public interface IDoDoorMath
{
    int GetValue { get; }
    void UseNewValue(int result, DoMathType type);
}
