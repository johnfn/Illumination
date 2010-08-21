using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Graphics.AnimationHandler
{
    public class AnimationSequence<T>
    {
        public delegate T Interpolator(T frame1, T frame2, double fraction);

        static Dictionary<int, double> easeEffectTable = new Dictionary<int, double>();
        const int easeEffectDiscreteDomain = 1000;

        static AnimationSequence() 
        {
            for (int keyFraction = 0; keyFraction <= easeEffectDiscreteDomain; keyFraction++)
            {
                double oldFraction = keyFraction / (double)easeEffectDiscreteDomain;
                double theta = (1 - oldFraction) * Math.PI;
                double newFraction = (1 + Math.Cos(theta)) / 2;
                easeEffectTable.Add(keyFraction, newFraction);
            }
        }

        struct FramePoint
        {
            public T info;
            public double time;
            public Animation.EaseType easeType;
            public FramePoint(T info, double time, Animation.EaseType easeType)
            {
                this.info = info;
                this.time = time;
                this.easeType = easeType;
            }
        };

        class FrameComparerHelper : IComparer<FramePoint>
        {
            public int Compare(FramePoint frame1, FramePoint frame2)
            {
                if (frame1.time > frame2.time)
                {
                    return 1;
                }
                else if (frame1.time < frame2.time)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        static FrameComparerHelper FrameComparer = new FrameComparerHelper();

        List<FramePoint> frameList;
        Interpolator Interpolate;

        public AnimationSequence(T initialInfo, Interpolator customInterpolator)
        {
            frameList = new List<FramePoint>();
            Interpolate = customInterpolator;

            AddFrame(initialInfo, 0, Animation.EaseType.None);
        }

        public bool Empty()
        {
            return frameList.Count == 0;
        }

        public void AddFrame(T frameInfo, double targetTime, Animation.EaseType easeType)
        {
            FramePoint newFrame = new FramePoint(frameInfo, targetTime, easeType);

            RemoveFrame(targetTime);

            frameList.Add(newFrame);
            frameList.Sort(FrameComparer);
        }

        public void RemoveFrame(double targetTime)
        {
            if (frameList.Count == 0)
            {
                return;
            }

            FramePoint newFrame = frameList[0];
            newFrame.time = targetTime;
            int index = frameList.BinarySearch(newFrame, FrameComparer);
            if (index >= 0)
            {
                frameList.RemoveAt(index);
            }
        }

        public T InterpolateFrame(double targetTime)
        {
            if (targetTime <= 0)
            {
                return frameList[0].info;
            }

            FramePoint prevFrame = frameList[0];
            foreach (FramePoint curFrame in frameList)
            {
                if (targetTime <= curFrame.time)
                {
                    double fraction = (targetTime - prevFrame.time) / (curFrame.time - prevFrame.time);

                    /* Ease */
                    bool leftEase = prevFrame.easeType == Animation.EaseType.Out || prevFrame.easeType == Animation.EaseType.InAndOut;
                    bool rightEase = curFrame.easeType == Animation.EaseType.In || curFrame.easeType == Animation.EaseType.InAndOut;
                    if (leftEase && rightEase)
                    {
                        fraction = Ease(fraction, Animation.EaseType.InAndOut);
                    }
                    else if (leftEase)
                    {
                        fraction = Ease(fraction, Animation.EaseType.In);
                    }
                    else if (rightEase)
                    {
                        fraction = Ease(fraction, Animation.EaseType.Out);
                    }

                    return Interpolate(prevFrame.info, curFrame.info, fraction);
                }

                prevFrame = curFrame;
            }

            // If the targetTime is beyond the last frame.
            return frameList[frameList.Count - 1].info;
        }

        double Ease(double oldFraction, Animation.EaseType type)
        {
            double newFraction = oldFraction;
            int keyFraction = (int)(oldFraction * easeEffectDiscreteDomain);

            if (type == Animation.EaseType.In) 
            {
                keyFraction = keyFraction / 2;
                newFraction = easeEffectTable[keyFraction] * 2;
            }
            else if (type == Animation.EaseType.Out)
            {
                keyFraction = (keyFraction / 2) + (easeEffectDiscreteDomain / 2);
                newFraction = (easeEffectTable[keyFraction] - 0.5) * 2;
            }
            else
            {
                newFraction = easeEffectTable[keyFraction];
            }

            //Console.WriteLine("{0} to {1} when " + type.ToString(), oldFraction, newFraction);
            return newFraction;
        }
    }
}
