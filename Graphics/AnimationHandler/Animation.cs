﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SpriteSheetRuntime;
using Illumination.Graphics;
using Illumination.Components;

namespace Illumination.Graphics.AnimationHandler
{
    public class Animation
    {
        public enum ImageType
        {
            Texture,
            Sheet,
            Component
        }

        public enum EaseType
        {
            In,
            Out,
            InAndOut,
            None
        }

        struct EventFrame
        {
            public FrameEvent customEvent;
            public bool isTriggered;
            public EventFrame(FrameEvent customEvent)
            {
                this.customEvent = customEvent;
                isTriggered = false;
            }
        };

        ImageType image;
        
        /* Image */
        Texture2D texture;
        SpriteSheet spriteSheet;
        Component component;

        AnimationSequence<Dimension> sizeSequence;
        AnimationSequence<Point> positionSequence;
        AnimationSequence<float> angleSequence;
        AnimationSequence<Color> colorSequence;
        AnimationSequence<EventFrame> eventSequence;

        /* Time */
        double elapsedTotalSec = 0;
        double animationDuration;
        double spriteFrameDuration; // only for multiple images

        public double ElapsedTotalSec
        {
            get { return elapsedTotalSec; }
            set { elapsedTotalSec = value; }
        }
        
        Vector2 relativeOrigin = new Vector2(0, 0);

        bool stopped = false;

        float layerDepth = 0;
        public float LayerDepth {
            get { return layerDepth; }
            set { 
                layerDepth = value;
                if (image == ImageType.Component) {
                    component.LayerDepth = layerDepth;
                }
            }
        }

        public Animation(Texture2D texture, Point position, Dimension size, double durationInSec)
        {
            image = ImageType.Texture;
            this.texture = texture;
            animationDuration = durationInSec;

            Initialize(position, size);
        }

        public Animation(SpriteSheet spriteSheet, Point position, Dimension size, double durationInSec, double spriteFrameDurationInSec)
        {
            image = ImageType.Sheet;
            this.spriteSheet = spriteSheet;
            animationDuration = durationInSec;
            spriteFrameDuration = spriteFrameDurationInSec;

            Initialize(position, size);
        }

        public Animation(Component component, double durationInSec)
        {
            image = ImageType.Component;
            this.component = component;
            animationDuration = durationInSec;

            Initialize(new Point(component.BoundingBox.X, component.BoundingBox.Y),
                new Dimension(component.BoundingBox.Width, component.BoundingBox.Height));
        }

        void Initialize(Point position, Dimension size)
        {
            sizeSequence = new AnimationSequence<Dimension>(size, InterpolateSize);
            positionSequence = new AnimationSequence<Point>(position, InterpolatePosition);
            angleSequence = new AnimationSequence<float>(0, InterpolateAngle);
            colorSequence = new AnimationSequence<Color>(new Color(255, 255, 255, 255), InterpolateColor);

            EventFrame initEventFrame = new EventFrame(nullEvent);
            eventSequence = new AnimationSequence<EventFrame>(initEventFrame, InterpolateEvent);
        }

        ///  <summary>
        ///  Sets the relative origin of the image that will be syncronized with the position on the map.
        ///  It is also the origin of rotation. Default is (0, 0).
        ///  </summary>
        public void SetRelativeOrigin(Vector2 origin)
        {
            relativeOrigin = origin;
        }

        public void SetAnimationDuration(double animationDuration)
        {
            this.animationDuration = animationDuration;
        }

        public void AddExtensionFrame(Dimension size, double targetTime)
        {
            sizeSequence.AddFrame(size, targetTime, EaseType.None);
        }

        public void AddExtensionFrame(Dimension size, double targetTime, EaseType easeType)
        {
            sizeSequence.AddFrame(size, targetTime, easeType);
        }

        public void AddTranslationFrame(Point position, double targetTime)
        {
            positionSequence.AddFrame(position, targetTime, EaseType.None);
        }

        public void AddTranslationFrame(Point position, double targetTime, EaseType easeType)
        {
            positionSequence.AddFrame(position, targetTime, easeType);
        }

        public void AddRotationFrame(float angle, double targetTime)
        {
            angleSequence.AddFrame(angle, targetTime, EaseType.None);
        }

