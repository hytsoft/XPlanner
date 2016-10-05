using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlanner.LabElements.Ingredients
{
    // Ingredients (just like vessels) are (currently) inertic. i.e. it has no methods.
    // Due to its divesity, this class will be defined as dynamic.
    // Thus, structure may change constantly.

    // Ingredients current structure is partly taken from 'Sigma-Aldrich' site.
    // https://www.sigmaaldrich.com/life-science/cell-biology.html

    public class Ingredient : LabElement
    {
        // Enums 

        public enum ADME_ToxType
        {
            PTAs, // Permiablity and Transporter Assays
            MAs,  // Metabolism Assays
            TAs,  // Toxicology Assays
        };

        public enum AntibioticType
        {
            ABPs, // Antibiotic Products
            ABSTs, // Antibiotic Selector Tools
            ABs,   // Antibiotic (will be sorted by groups)
        };

        public enum AntibodyType
        {
           MonoclonalAntiBeta_Actin_Mouse_AC15_AF, // Monoclonal Anti-beta Actin Antibody produced in Mouse, Clone AC-15, Ascites fluid
           AntiActin_Rabbit_AI // Anti-Actin antibody produced in Rabbit, Affinity Isolated Antibody
        };

        public enum BiochemicalType
        {
            NOR3, // E4Ethyl2_E_Hydroxymino_5Nitro3Hexenamide
            NOR4, // E4Ethyl2_Z_Hydroxymino_5Nitro3Hexen1ylNicotinamide
        };

        public enum ReagentType
        {
            
        };

        public enum CellCultureType
        {
            Fibroblast,
            Endothelia,
            Epithelia,
            Neuro,
        };

        public enum GenType
        {
            DNA,
            RNA,
            TF,
        };

        public enum BufferType
        {
            AMPD,
            AMP95, 
            ACES // N-(2-Acetamido)-2-aminoethanesulfonic acid
        };

        public enum StainType
        {
            AcridineOrange,
            BismarkBrown,
            Carmine,
            CoomassieBlue,
            CresylViolet,
            CrystalViolet,
            DAPI,
            Eosin,
            EthidiumBromide,
            AcidFuchsine,
            Hematoxilin,
        };

        // Classes - optimization may be required

        public class ADME_Tox : Ingredient
        {
            ADME_ToxType m_type;
        }

        public class Antibody : Ingredient
        {
            AntibodyType m_type;
        }

        public class Biochemical : Ingredient
        {
            BiochemicalType m_type;
        }

        public class Reagent : Ingredient 
        {
            ReagentType m_type;
        }

        public class CellCulture : Ingredient
        {
            CellCultureType m_type;
        }

        public class Genetic : Ingredient
        {
            GenType m_type;
        }

        public class Buffur : Ingredient
        {
            BufferType m_type;
        }

        public class Stain : Ingredient
        {
            StainType m_type;
        }

    }
}
