using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzles : MonoBehaviour
{
    public Button[] buttons; // Масив кнопок
    public TMP_Text[] buttonTexts; // Масив текстів кнопок
    public Color[] colorsToUse; // Масив кольорів для використання
    private int[] currentColorIndices; // Масив індексів поточного кольору для кожної кнопки

    // Масиви для зберігання початкових кольорів тексту для кожної кнопки
    private Color[] initialTextColors;

    void Start()
    {
        currentColorIndices = new int[buttons.Length];
        initialTextColors = new Color[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            currentColorIndices[i] = 0;
            initialTextColors[i] = buttonTexts[i].color;
        }

        // Налаштовуємо потрібний індекс для середньої кнопки (індекс -1, оскільки ми рахуємо з 0, аби не забути)
        int middleButtonIndex = buttons.Length / 2;
        currentColorIndices[middleButtonIndex] = 1; // 1 - другий індекс в масиві кольорів
    }

    public void ChangeButtonColor(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
        {
            return;
        }

        ToggleButtonColor(buttonIndex);

        // Перевіряємо, чи існують сусідні кнопки
        if (buttonIndex > 0)
        {
            ToggleButtonColor(buttonIndex - 1); // Змінюємо колір лівої сусідньої кнопки
        }
        if (buttonIndex < buttons.Length - 1)
        {
            ToggleButtonColor(buttonIndex + 1); // Змінюємо колір правої сусідньої кнопки
        }
    }

    private void ToggleButtonColor(int buttonIndex)
    {
        Color textColor = colorsToUse[currentColorIndices[buttonIndex]];
        buttonTexts[buttonIndex].color = textColor;

        ColorBlock cb = buttons[buttonIndex].colors;
        cb.normalColor = textColor;
        cb.highlightedColor = textColor;
        cb.pressedColor = textColor;
        cb.selectedColor = textColor;
        buttons[buttonIndex].colors = cb;

        currentColorIndices[buttonIndex] = (currentColorIndices[buttonIndex] + 1) % colorsToUse.Length;
    }

    public void RestoreButtonColors(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
        {
            return;
        }

        // Відновлення початкових кольорів тексту для вибраної кнопки
        buttonTexts[buttonIndex].color = initialTextColors[buttonIndex];

        // Відновлення початкових кольорів фону кнопки для стану normal звичайний
        ColorBlock cb = buttons[buttonIndex].colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = Color.white;
        cb.pressedColor = Color.white;
        cb.selectedColor = Color.white;
        buttons[buttonIndex].colors = cb;
    }
}
