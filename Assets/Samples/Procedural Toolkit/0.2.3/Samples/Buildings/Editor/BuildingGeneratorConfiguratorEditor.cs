using UnityEngine;
using UnityEditor;

namespace ProceduralToolkit.Samples.Buildings
{
    [CustomEditor(typeof(BuildingGeneratorConfigurator))]
    public class BuildingGeneratorConfiguratorEditor : Editor
    {
        private BuildingGeneratorConfigurator generator;

        private void OnEnable()
        {
            generator = (BuildingGeneratorConfigurator) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            if (GUILayout.Button("Generate building"))
            {
                if(generator.building != null){
                    Undo.RecordObjects(new Object[]
                    {
                        generator,
                        generator.building,
                        generator.platformMeshFilter,
                    }, "Generate building");
                } else {
                    Undo.RecordObjects(new Object[]
                    {
                        generator,
                        generator.platformMeshFilter,
                    }, "Generate building");

                }

                
                generator.Generate(randomizeConfig: false);
            }
        }
    }
}
