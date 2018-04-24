using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DrawablesUI
{
    public class DrawableGui
    {
        private readonly IDrawable _picture;
        private readonly Form _form;
        private Thread _thread;

        public DrawableGui(IDrawable picture)
        {
            _picture = picture;
            _form = new Form();
        }

        public void Refresh()
        {
            _form.Invalidate();
        }

        public void Stop()
        {
            _form.Invoke((MethodInvoker) (() =>
            {
               _form.Close();
               Application.ExitThread();
            }));
        }

        public void Start()
        {
            _thread = new Thread(() =>
            {
                _form.Paint += (sender, e) =>
                {
                    using (var x = new GraphicsDrawer(e.Graphics))
                    {
                        _picture.Draw(x);
                    }
                };
                _form.BackColor = Color.White;
                _form.Show();
                Application.Run();
            });
            _thread.Start();
        }
    }
}
