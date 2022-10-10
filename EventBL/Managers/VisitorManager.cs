using EventBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Managers
{
    public class VisitorManager
    {
        private int id = 0;
        private Dictionary<int,Visitor> visitors = new Dictionary<int, Visitor>();
        public Visitor RegisterVisitor(Visitor visitor)
        {
            visitor.SetId(++id);
            return visitor;
        }
        public void SubscribeToList(Visitor visitor)
        {
            if (visitor == null) throw new EventException("VisitorManager - Subscribe");
            if (visitor.Id == 0) throw new EventException("VisitorManager - Subscribe");
            if (visitors.ContainsKey(visitor.Id)) throw new EventException("VisitorManager - Subscribe");            
            visitors.Add(visitor.Id, visitor);  
        }
        public void UnsubscribeFromList(Visitor visitor)
        {
            if (visitor == null) throw new EventException("VisitorManager - UnSubscribe");
            if (visitor.Id == 0) throw new EventException("VisitorManager - UnSubscribe");
            if (!visitors.ContainsKey(visitor.Id)) throw new EventException("VisitorManager - UnSubscribe");
            visitors.Remove(visitor.Id);
        }
        public IReadOnlyList<Visitor> GetVisitors()
        {
            return visitors.Values.ToList().AsReadOnly();
        }
        public Visitor GetVisitor(int id)
        {
            if (!visitors.ContainsKey(id)) throw new EventException("VisitorManager - GetVisitor");
            return visitors[id];
        }
        public void UpdateVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("VisitorManager - Update");
            if (visitor.Id == 0) throw new EventException("VisitorManager - Update");
            if (!visitors.ContainsKey(visitor.Id)) throw new EventException("VisitorManager - Update");
            if (!visitor.IsDifferent(visitors[visitor.Id])) throw new EventException("VisitorManager - Update");
            visitors[visitor.Id] = visitor;
        }
    }
}
