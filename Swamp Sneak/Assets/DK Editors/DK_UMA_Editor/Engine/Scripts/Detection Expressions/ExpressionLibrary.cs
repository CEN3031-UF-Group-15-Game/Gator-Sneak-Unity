using UnityEngine;
using System.Collections.Generic;
using System;

public class ExpressionLibrary : MonoBehaviour {
	public DK_ExpressionData[] ExpressionList = new DK_ExpressionData[0];
	public Dictionary<string, DK_ExpressionData> ExpressionDictionary = new Dictionary<string, DK_ExpressionData>();

    public void Awake(){
        UpdateDictionary();
    }

    public void UpdateDictionary(){
        ExpressionDictionary.Clear();
        for (int i = 0; i < ExpressionList.Length; i++){
            if (ExpressionList[i]){
                if (!ExpressionDictionary.ContainsKey(ExpressionList[i].Name)){
                    ExpressionDictionary.Add(ExpressionList[i].Name, ExpressionList[i]);
                }
            }
        }
    }

	public void AddExpression(DK_ExpressionData Expression)
    {
        for (int i = 0; i < ExpressionList.Length; i++)
        {
            if (ExpressionList[i].Name == Expression.Name)
            {
                ExpressionList[i] = Expression;
                return;
            }
        }
		var list = new DK_ExpressionData[ExpressionList.Length + 1];
        Array.Copy(ExpressionList, list, ExpressionList.Length );
        list[ExpressionList.Length] = Expression;
        ExpressionList = list;
        ExpressionDictionary.Add(Expression.Name, Expression);
    }

	internal DK_ExpressionData GetExpression(string ExpressionName)
    {
        return ExpressionDictionary[ExpressionName];
    }
}
