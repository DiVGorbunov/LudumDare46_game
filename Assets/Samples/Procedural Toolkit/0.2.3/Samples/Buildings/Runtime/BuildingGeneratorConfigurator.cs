using System.Collections.Generic;
using ProceduralToolkit.Buildings;
using ProceduralToolkit.Samples.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProceduralToolkit.Samples.Buildings
{
    /// <summary>
    /// Configurator for BuildingGenerator with UI and editor controls
    /// </summary>
    public class BuildingGeneratorConfigurator : ConfiguratorBase
    {
        public GameObject building;
        public MeshFilter platformMeshFilter;
        // public RectTransform leftPanel;
        public bool constantSeed = false;
        [FormerlySerializedAs("facadePlanningStrategy")]
        public FacadePlanner facadePlanner;
        [FormerlySerializedAs("facadeConstructionStrategy")]
        public FacadeConstructor facadeConstructor;
        [FormerlySerializedAs("roofPlanningStrategy")]
        public RoofPlanner roofPlanner;
        [FormerlySerializedAs("roofConstructionStrategy")]
        public RoofConstructor roofConstructor;
        public List<PolygonAsset> foundationPolygons = new List<PolygonAsset>();
        public BuildingGenerator.Config config = new BuildingGenerator.Config();

        private const int minFloorCount = 1;
        private const int maxFloorCount = 15;
        private static readonly RoofType[] roofTypes = new RoofType[]
        {
            RoofType.Flat,
            RoofType.Hipped,
            RoofType.Gabled,
        };

        private const float platformHeight = 0.5f;
        private const float platformRadiusOffset = 2;

        private BuildingGenerator generator;
        private Mesh platformMesh;
        private int currentPolygon = 0;
        private int currentRoofType = 1;

        private void Awake()
        {
            // Generate();
            SetupSkyboxAndPalette();
        }

        private void Update()
        {
            UpdateSkybox();
        }

        public void Generate(bool randomizeConfig = true)
        {
            if (constantSeed)
            {
                Random.InitState(0);
            }

            if (randomizeConfig)
            {
                GeneratePalette();
                config.palette.wallColor = GetMainColorHSV().ToColor();
            }

            if (generator == null)
            {
                generator = new BuildingGenerator();
            }

            if (building != null)
            {
                // foreach (Transform child in transform)
                // {                    
                //     if (Application.isPlaying)
                //     {
                //         Destroy(building);
                //     }
                //     else
                //     {
                //         DestroyImmediate(building);
                //     }
                // }
                if (Application.isPlaying)
                {
                    Destroy(building);
                }
                else
                {
                    DestroyImmediate(building);
                }
            }
            generator.SetFacadePlanner(facadePlanner);
            generator.SetFacadeConstructor(facadeConstructor);
            generator.SetRoofPlanner(roofPlanner);
            generator.SetRoofConstructor(roofConstructor);
            var foundationPolygon = foundationPolygons[currentPolygon];
            config.roofConfig.type = roofTypes[currentRoofType];
            // building.
            // building = GameObject.Instantiate(this.transform);
            building = new GameObject("Building");
            building.transform.SetParent(this.transform);
            building = generator.Generate(foundationPolygon.vertices, config, building.transform).gameObject;
            
            var rect = Geometry.GetRect(foundationPolygon.vertices);
            float platformRadius = Geometry.GetCircumradius(rect) + platformRadiusOffset;
            var platformDraft = Platform(platformRadius, platformHeight);
            AssignDraftToMeshFilter(platformDraft, platformMeshFilter, ref platformMesh);
        }
    }
}
