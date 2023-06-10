using SFML.Graphics;
using SFML.System;
using System;
using TCEngine;
using System.Xml.Linq;

#region 1
//MRS
#endregion

namespace TCGame
{
    public class HUDComponent : RenderComponent
    {
        private int m_Points = 0;

        private Font m_Font;
        private Text m_Text;
        private Text m_BlinkText;

        private string m_Label;

        public HUDComponent(string _label)
        {
            m_RenderLayer = ERenderLayer.HUD;

            m_Label = _label;

            m_Font = new Font("Data/Fonts/VT323.ttf");
            m_Text = new Text(m_Label, m_Font);
            m_BlinkText = new Text(m_Points.ToString(), m_Font);

            SetupTextProperties();
            UpdateText();
        }

        public HUDComponent(string _label, Font _font)
        {
            m_RenderLayer = ERenderLayer.HUD;

            m_Label = _label;

            m_Font = _font;
            m_Text = new Text(m_Label, m_Font);
            
            SetupTextProperties();
            UpdateText();
        }

        public override void Update(float _dt)
        {
            base.Update(_dt);
        }

        public override void Draw(RenderTarget _target, RenderStates _states)
        {
            base.Draw(_target, _states);

            _states.Transform *= Owner.GetWorldTransform();
            _target.Draw(m_Text, _states);
            _target.Draw(m_BlinkText, _states);
        }

        private void SetupTextProperties()
        {

            const uint characterSize = 40u;
            const float outlineThickness = 0.0f;
            const float pointsOffset = 50.0f;
            Color outlineColor = Color.Red;

            m_Text.CharacterSize = characterSize;
            m_Text.OutlineThickness = outlineThickness;
            m_Text.OutlineColor = outlineColor;

            m_BlinkText.CharacterSize = characterSize;
            m_BlinkText.OutlineThickness = outlineThickness;
            m_BlinkText.OutlineColor = outlineColor;

            m_BlinkText.Position = new Vector2f(pointsOffset + m_Text.GetLocalBounds().Width, 0.0f);
        }

        private void UpdateText()
        {
            m_Text.DisplayedString = String.Format("{0}:", m_Label);
            m_BlinkText.DisplayedString = String.Format("{0}", m_Points);
        }

        public void IncreasePoints()
        {
            ++m_Points;
            UpdateText();
        }

        public void ResetPoints()
        {
            m_Points = 0;
            UpdateText();
        }


        public override object Clone()
        {
            HUDComponent clonedComponent = new HUDComponent(m_Label, m_Font);
            return clonedComponent;
        }
    }
}
