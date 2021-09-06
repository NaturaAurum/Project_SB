using System.Collections;
using System.Collections.Generic;
using SB.GameLogic.Character;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace SB
{
    [CustomEditor(typeof(CharacterPhysics))]
    public class CharacterPhysicsEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.LabelField((target as CharacterPhysics).CurrentState.Type.ToString());
        }
    }
}
