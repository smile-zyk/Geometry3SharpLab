using CommunityToolkit.Mvvm.ComponentModel;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry3SharpLab.Models
{
    public class Scene : ObservableRecipient
    {
        private void UpdateActor(SceneTreeNode node)
        {
            if (node.Actor != null && Renderer.HasViewProp(node.Actor) == 0)
            {
                Renderer.AddActor(node.Actor);
            }
            if (node.IsVisibilty)
            {
                node.Actor.VisibilityOn();
            }
            else
            {
                node.Actor.VisibilityOff();
            }
        }

        void UpdateScene()
        {
            SceneTreeNode.TraverseNode(SceneRoot, UpdateActor);
            Renderer.ResetCamera();
            Renderer.GetRenderWindow().Render();
        }

        void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var list = e.NewItems;
                foreach (var item in list)
                {
                    var node = item as SceneTreeNode;
                    node.PropertyChanged += (a, b) => { UpdateScene(); };
                    node.Children.CollectionChanged += CollectionChanged;
                }
            }
        }

        public vtkRenderer Renderer { get; set; }

        public List<SceneTreeNode> SceneItemSources
        {
            get => new List<SceneTreeNode> { SceneRoot };
        }

        private SceneTreeNode sceneRoot;
        public SceneTreeNode SceneRoot
        {
            get => sceneRoot; 
            set 
            {
                if(SetProperty(ref sceneRoot, value))
                {
                    OnPropertyChanged(nameof(SceneItemSources));
                }
            }
        }

        public void AddNode(SceneTreeNode node)
        {
            SceneRoot.Children.Add(node);
        }

        public Scene()
        {
            SceneRoot = new SceneTreeNode();
            SceneRoot.Name = "SceneTree";
            SceneRoot.PropertyChanged += (s, t) => { UpdateScene(); };
            SceneRoot.Children.CollectionChanged += CollectionChanged;
        }
    }
}
