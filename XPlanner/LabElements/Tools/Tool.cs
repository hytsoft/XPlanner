using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlanner.LabElements.Vessels;

namespace XPlanner.LabElements.Tools
{
    public class Tool : LabElement
    {
        Vessel m_Sample;
    }

    public class Vortex: Tool
    {
        public void Mix()
        {
            //blah
        }
    }
}
