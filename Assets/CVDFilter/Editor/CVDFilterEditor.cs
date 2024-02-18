using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace SOG.CVDFilter
{
    [CustomEditor(typeof(CVDFilter))]
    public class CVDFilterEditor : Editor
    {
        VisualElement root;
        VisualTreeAsset visualTree;
        StyleSheet styleSheet;

        CVDFilter cvdFilter;

        EnumField visionTypeEnum;
        VisualElement previewImage;
        Label descriptionLabel;

        public void OnEnable()
        {
            cvdFilter = (CVDFilter)target;

            // Each editor window contains a root VisualElement object
            root = new VisualElement();

            // Import UXML
            visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/CVDFilter/Editor/CVDFilterEditor.uxml");
            //root.Add(visualTree.Instantiate());

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/CVDFilter/Editor/CVDFilterEditor.uss");
            root.styleSheets.Add(styleSheet);
        }

        public override VisualElement CreateInspectorGUI()
        {
            root.Clear();
            visualTree.CloneTree(root);

            visionTypeEnum = root.Q<EnumField>("VisionTypeEnum");
            previewImage = root.Q<VisualElement>("PreviewImage");
            descriptionLabel = root.Q<Label>("DescriptionLabel");

            visionTypeEnum.Init(VisionTypeNames.Normal);
            visionTypeEnum.BindProperty(serializedObject.FindProperty("currentType"));
            

            visionTypeEnum.RegisterValueChangedCallback((e) =>
            {
                cvdFilter.ChangeProfile();
                previewImage.style.backgroundImage = cvdFilter.SelectedVisionType.previewImage;
                descriptionLabel.text = cvdFilter.SelectedVisionType.description;
            });

            return root;
        }
    }
}
