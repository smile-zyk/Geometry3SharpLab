using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;
using System.IO;

namespace Geometry3SharpLab.Models
{
    public class SceneTreeNode : ObservableRecipient
    {
        public static void TraverseNode(SceneTreeNode node, Action<SceneTreeNode> action)
        {
            if (node == null) return;
            Console.WriteLine("Name: {0}, IsVisibity: {1}, IsExpanded: {2}"
                , node.Name, node.IsVisibilty, node.IsExpanded);
            action?.Invoke(node);
            foreach (var child in node.Children)
            {
                TraverseNode(child, action);
            }
        }

        public SceneTreeNode()
        {
            Name = "Empty";
            Uid = Guid.NewGuid();
            IsVisibilty = true;
            Actor = vtkActor.New();
            Children = new ObservableCollection<SceneTreeNode>();
            Children.CollectionChanged += (s, e) => { OnPropertyChanged(nameof(Children)); };
        }

        public void ReadFile(string path)
        {
            string extension = Path.GetExtension(path);
            vtkPolyDataAlgorithm reader = null;
            if (extension == ".obj")
            {
                reader = vtkOBJReader.New();
                vtkOBJReader.SafeDownCast(reader).SetFileName(path);
            }
            else if (extension == ".stl")
            {
                reader = vtkSTLReader.New();
                vtkSTLReader.SafeDownCast(reader).SetFileName(path);
            }
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            Name = Path.GetFileNameWithoutExtension(path);
            Actor = vtkActor.New();
            Actor.SetMapper(mapper);
            Uid = Guid.NewGuid();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private bool _isVisiblity;
        public bool IsVisibilty
        {
            get { return _isVisiblity; }
            set
            {
                SetProperty(ref _isVisiblity, value);
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        private bool _isExpanded = true; 
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
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
            set { SetProperty(ref _actor, value); }
        }

        public ObservableCollection<SceneTreeNode> _children;
        public ObservableCollection<SceneTreeNode> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }
    }
}
