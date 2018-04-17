using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Hangfire;

namespace WebApplication1
{
    public class ContainerJobActivator : JobActivator
    {
        private IContainer _container;
        public ContainerJobActivator(IContainer container)
        {
            _container = container;
        }

        //public override object ActivateJob(Type type)
        //{
        //   // return _container.Resolve(type);
        //}
    }
}