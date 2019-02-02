using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Tekla Structures Namespaces
using Tekla.Structures;
using Tekla.Structures.Model;
using T3D = Tekla.Structures.Geometry3d;
using TSMUI = Tekla.Structures.Model.UI;
using System.Collections;

namespace Cuts_And_Fittings
{
    public partial class Form1 : Form
    {
        Model currentModel;

        public Form1()
        {
            InitializeComponent();

            try
            {
                currentModel = new Model();
            }
            catch 
            {

                MessageBox.Show("Model may not be connected!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnWorkPlane_Click(object sender, EventArgs e)
        {
            // Reset workplane back to global
            currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

            TSMUI.Picker currentPicker = new TSMUI.Picker();
            Part pickedPart = null;

            try
            {
                pickedPart = currentPicker.PickObject(TSMUI.Picker.PickObjectEnum.PICK_ONE_PART) as Part;
            }
            catch 
            {

                pickedPart = null;
            }

            if (pickedPart != null)
            {
                // Change the workplane to the coordinate system of the plate
                currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(pickedPart.GetCoordinateSystem()));

                // Show the plate in the model and the workplane change
                currentModel.CommitChanges();

                // Draw Positive Z axis
                TSMUI.GraphicsDrawer myDrawer = new TSMUI.GraphicsDrawer();
                myDrawer.DrawLineSegment(new T3D.LineSegment(new T3D.Point(0, 0, 0), new T3D.Point(0, 0, 500)), new TSMUI.Color(1, 0, 0));

            }
        }

        private void btnApplyFitting_Click(object sender, EventArgs e)
        {
            // Current workplane. Remember how the user had the model before you changed things.
            TransformationPlane currentPlane = currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            TSMUI.Picker myPicker = new TSMUI.Picker();
            Beam currentBeam = null;

            try
            {
                currentBeam = myPicker.PickObject(TSMUI.Picker.PickObjectEnum.PICK_ONE_PART) as Beam;
            }
            catch 
            {
                currentBeam = null;
            }

            if (currentBeam != null)
            {
                // Change the workplane to the coordinate system of the beam.
                currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(currentBeam.GetCoordinateSystem()));

                // Apply fitting
                Fitting beamFitting = new Fitting();
                beamFitting.Father = currentBeam;
                Plane fittingPlane = new Plane();
                fittingPlane.Origin = new T3D.Point(500, 0, 0);
                fittingPlane.AxisX = new T3D.Vector(0, 0, 500);
                fittingPlane.AxisY = new T3D.Vector(0, 500, 0);
                beamFitting.Plane = fittingPlane;
                beamFitting.Insert();

                // Apply cut line
                CutPlane beamLineCut = new CutPlane();
                beamLineCut.Father = currentBeam;
                Plane beamCutPlane = new Plane();
                beamCutPlane.Origin = new T3D.Point(2000, 0, 0);
                beamCutPlane.AxisX = new T3D.Vector(0, 0, 500);
                // Changing positive vs. negative value here determines which direction
                // the line cut will take away material where as fittin glooks at which end
                // of the beam it is closest to to figure out how to cut
                beamCutPlane.AxisY = new T3D.Vector(0, -500, 0);
                beamLineCut.Plane = beamCutPlane;
                beamLineCut.Insert();

                // Setworkplane back to what user had before
                currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);

                // Show the fitting in the model but the user will never see the workplane change
                currentModel.CommitChanges();
            }

        }

