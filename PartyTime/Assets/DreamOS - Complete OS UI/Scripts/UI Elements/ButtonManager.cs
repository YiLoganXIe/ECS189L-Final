using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Michsky.DreamOS
{
    public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        // Content
        public string buttonText = "Button";
        public Sprite buttonIcon;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        Button buttonVar;

        // Resources
        public TextMeshProUGUI normalText;
        public TextMeshProUGUI highlightedText;
        public Image normalIcon;
        public Image highlightedIcon;
        public AudioSource soundSource;
        public GameObject rippleParent;

        // Settings
        public bool useCustomContent = false;
        public bool enableIcon = false;
        public bool enableButtonSounds = false;
        public bool useHoverSound = true;
        public bool useClickSound = true;
        public bool useRipple = true;

        // Ripple
        public Sprite rippleShape;
        [Range(0.1f, 5)] public float speed = 1f;
        [Range(0.5f, 25)] public float maxSize = 4f;
        public Color startColor = new Color(1f, 1f, 1f, 1f);
        public Color transitionColor = new Color(1f, 1f, 1f, 1f);
        public bool renderOnTop = false;
        public bool centered = false;

        bool isPointerOn;

        void Awake()
        {
            maxSize = Mathf.Clamp(maxSize, 0.5f, 1000f);
        }

        void OnEnable()
        {
            // If custom content is disabled, then update the values
            if (useCustomContent == false)
                UpdateUI();
        }

        void Start()
        {
            if (enableButtonSounds == true)
            {
                try { buttonVar = gameObject.GetComponent<Button>(); }

                catch
                {
                    gameObject.AddComponent<Button>();
                    buttonVar = gameObject.GetComponent<Button>();
                }

                if (useClickSound == true)
                    buttonVar.onClick.AddListener(delegate { soundSource.PlayOneShot(clickSound); });
            }

            // If ripple is disabled, Destroy it for optimization
            if (useRipple == true && rippleParent != null)
                rippleParent.SetActive(false);
            else if (useRipple == false && rippleParent != null)
                Destroy(rippleParent);
        }

        public void UpdateUI()
        {
            // Update the values when this function is called
            normalText.text = buttonText;
            highlightedText.text = buttonText;

            if (enableIcon == true)
            {
                normalIcon.sprite = buttonIcon;
                highlightedIcon.sprite = buttonIcon;
            }
        }

        public void CreateRipple(Vector2 pos)
        {
            // If Ripple Parent is assigned, create the object and get the necessary components
            if (rippleParent != null)
            {
                GameObject rippleObj = new GameObject();
                rippleObj.AddComponent<Image>();
                rippleObj.GetComponent<Image>().sprite = rippleShape;
                rippleObj.name = "Ripple";
                rippleParent.SetActive(true);
                rippleObj.transform.SetParent(rippleParent.transform);

                if (renderOnTop == true)
                    rippleParent.transform.SetAsLastSibling();
                else
                    rippleParent.transform.SetAsFirstSibling();

                if (centered == true)
                    rippleObj.transform.localPosition = new Vector2(0f, 0f);
                else
                    rippleObj.transform.position = pos;

                rippleObj.AddComponent<Ripple>();
                Ripple tempRipple = rippleObj.GetComponent<Ripple>();
                tempRipple.speed = speed;
                tempRipple.maxSize = maxSize;
                tempRipple.startColor = startColor;
                tempRipple.transitionColor = transitionColor;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (useRipple == true && isPointerOn == true)
                CreateRipple(Input.mousePosition);
            else if (useRipple == false)
                this.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Process On Pointer Enter events
            if (enableButtonSounds == true && useHoverSound == true && buttonVar.interactable == true)
                soundSource.PlayOneShot(hoverSound);

            isPointerOn = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Process On Pointer Exit events
            isPointerOn = false;
        }
    }
}