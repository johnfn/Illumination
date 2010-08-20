using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumination.Graphics.AnimationHandler
{
    public class AnimationSequence<T>
    {
        public delegate T Interpolator(T frame1, T frame2, double fraction);

        struct FramePoint
        {
            public T info;
            public double time;
            public FramePoint(T info, double time)
            {
                this.info = info;
                this.time = time;
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

            AddFrame(initialInfo, 0);
        }

        public void AddFrame(T frameInfo, double targetTime)
        {
            FramePoint newFrame = new FramePoint(frameInfo, targetTime);

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
                    return Interpolate(prevFrame.info, curFrame.info, fraction);
                }

                prevFrame = curFrame;
            }

            // If the targetTime is beyond the last frame.
            return frameList[frameList.Count - 1].info;
        }
    }
}
