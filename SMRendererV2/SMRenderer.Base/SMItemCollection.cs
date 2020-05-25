using System.Collections.Generic;
using System.Linq;
using SMRenderer.Base.Interfaces;

namespace SMRenderer.Base
{
    public class SMItemCollection : List<IShowObject>, IShowObject
    {
        public float RenderPosition { get; set; } = 0;

        public new void Add(IShowObject item)
        { 
            item.Add(this);
            base.Add(item);
        }

        public new void Remove(IShowObject item)
        {
            item.Remove(this);
            base.Remove(item);
        }

        public void Prepare() => ForEach(a => a.Prepare());

        public void Draw() => ForEach(a => a.Draw());

        public void Order()
        {
            var i = this.OrderBy(a => a.RenderPosition);
            RemoveAll(a => true);
            AddRange(i);
        }

        public void Add(SMItemCollection collection) => ForEach(a => a.Add(this));
        public void Remove(SMItemCollection collection) => ForEach(a => a.Remove(this));
    }
}