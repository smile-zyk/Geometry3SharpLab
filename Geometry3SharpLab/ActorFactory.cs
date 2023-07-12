using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace Geometry3SharpLab.VTK
{
    public static class ActorFactory
    {
        public static vtkActor CreateSphereActor(double radius, double[] center)
        {
            if (center.Length != 3 || radius <= 0.0) return null;
            vtkSphereSource sphereSource = vtkSphereSource.New();
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            vtkActor actor = new vtkActor();

            sphereSource.SetRadius(radius);
            sphereSource.SetCenter(center[0], center[1], center[2]);
            mapper.SetInputConnection(sphereSource.GetOutputPort());
            actor.SetMapper(mapper);

            return actor;
        }

        public static vtkActor CreateCubeActor(double[] bounds, double[] center)
        {
            if (bounds.Length != 6 || center.Length != 3) return null;
            vtkCubeSource cubeSource = vtkCubeSource.New();
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            vtkActor actor = new vtkActor();

            cubeSource.SetBounds(bounds[0], bounds[1], bounds[2], bounds[3], bounds[4], bounds[5]);
            cubeSource.SetCenter(center[0], center[1], center[2]);
            mapper.SetInputConnection(cubeSource.GetOutputPort());
            actor.SetMapper(mapper);

            return actor;
        }

        public static vtkActor CreateTriangleMeshActor(double[][] vertices, int[] indices)
        {
            vtkActor actor = new vtkActor();
            return actor;
        }

        public static vtkActor CreateQuadMeshActor(double[][] vertices, int[] indices)
        {
            vtkActor actor = new vtkActor();
            return actor;
        }

        public static vtkActor CreatePolylineActor(double[][] vertices, bool isClosed = false)
        {
            vtkActor actor = new vtkActor();
            return actor;
        }
    }
}
