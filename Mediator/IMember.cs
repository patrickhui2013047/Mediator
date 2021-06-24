using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PH.Pattern.Behavior
{
    public interface IMember
    {
        int ID { get; protected set; }
        Type MemberType { get; }
        bool IsServiceProvider { get; }
        internal void SetID(int id) { ID = id; }
    }
}
