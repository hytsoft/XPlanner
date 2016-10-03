using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlanner.LabElements.Ingredients
{
    // Ingredients (just like vessels) are (currently) inertic. i.e. it has no methods.
    // Due to its divesity, this class will be defined as a dynamic one.
    // Thus, structure may change constantly.

    // Ingredients current structure is partly taken from 'Sigma-Aldrich' site.
    // https://www.sigmaaldrich.com/life-science/cell-biology.html

    public class Ingredient : LabElement
    {
        // Enums 

        public enum ADME_ToxType
        {
            
        };

        public enum AntibodyType
        {
           
        };

        public enum ProteinAssayType
        {
            Lysyle_Oxidase,
            Amilase,
        };

        public enum ReagentType
        {
            
        };

        public enum CellCultureType
        {
            Fibroblast,
            Endothelial,
            Epithelial,
            Neuron,
        };

        public enum GenType
        {
            DNA,
            RNA,
            TF,
        };

        public enum BufferType
        {

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

        public class ProteinAssay : Ingredient
        {
            ProteinAssayType m_type;
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
