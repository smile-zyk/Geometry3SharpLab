using Geometry3SharpLab.Models;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.Integration;

namespace Geometry3SharpLab.AttachedProperty
{
    public class Helper
    {
        public static RenderWindowControl GetRenderWindow(DependencyObject obj)
        {
            return (RenderWindowControl)obj.GetValue(RenderWindowProperty);
        }

        public static void SetRenderWindow(DependencyObject obj, RenderWindowControl value)
        {
            obj.SetValue(RenderWindowProperty, value);
        }

        public static readonly DependencyProperty RenderWindowProperty =
            DependencyProperty.RegisterAttached("RenderWindow", typeof(RenderWindowControl), typeof(Helper), new PropertyMetadata(null, VTKInit));

        private static void VTKInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RenderWindowControl window = e.NewValue as RenderWindowControl;
            if (window != null)
            {
                var renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
                renderer.SetBackground(0.6, 0.6, 0.6);
                SetRenderer(d, window.RenderWindow.GetRenderers().GetFirstRenderer());
            }
        }

        public static vtkRenderer GetRenderer(DependencyObject obj)
        {
            return (vtkRenderer)obj.GetValue(RendererProperty);
        }

        public static void SetRenderer(DependencyObject obj, vtkRenderer value)
        {
            obj.SetValue(RendererProperty, value);
        }

        public static readonly DependencyProperty RendererProperty =
            DependencyProperty.RegisterAttached("Renderer", typeof(vtkRenderer), typeof(Helper), new PropertyMetadata(null));

        public static ObservableCollection<SceneTreeNode> GetSceneRoots(DependencyObject obj)
        {
            return (ObservableCollection<SceneTreeNode>)obj.GetValue(SceneRootsProperty);
        }

        public static void SetSceneRoots(DependencyObject obj, ObservableCollection<SceneTreeNode> value)
        {
            obj.SetValue(SceneRootsProperty, value);
        }

        // Using a DependencyProperty as the backing store for SceneRoots.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SceneRootsProperty =
            DependencyProperty.RegisterAttached("SceneRoots", typeof(ObservableCollection<SceneTreeNode>), typeof(Helper), new PropertyMetadata(null, SceneInit));

        private static void UpdateActor(SceneTreeNode node)
        {
            if (node.Actor != null && node.Renderer.HasViewProp(node.Actor) == 0)
            {
                node.Renderer.AddActor(node.Actor);
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

        private static void SceneInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<SceneTreeNode> SceneRoots = e.NewValue as ObservableCollection<SceneTreeNode>;
            SceneRoots.CollectionChanged += (s, t) =>
            {
                var list = t.NewItems;
                vtkRenderer renderer = GetRenderer(d);
                foreach (var item in list)
                {
                    var root = item as SceneTreeNode;
                    SceneTreeNode.TraverseNode(root, UpdateActor);
                    renderer.GetRenderWindow().Render();
                    // 保证后续添加children时依然能触发
                    root.PropertyChanged += (a, b) =>
                    {
                        SceneTreeNode.TraverseNode(root, UpdateActor);
                        renderer.GetRenderWindow().Render();
                    };
                }
            };
        }

        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Helper), new PropertyMetadata(false));



        public static bool GetIsFoucs(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFoucsProperty);
        }

        public static void SetIsFoucs(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFoucsProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsFoucs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFoucsProperty =
            DependencyProperty.RegisterAttached("IsFoucs", typeof(bool), typeof(Helper), new PropertyMetadata(false));


    }
}
