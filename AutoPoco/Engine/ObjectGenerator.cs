using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Util;
using AutoPoco.Actions;

namespace AutoPoco.Engine
{
    public class ObjectGenerator<T> : IObjectGenerator<T>
    {
        private IObjectBuilder mType;
        private List<IObjectAction> mOverrides = new List<IObjectAction>();
        private IGenerationSession mSession;

        public ObjectGenerator(IGenerationSession session, IObjectBuilder type)
        {
            mSession = session;
            mType = type;
        }

        public T Get()
        {
            // Create the object     
            Object createdObject = mType.CreateObject(mSession);
 
            // And overrides
            foreach (var action in mOverrides)
            {                
                action.Enact(mSession, createdObject);
            }

            // And return the created object
            return (T)createdObject;
        }

        public void AddAction(IObjectAction action)
        {
            mOverrides.Add(action);
        }

        public IObjectGenerator<T> Impose<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> propertyExpr, TMember value)
        {
            var member = ReflectionHelper.GetMember(propertyExpr);
            if (member.IsField)
            {
                this.AddAction(new ObjectFieldSetFromValueAction((EngineTypeFieldMember)member, value));
            }
            else if (member.IsProperty)
            {
                this.AddAction(new ObjectPropertySetFromValueAction((EngineTypePropertyMember)member, value));
            }
                        
            return this;
        }
    }
}
