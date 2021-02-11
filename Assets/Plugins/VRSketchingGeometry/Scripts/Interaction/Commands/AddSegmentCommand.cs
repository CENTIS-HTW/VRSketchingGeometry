﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.SketchObjectManagement;

namespace VRSketchingGeometry.Commands.Patch {
    /// <summary>
    /// Add control point at the end of spline.
    /// </summary>
    public class AddSegmentCommand : ICommand
    {
        private PatchSketchObject PatchSketchObject;
        private List<Vector3> NewSegment;

        public AddSegmentCommand(PatchSketchObject patchSketchObject, List<Vector3> newSegment) {
            this.PatchSketchObject = patchSketchObject;
            this.NewSegment = newSegment;
        }

        public void Execute()
        {
            this.PatchSketchObject.AddPatchSegment(NewSegment);
        }

        public void Redo()
        {
            this.Execute();
        }

        public void Undo()
        {
            this.PatchSketchObject.RemovePatchSegment();
        }

        /// <summary>
        /// This will only return a command object if the distance between the previous and new control point is at least minimumDistance.
        /// </summary>
        /// <param name="patchSketchObject"></param>
        /// <param name="point"></param>
        /// <param name="rotation"></param>
        /// <param name="minimumDistanceToLastControlPoint"></param>
        /// <returns>A command or null if the distance is smaller than minimumDistance.</returns>
        public static AddSegmentCommand GetAddSegmentCommandContinuous(PatchSketchObject patchSketchObject, List<Vector3> segment, float minimumDistanceToLastSegment)
        {
            if (segment.Count != patchSketchObject.Width) return null;

            bool distanceExceeded = true;

            List<Vector3> lastSegment = patchSketchObject.GetLastSegment();

            for (int i = 0; i < lastSegment.Count; i++)
            {
                if ((lastSegment[i] - segment[i]).magnitude < minimumDistanceToLastSegment) {
                    distanceExceeded = false;
                }
            }


            if (distanceExceeded)
            {
                return new AddSegmentCommand(patchSketchObject, segment);
            }
            else
            {
                return null;
            }
        }
    }
}
