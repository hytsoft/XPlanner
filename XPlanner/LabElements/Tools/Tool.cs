using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlanner.LabElements.Vessels;
using XPlanner.LabElements.Devices;

namespace XPlanner.LabElements.Tools
{
    public class Tool : LabElement
    {
        Vessel m_Sample;
    }

    public enum Scale
    {
        Mili,
        Micro
    };

    public class Pipette : Tool, IFlowable
    {
        Scale m_Scale;
        int m_Volume;

        public void Draw()
        {

        }
        public void Release()
        {

        }
    }

    public class Vortex: Tool, IMixable
    {
        public void Mix()
        {
           
        }
    }

    public class Stirrer: Tool, IMixable
    {
        public void Mix()
        {
        }
    }

    public interface IHeatable
    {
        void Heat();
    }
    
}
