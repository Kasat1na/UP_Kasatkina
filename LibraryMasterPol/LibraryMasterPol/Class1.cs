using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMasterPol
{
    public class MaterialCalculator
    {
        // Коэффициенты типа продукции и процент брака для каждого типа продукции и материала
        private const double ProductType1Coefficient = 1.2;
        private const double ProductType2Coefficient = 1.5;

        private const double MaterialType1DefectRate = 0.05; 
        private const double MaterialType2DefectRate = 0.1;  

        public static int CalculateMaterialRequired(int productId, int materialId, int productCount, double productParam1, double productParam2)
        {
            // проверка на корректн
            if (productId < 1 || productId > 2 || materialId < 1 || materialId > 2)
            {
                return -1;
            }

            // выбор коэффициента в зависимости от типа продукции
            double productCoefficient;
            if (productId == 1)
            {
                productCoefficient = ProductType1Coefficient;
            }
            else
            {
                productCoefficient = ProductType2Coefficient;
            }
            double defectRate; // выбор процента брака в зависимости от типа материала
            if (materialId == 1)
            {
                defectRate = MaterialType1DefectRate;
            }
            else
            {
                defectRate = MaterialType2DefectRate;
            }
            double materialPerProduct = productParam1 * productParam2 * productCoefficient; // материал на 1 единицу продукции
            double materialWithDefect = materialPerProduct * (1 + defectRate);// проуент брака
            int totalMaterial = (int)Math.Ceiling(materialWithDefect * productCount);  // общее колво
            return totalMaterial;
        }
    }
}
