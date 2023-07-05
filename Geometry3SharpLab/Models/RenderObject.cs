using CommunityToolkit.Mvvm.ComponentModel;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry3SharpLab.Models
{
    public class RenderObject : ObservableRecipient
    {
        public RenderObject(string path)
        {
            string extension = Path.GetExtension(path);
            vtkPolyDataAlgorithm reader = null;
            if (extension == ".obj")
            {
                reader = vtkOBJReader.New();
                vtkOBJReader.SafeDownCast(reader).SetFileName(path);
            }
            else if(extension == ".stl")
            {
                reader = vtkSTLReader.New();
                vtkSTLReader.SafeDownCast(reader).SetFileName(path);
            }
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            Name = Path.GetFileName(path);
            Actor = vtkActor.New();
            Actor.SetMapper(mapper);
            Uid = Guid.NewGuid();
        }

        public RenderObject()
        {
            Name = "Empty";
            Uid = Guid.NewGuid();
            Actor = null;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private Guid _uid;
        public Guid Uid
        {
            get { return _uid; }
            set { SetProperty(ref _uid, value); }
        }

        private vtkActor _actor;
        public vtkActor Actor
        { 
            get { return _actor; } 
            set {  SetProperty(ref _actor, value); } 
        }
    }
}
