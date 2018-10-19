using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardMech
{
    int GetValue();
    void SetValue(int value);
    string ToolTipText();
}
