using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medevia.Core.Framework
{
    public interface IRepository
    {
        /// <summary>
        /// Use it to define class is a repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
