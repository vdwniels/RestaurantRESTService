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
        private int id = 1;
        private Dictionary<int,Visitor> visitors = new Dictionary<int, Visitor>();

        public VisitorManager()
        {
            visitors.Add(id, new Visitor("John", DateTime.Parse("12/3/1975"), id++));
            visitors.Add(id, new Visitor("Jane", DateTime.Parse("18/7/1995"), id++));
            visitors.Add(id, new Visitor("David", DateTime.Parse("2/4/2001"), id++));
            visitors.Add(id, new Visitor("Chris", DateTime.Parse("12/9/1999"), id++));
        }

        public Visitor RegisterVisitor(Visitor visitor)
        {
            visitor.SetId(id++);
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
