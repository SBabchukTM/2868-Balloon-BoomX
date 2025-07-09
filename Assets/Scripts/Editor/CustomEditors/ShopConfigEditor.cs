using Runtime.Gameplay.BalloonSkinsShop;
using UnityEditor;
using UnityEngine;

namespace Editor.CustomEditors
{
    [CustomEditor(typeof(ShopConfig))]
    public class ShopConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var shopConfig = (ShopConfig)target;

            if (GUILayout.Button("Assign Item IDs based on list index."))
                AssignUniqueItemIDs(shopConfig);
        }

        private void AssignUniqueItemIDs(ShopConfig shopConfig)
        {
            if (shopConfig.ShopItems == null || shopConfig.ShopItems.Count == 0)
                return;

            var id = 0;

            foreach (var item in shopConfig.ShopItems)
            {
                var serializedItem = new SerializedObject(item);
                var itemIdProperty = serializedItem.FindProperty("_itemID");

                itemIdProperty.intValue = id;
                id++;

                serializedItem.ApplyModifiedProperties();
            }

            Debug.Log("Item IDs have been successfully updated.");
        }
    }
}