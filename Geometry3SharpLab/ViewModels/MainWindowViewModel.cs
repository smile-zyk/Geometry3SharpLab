using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Geometry3SharpLab.Models;
using System.Windows.Forms;
using Kitware.VTK;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Geometry3SharpLab.ViewModels
{
    internal class MainWindowViewModel : ObservableRecipient
    {
        public MainWindowViewModel()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
            SceneTreeItemChangedCommand = new RelayCommand<object>(SceneTreeItemChanged);
            RenderScene = new Scene();
        }

        private void SceneTreeItemChanged(object obj)
        {
            SceneTreeNode node = obj as SceneTreeNode;
            SceneTreeNode.TraverseNode(RenderScene.SceneRoot, (n) => { n.IsSelected = false; });
            SelectedNode = node;
            SelectedNode.IsSelected = true;
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Wavefront OBJ files (*.obj)|*.obj|STL files (*.stl)|*.stl|PLY files (*.ply)|*.ply|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                SceneTreeNode node = new SceneTreeNode();
                node.ReadFile(filePath);
                RenderScene.AddNode(node);
                SceneTreeNode.TraverseNode(RenderScene.SceneRoot, (n) => { n.IsExpanded = true; });
            }
        }

        public SceneTreeNode SelectedNode { get; set; }

        private Scene renderScene;
        public Scene RenderScene
        {
            get => renderScene;
            set => SetProperty(ref renderScene, value);
        }

        private vtkRenderer _vtkRenderer;
        public vtkRenderer VTKRenderer
        {
            get { return _vtkRenderer; }
            set { SetProperty(ref _vtkRenderer, value); }
        }
        public RelayCommand OpenFileCommand { get; }
        public RelayCommand<object> SceneTreeItemChangedCommand { get; }
    }
}
