using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlanner.LabElements.Ingredients;

namespace XPlanner.LabElements.Vessels
{
    public enum SyringeType
    {
        WithNeedle,
        WithoutNeedle
    };

    public enum CoverslipType
    {
        Glass,
        Silicon,
        Plastic
    };

    public enum DishType
    {
        Matek,
        Petri
    };

    public class Vessel : LabElement
    {
        List<KeyValuePair<Ingredient, double>> m_Ingredients;
        double m_Volume;

        public double Volume
        {
            get
            {
                return m_Volume;
            }
        }

        public void AddIngredient(Ingredient ingredient, double amount)
        {
            if (ingredient != null)
            {
                m_Ingredients.Add(new KeyValuePair<Ingredient, double>(ingredient, amount));
            }
        }

        public void RemoveIngredient(Ingredient ingredient, double amount = 0)
        {
            if (ingredient != null)
            {
                if (amount == 0)
                {
                    try
                    {
                        KeyValuePair<Ingredient, double> removedIngredient = m_Ingredients.Find(x => x.Key == ingredient);
                        if (default(KeyValuePair<Ingredient, double>).Equals(removedIngredient))
                        {
                            m_Ingredients.Remove(removedIngredient);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    try
                    {
                        KeyValuePair<Ingredient, double> updatedIngredient = m_Ingredients.Find(x => x.Key == ingredient);
                        if (default(KeyValuePair<Ingredient, double>).Equals(updatedIngredient))
                        {
                            m_Ingredients.Remove(updatedIngredient);
                            m_Ingredients.Add(new KeyValuePair<Ingredient, double>(ingredient, amount));
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

    }

    public class Dish: Vessel
    {
        DishType m_Type;
    }

    public class Falcon : Vessel
    {
        
    }

    public class Syringe : Vessel
    {
        SyringeType m_Type;
    }

    public class Coverslip : Vessel
    {
        CoverslipType m_Type;
        string m_Shape;
    }

    public class Eppendorf : Vessel
    {

    }

    public class VesselArray: LabElement
    {
        List<Vessel> m_Vessels;
    }
}
