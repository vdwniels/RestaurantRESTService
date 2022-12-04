using EventBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Managers {
    public class VisitorManager {
        private int id = 0; //Normaalgezien spreken we hiervoor de datalaag aan: Het is de id die we terugkrijgen na een visitor te registreren.
        private Dictionary<int, Visitor> _visitors = new Dictionary<int, Visitor>();

        public VisitorManager() {
            _visitors.Add(++id, new Visitor(id, "Sarah", new DateTime(1980, 2, 3)));
            _visitors.Add(++id, new Visitor(id, "Karel", new DateTime(1970, 12, 23)));
            _visitors.Add(++id, new Visitor(id, "Anna", new DateTime(1990, 8, 13)));
            _visitors.Add(++id, new Visitor(id, "Frederik", new DateTime(1980, 4, 30)));
        }

        public Visitor RegisterVisitor(Visitor visitor) {
            visitor.SetId(++id);
            return visitor;
        }
        public void SubscribeVisitor(Visitor visitor) {
            if (visitor == null) { throw new EventException("VisitorManager - Subscribe"); }
            if (visitor.Id == 0) { throw new EventException("VisitorManager - Subscribe"); }
            if (_visitors.ContainsKey(visitor.Id)) { throw new EventException("VisitorManager - Subscribe"); }
            _visitors.Add(visitor.Id, visitor);
        }
        public void UnsubscribeVisitor(Visitor visitor) {
            if (visitor == null) { throw new EventException("VisitorManager - Subscribe"); }
            if (visitor.Id == 0) { throw new EventException("VisitorManager - Subscribe"); }
            if (!_visitors.ContainsKey(visitor.Id)) { throw new EventException("VisitorManager - Subscribe"); }
            _visitors.Remove(visitor.Id);

        }
        public IReadOnlyList<Visitor> GetVisitors() {
            return _visitors.Values.ToList(); //.AsReadOnly();
        }
        public Visitor GetVisitor(int id) {
            if (id == 0) { throw new EventException("VisitorManager - GetVisitor"); } // optioneel
            if (!_visitors.ContainsKey(id)) { throw new EventException("VisitorManager - GetVisitor"); }
            return _visitors[id];
        }
        public void UpdateVisitor(Visitor visitor) {
            if (visitor == null) { throw new EventException("VisitorManager - UpdateVisitor"); }
            if (visitor.Id == 0) { throw new EventException("VisitorManager - UpdateVisitor"); }
            if (!_visitors.ContainsKey(visitor.Id)) { throw new EventException("VisitorManager - UpdateVisitor"); }

            // Zijn er effectief wijzigingen?
            if (!visitor.IsDifferent(_visitors[visitor.Id])) { throw new EventException("VisitorManager - UpdateVisitor"); }

            // Visitor teruggeven
            _visitors[visitor.Id] = visitor;
        }
    }
}
