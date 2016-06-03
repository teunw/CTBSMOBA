using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IFieldObject
    {
        /// <summary>
        /// Method for calculating the distance of whether an object is done with its action or not
        /// </summary>
        /// <returns>Whether it has done its action</returns>
        void ActionDone();
    }
}
