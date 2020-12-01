using System;
using System.Windows.Forms;

namespace AdventOfCode.App
{
    public static class ControlExtensions
    {
        public static IDisposable UseCursor(this Control source, Cursor cursor)
        {
            return new CustomCursor(source, cursor);
        }
    }

    internal class CustomCursor : IDisposable
    {
        private readonly Control _ctrl;
        private readonly Cursor _previous;

        public CustomCursor(Control ctrl, Cursor cursor)
        {
            _ctrl = ctrl;
            _previous = ctrl.Cursor;
            ctrl.Cursor = cursor;
        }
        public void Dispose()
        {
            _ctrl.Cursor = _previous;
        }
    }
}
