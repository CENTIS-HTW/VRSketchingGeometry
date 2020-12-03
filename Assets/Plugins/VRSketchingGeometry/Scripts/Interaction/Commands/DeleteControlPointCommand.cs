﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.SketchObjectManagement;

namespace VRSketchingGeometry.Commands
{
    /// <summary>
    /// Delete control point at the end of spline.
    /// </summary>
    public class DeleteControlPointCommand : ICommand
    {
        private LineSketchObject LineSketchObject;
        private Vector3 OldControlPoint;

        public DeleteControlPointCommand(LineSketchObject lineSketchObject)
        {
            this.LineSketchObject = lineSketchObject;
        }

        public void Execute()
        {
            this.OldControlPoint = LineSketchObject.getControlPoints()[LineSketchObject.getNumberOfControlPoints() - 1];
            LineSketchObject.deleteControlPoint();
        }

        public void Redo()
        {
            this.Execute();
        }

        public void Undo()
        {
            LineSketchObject.addControlPoint(OldControlPoint);
        }
    }
}