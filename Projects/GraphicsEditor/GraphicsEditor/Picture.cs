using System;
using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor
{
    public class Picture : IDrawable
    {
        private readonly List<IDrawable> shapes = new List<IDrawable>();
        private readonly object lockObject = new object();

        public event Action Changed;

        public void Remove(IDrawable shape)
        {
            lock (lockObject)
            {
                shapes.Remove(shape);
            }
        }

        public void RemoveAt(int index)
        {
            lock (lockObject)
            {
                shapes.RemoveAt(index);
                Changed?.Invoke();
            }
        }

        public void Add(IDrawable shape)
        {
            lock (lockObject)
            {
                shapes.Add(shape);
                Changed?.Invoke();
            }
        }

        public void Add(int index, IDrawable shape)
        {
            lock (lockObject)
            {
                shapes.Insert(index, shape);
                Changed?.Invoke();
            }
        }

        public void Draw(IDrawer drawer)
        {
            lock (lockObject)
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }
    }
}
