using Geometry3SharpLab.Models;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Specialized;

namespace Geometry3SharpLab.VTK
{
    public class AttachedProperty
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
            DependencyProperty.RegisterAttached("RenderWindow", typeof(RenderWindowControl), typeof(AttachedProperty), new PropertyMetadata(null, VTKInit));

        private static void VTKInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RenderWindowControl window = e.NewValue as RenderWindowControl;
            if (window != null)
            {
                var renderer = window.RenderWindow.GetRenderers().GetFirstRenderer();
                renderer.SetBackground(0.6, 0.6, 0.6);
                SetRenderer(d, window.RenderWindow.GetRenderers().GetFirstRenderer());
                GetRenderScene(d).Renderer = renderer;
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
            DependencyProperty.RegisterAttached("Renderer", typeof(vtkRenderer), typeof(AttachedProperty), new PropertyMetadata(null));

        public static Scene GetRenderScene(DependencyObject obj)
        {
            return (Scene)obj.GetValue(RenderSceneProperty);
        }

        public static void SetRenderScene(DependencyObject obj, Scene value)
        {
            obj.SetValue(RenderSceneProperty, value);
        }

        // Using a DependencyProperty as the backing store for RenderScene.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RenderSceneProperty =
            DependencyProperty.RegisterAttached("RenderScene", typeof(Scene), typeof(AttachedProperty), new PropertyMetadata(null));
    }
}
