using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public class ObjectBuilder : IObjectBuilder
    {
        private List<IObjectAction> mActions = new List<IObjectAction>();

        public Type InnerType
        {
            get;
            private set;
        }

        public IEnumerable<IObjectAction> Actions
        {
            get { return mActions; }
        }

        public void ClearActions()
        {
            mActions.Clear();
        }

        public void AddAction(IObjectAction action)
        {
            mActions.Add(action);
        }

        public void RemoveAction(IObjectAction action)
        {
            mActions.Remove(action);
        }

        /// <summary>
        /// Creates this object builder
        /// </summary>
        /// <param name="type"></param>
        public ObjectBuilder(Type type)
        {
            this.InnerType = type;
        }

        public Object CreateObject(IGenerationSession session)
        {
            Object createdObject = Activator.CreateInstance(this.InnerType);

            // Perform configuration actions
            foreach (var action in this.mActions)
            {
                action.Enact(session, createdObject);
            }
            return createdObject;
        }
    }
}
