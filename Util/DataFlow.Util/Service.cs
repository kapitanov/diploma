using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AISTek.DataFlow.Util
{
    public static class Service
    {
        public static void Shield(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                throw new FaultException(new FaultReason(e.ToString()), new FaultCode(e.GetType().FullName));
            }
        }

        public static T Shield<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                throw new FaultException(new FaultReason(e.ToString()), new FaultCode(e.GetType().FullName));
            }
        }
    }
}
