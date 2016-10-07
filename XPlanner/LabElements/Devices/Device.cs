using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlanner.LabElements.Devices
{
    public class Device : LabElement
    {

    }

    public interface IImageable
    {
        void Scan();
    }

    public interface IObservable
    {
        void Observe();
    }

    public interface IMeasurable
    {
        void Measure(string parameter);
    }

    public class AFM : Device, IImageable, IObservable, IMeasurable
    {
        public void Scan()
        {

        }

        public void Observe()
        {

        }

        public void Measure(string parameter)
        {

        }
    }

    public class Confocal: Device, IImageable, IObservable
    {
        public void Scan()
        {

        }

        public void Observe()
        {

        }
    }

    public class SEM : Device, IImageable, IObservable
    {
        public void Scan()
        {

        }

        public void Observe()
        {

        }
    }

    public class Fluorescence : Device, IImageable, IObservable
    {
        public void Scan()
        {

        }

        public void Observe()
        {

        }
    }

    public class LightMicroscope : Device, IObservable
    {
        public void Observe()
        {

        }
    }

    public class Rheometer : Device, IMeasurable
    {
        public void Measure(string parameter)
        {

        }
    }

    public class Viscometer : Device, IMeasurable
    {
        public void Measure(string parameter)
        {

        }
    }

    public class OpticalTweezers : Device, IMeasurable, IObservable
    {
        public void Observe()
        {

        }

        public void Measure(string parameter)
        {

        }
    }

    public class MagneticTweezers : Device, IMeasurable, IObservable
    {
        public void Observe()
        {

        }

        public void Measure(string parameter)
        {

        }
    }

    public class PhotoSpectrometer: Device, IMeasurable
    {
        public void Measure(string paramter)
        {

        }
    }

    public class Spectroscope : Device, IMeasurable
    {
        public void Measure(string paramter)
        {

        }
    }

    //Microscopy
    //  AFM - Surface Scan
    //  SEM - Scan, Observe
    //  Confocal - Scan, Observe
    //  Fluorescence - Scan, Observe
    //  Optical - Observe

    //Rheology
    //  AFM - Force Measurement
    //  Rheometer - Bulk stiffness measurement
    //  Viscometer - Bulk viscosity measurement
    //  Optical Tweezers - Cell Stiffness
    //  Magnetic Tweezers - Cell Stiffness

    //Spectrometry
    //  Photo-spectrometer - Molecular composition

    //Spectroscopy
    //  Spectroscope - Spectrographic analysis
}
