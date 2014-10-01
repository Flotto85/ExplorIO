using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExplorIO.UI.Interfaces;
using ExplorIO.Data;

namespace ExplorIO.UI.Presenters
{
    public class Presenter<T> where T : IView
    {
        protected static IModel Model { get; private set; }
        protected T View { get; private set; }

        public Presenter(T view)
        {
            View = view;
        }
    }
}