        private void btnPartCut_Click(object sender, EventArgs e)
        {
            // Current Workplane. Remember how the user had the model before you made changes
            TransformationPlane currentPlane = currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            TSMUI.Picker myPicker = new TSMUI.Picker();
            Part pickedPart = null;
            try
            {
                pickedPart = myPicker.PickObject(Tekla.Structures.Model.UI.Picker.PickObjectEnum.PICK_ONE_PART) as Part;
            }
            catch 
            {
                pickedPart = null;
            }

            if (pickedPart != null)
            {
                // Change the workplaen to the coordinate system of the part
                currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(pickedPart.GetCoordinateSystem()));

                Beam beamPartObject = new Beam();
                beamPartObject.StartPoint = new T3D.Point(400, 0, -200);
                beamPartObject.EndPoint = new T3D.Point(400, 0, 200);
                beamPartObject.Profile.ProfileString = "D200";
                beamPartObject.Material.MaterialString = "ANTIMATERIAL";
                beamPartObject.Class = BooleanPart.BooleanOperativeClassName;
                beamPartObject.Name = "CUT"; ;
                beamPartObject.Position.Depth = Position.DepthEnum.MIDDLE;
                beamPartObject.Position.Rotation = Position.RotationEnum.FRONT;
                beamPartObject.Position.Plane = Position.PlaneEnum.MIDDLE;

                if (!beamPartObject.Insert())
                {
                    Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Cut was not created.");
                    // Setworkplane back to what user had vefore changes
                    currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
                }
                else
                {
                    BooleanPart partCut = new BooleanPart();
                    partCut.Father = pickedPart;
                    partCut.OperativePart = beamPartObject;
                    partCut.Type = BooleanPart.BooleanTypeEnum.BOOLEAN_CUT;

                    if (!partCut.Insert())
                    {
                        // Setworkplane back to what user had before
                        currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);

                    }
                    else
                    {
                        // We don't need the physical part in the model anymore
                        beamPartObject.Delete();

                        // Setworkplane back to what user had before
                        currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);

                        // Show the fitting in the model but the user will never see the workplane change
                        currentModel.CommitChanges();
                    }
                }
            }
        }

        private void btnPolygonCut_Click(object sender, EventArgs e)
        {
            // Current Workplane. Remember how the user had model before you made changes
            TransformationPlane currentPlane = currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            TSMUI.Picker myPicker = new TSMUI.Picker();
            Part pickedPart = null;

            try
            {
                pickedPart = myPicker.PickObject(Tekla.Structures.Model.UI.Picker.PickObjectEnum.PICK_ONE_PART) as Part;
            }
            catch 
            {
                pickedPart = null;
            }

            if (pickedPart != null)
            {
                // Change the workplane to the coordinate system of the part
                currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(pickedPart.GetCoordinateSystem()));

                // Apply polygon cut
                ContourPlate contourPlateObject = new ContourPlate();
                contourPlateObject.AssemblyNumber.Prefix = "XX";
                contourPlateObject.AssemblyNumber.StartNumber = 1;
                contourPlateObject.PartNumber.Prefix = "xx";
                contourPlateObject.PartNumber.StartNumber = 1;
                contourPlateObject.Name = "CUT";
                contourPlateObject.Profile.ProfileString = "200";
                contourPlateObject.Material.MaterialString = "ANTIMATERIAL";
                contourPlateObject.Finish = "";
                // THIS IS THE IMPORTANT PART!!!
                contourPlateObject.Class = BooleanPart.BooleanOperativeClassName;
                contourPlateObject.Position.Depth = Position.DepthEnum.MIDDLE;
                // When doing a polygon cut make sure you don't do it right along edge
                // or sometimes you might et a solid error and your part will disappear.
                contourPlateObject.AddContourPoint(new ContourPoint(new T3D.Point(-10, -10, 0), null));
                contourPlateObject.AddContourPoint(new ContourPoint(new T3D.Point(100, -10, 0), null));
                contourPlateObject.AddContourPoint(new ContourPoint(new T3D.Point(100, 100, 0), null));
                contourPlateObject.AddContourPoint(new ContourPoint(new T3D.Point(-10, 100, 0), null));

                if (!contourPlateObject.Insert())
                {
                    Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Plate was ot created");
                    // Set workplane to what user had before
                    currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
                }
                else
                {
                    // We don't need the physical part in the model anymore.
                    contourPlateObject.Delete();

                    // Set workplane to what user had before
                    currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);

                    // Show the fitting in the model, the user will never see the workplane change.
                    currentModel.CommitChanges();
                }

            }
        }
    }
}