        public void AddRotationFrame(float angle, double targetTime, EaseType easeType)
        {
            angleSequence.AddFrame(angle, targetTime, easeType);
        }

        public void AddColorFrame(Color color, double targetTime)
        {
            colorSequence.AddFrame(color, targetTime, EaseType.None);
        }

        public void AddColorFrame(Color color, double targetTime, EaseType easeType)
        {
            colorSequence.AddFrame(color, targetTime, easeType);
        }

        public void AddEventFrame(FrameEvent frameEvent, double targetTime)
        {
            EventFrame eventFrame = new EventFrame(frameEvent);
            eventSequence.AddFrame(eventFrame, targetTime, EaseType.None);
        }

        public bool Update(SpriteBatchRelative spriteBatch, GameTime gameTime)
        {
            if (stopped) {
                return false;
            }

            Dimension size = sizeSequence.InterpolateFrame(elapsedTotalSec);
            Point position = positionSequence.InterpolateFrame(elapsedTotalSec);
            float angle = angleSequence.InterpolateFrame(elapsedTotalSec);
            Color color = colorSequence.InterpolateFrame(elapsedTotalSec);

            Rectangle boundingBox = new Rectangle(position.X, position.Y, size.Width, size.Height);

            if (image == ImageType.Texture)
            {
                spriteBatch.DrawRelative(texture, boundingBox, null, color, angle, relativeOrigin, SpriteEffects.None, layerDepth);
            }
            else if (image == ImageType.Sheet)
            {
                int index = (int)(elapsedTotalSec / spriteFrameDuration) % spriteSheet.Count; 
                spriteBatch.DrawRelative(spriteSheet.Texture, boundingBox, spriteSheet.SourceRectangle(index), color, angle, relativeOrigin, SpriteEffects.None, layerDepth);
            }
            else if (image == ImageType.Component)
            {
                component.BoundingBox = boundingBox;
                component.Update();
                component.Draw(spriteBatch, true);
            }
            
            elapsedTotalSec += gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            /* Events will be triggered one frame earlier. */
            if (!eventSequence.Empty())
            {
                EventFrame eventFrame = eventSequence.InterpolateFrame(elapsedTotalSec);
                if (!eventFrame.customEvent.IsTriggered())
                {
                    eventFrame.customEvent.DoEvent(this);
                    eventFrame.customEvent.MarkTriggered();
                }
            }

            return elapsedTotalSec <= animationDuration;
        }

        public void StopAnimation() {
            stopped = true;
        }

        static Dimension InterpolateSize(Dimension size1, Dimension size2, double fraction)
        {
            Dimension newSize = size1;
            newSize.Width += (int)((size2.Width - size1.Width) * fraction);
            newSize.Height += (int)((size2.Height - size1.Height) * fraction);
            return newSize;
        }

        static Point InterpolatePosition(Point point1, Point point2, double fraction)
        {
            Point newPoint = point1;
            newPoint.X += (int)((point2.X - point1.X) * fraction);
            newPoint.Y += (int)((point2.Y - point1.Y) * fraction);
            return newPoint;
        }

        static float InterpolateAngle(float angle1, float angle2, double fraction)
        {
            float newAngle = angle1;
            newAngle += (float)((angle2 - angle1) * fraction);
            return newAngle;
        }

        static Color InterpolateColor(Color color1, Color color2, double fraction)
        {
            Color newColor = color1;
            newColor.R += (byte)((color2.R - color1.R) * fraction);
            newColor.G += (byte)((color2.G - color1.G) * fraction);
            newColor.B += (byte)((color2.B - color1.B) * fraction);
            newColor.A += (byte)((color2.A - color1.A) * fraction);
            return newColor;
        }

        static EventFrame InterpolateEvent(EventFrame event1, EventFrame event2, double fraction)
        {
            return event1;
        }

        public class NullEvent : FrameEvent
        {
            public override void DoEvent(Animation animation)
            {
                /* DOES NOTHING */
            }

            public override bool IsTriggered()
            {
                return true;
            }

            public override void MarkTriggered()
            {
                /* DOES NOTHING */
            }
        }

        static NullEvent nullEvent = new NullEvent();
    }
}
