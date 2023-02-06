using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TextToTMPNamespace.Instance02cb022ad41b423e838cac1499f2a20d;
namespace TextToTMPNamespace.Instance02cb022ad41b423e838cac1499f2a20d
{
	using UnityEngine;
	using TMPro;

	internal static class TextToTMPExtensions
	{
		public static void SetTMPAlignment( this TMP_Text tmp, TextAlignmentOptions alignment )
		{
			tmp.alignment = alignment;
		}

		public static void SetTMPAlignment( this TMP_Text tmp, TextAnchor alignment )
		{
			switch( alignment )
			{
				case TextAnchor.LowerLeft: tmp.alignment = TextAlignmentOptions.BottomLeft; break;
				case TextAnchor.LowerCenter: tmp.alignment = TextAlignmentOptions.Bottom; break;
				case TextAnchor.LowerRight: tmp.alignment = TextAlignmentOptions.BottomRight; break;
				case TextAnchor.MiddleLeft: tmp.alignment = TextAlignmentOptions.Left; break;
				case TextAnchor.MiddleCenter: tmp.alignment = TextAlignmentOptions.Center; break;
				case TextAnchor.MiddleRight: tmp.alignment = TextAlignmentOptions.Right; break;
				case TextAnchor.UpperLeft: tmp.alignment = TextAlignmentOptions.TopLeft; break;
				case TextAnchor.UpperCenter: tmp.alignment = TextAlignmentOptions.Top; break;
				case TextAnchor.UpperRight: tmp.alignment = TextAlignmentOptions.TopRight; break;
				default: tmp.alignment = TextAlignmentOptions.Center; break;
			}
		}

		public static void SetTMPFontStyle( this TMP_Text tmp, FontStyles fontStyle )
		{
			tmp.fontStyle = fontStyle;
		}

		public static void SetTMPFontStyle( this TMP_Text tmp, FontStyle fontStyle )
		{
			FontStyles fontStyles;
			switch( fontStyle )
			{
				case FontStyle.Bold: fontStyles = FontStyles.Bold; break;
				case FontStyle.Italic: fontStyles = FontStyles.Italic; break;
				case FontStyle.BoldAndItalic: fontStyles = FontStyles.Bold | FontStyles.Italic; break;
				default: fontStyles = FontStyles.Normal; break;
			}

			tmp.fontStyle = fontStyles;
		}

		public static void SetTMPHorizontalOverflow( this TMP_Text tmp, HorizontalWrapMode overflow )
		{
			tmp.enableWordWrapping = ( overflow == HorizontalWrapMode.Wrap );
		}

		public static HorizontalWrapMode GetTMPHorizontalOverflow( this TMP_Text tmp )
		{
			return tmp.enableWordWrapping ? HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
		}

		public static void SetTMPVerticalOverflow( this TMP_Text tmp, TextOverflowModes overflow )
		{
			tmp.overflowMode = overflow;
		}

		public static void SetTMPVerticalOverflow( this TMP_Text tmp, VerticalWrapMode overflow )
		{
			tmp.overflowMode = ( overflow == VerticalWrapMode.Overflow ) ? TextOverflowModes.Overflow : TextOverflowModes.Truncate;
		}

		public static void SetTMPLineSpacing( this TMP_Text tmp, float lineSpacing )
		{
			tmp.lineSpacing = ( lineSpacing - 1 ) * 100f;
		}

		public static void SetTMPCaretWidth( this TMP_InputField tmp, int caretWidth )
		{
			tmp.caretWidth = caretWidth;
		}

		public static void SetTMPCaretWidth( this TMP_InputField tmp, float caretWidth )
		{
			tmp.caretWidth = (int) caretWidth;
		}
	}
}


public class Projectile : MonoBehaviour
{
    float duration;
    Weapon.Alignment alignment;
    [HideInInspector]
    public int damageValue;

    private void Start()
    {
        StartCoroutine(Duration());
        this.tag = "Projectile";
    }

    void Update()
    {
        Vector2 direction = GetComponent<Rigidbody2D>().velocity;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public Projectile(float duration, Weapon.Alignment alignment, int damageValue)
    {
        this.duration = duration;
        this.damageValue = damageValue;
    }

    public void SetValues(float duration, Weapon.Alignment alignment, int damageValue)
    {
        this.duration = duration;
        this.damageValue = damageValue;
    }

    IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

	IEnumerator DurationFast()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
		else if (collision.CompareTag("Platform"))
		{
			StartCoroutine(DurationFast());
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
