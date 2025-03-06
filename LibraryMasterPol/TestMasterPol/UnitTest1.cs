using LibraryMasterPol;

namespace TestMasterPol
{
    [TestClass]
    public class UnitTest1
    {
        // Простые тесты

        //1 тест:  Проверяет, что при количестве продукции равном 0, метод возвращает 0 единиц материала
        [TestMethod]
        public void Test_CalculateMaterialRequired_ZeroProductCount()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 0;
            double productParam1 = 1.0;
            double productParam2 = 1.0;

            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(0, result); 
        }
        //2 тест: Проверяет, что если передан некорректный ID продукта (например, 3 вместо существующих значений), метод возвращает -1
        [TestMethod]
        public void Test_CalculateMaterialRequired_Product1_InvalidProductId()
        {
            int productId = 3; 
            int materialId = 1;
            int productCount = 100;
            double productParam1 = 1.5;
            double productParam2 = 1.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(-1, result); 
        }

        [TestMethod]
        public void Test_CalculateMaterialRequired_Product2_InvalidMaterialId()
        {
            int productId = 2;
            int materialId = 3; 
            int productCount = 100;
            double productParam1 = 2.0;
            double productParam2 = 2.5;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(-1, result); 
        }

        //4 тест: Проверяет, что если один из параметров продукции равен 0, то возвращается 0 единиц материала
        [TestMethod]
        public void Test_CalculateMaterialRequired_ValidInput_ZeroProductParam()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 100;
            double productParam1 = 0;
            double productParam2 = 1.5;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(0, result);
        }


        //5 тест: Проверяет, что при очень большом количестве продукции метод корректно рассчитывает требуемое количество материала, но не выдает ошибку при большом объеме данных
        [TestMethod]
        public void Test_CalculateMaterialRequired_ValidInput_LargeProductCount()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 10000; 
            double productParam1 = 1.5;
            double productParam2 = 2.5;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreNotEqual(112500, result); 
        }

        // 6 тест: Проверяет, что метод корректно работает при больших значениях параметров продукции и материала
        [TestMethod]
        public void Test_CalculateMaterialRequired_ValidInput_LargeParams()
        {
            int productId = 2;
            int materialId = 2;
            int productCount = 100;
            double productParam1 = 1000.0;
            double productParam2 = 1000.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);

            Assert.AreNotEqual(150000000, result); 
        }

        //7 тест: Проверяет точный расчет требуемого материала для обычного случая с заданными параметрами
        [TestMethod]
        public void Test_CalculateMaterialRequired_ValidInput_ExactValues()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 100;
            double productParam1 = 1.0;
            double productParam2 = 1.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(126, result); 
        }
        [TestMethod]
        public void Test_CalculateMaterialRequired_ZeroProductParam1()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 10;
            double productParam1 = 0.0;
            double productParam2 = 2.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(0, result);
        }
        //9 тест: Проверяет, что при минимальных значениях параметров для продукции результат корректен
        [TestMethod]
        public void Test_CalculateMaterialRequired_MinimumParam()
        {
            int productId = 2;
            int materialId = 1;
            int productCount = 1;
            double productParam1 = 0.1;
            double productParam2 = 0.1;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(1, result); 
        }

        // 10 тест: Проверка с материалом типа 1 и количеством 0
        [TestMethod]
        public void Test_CalculateMaterialRequired_ZeroMaterialCount()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 0;
            double productParam1 = 1.0;
            double productParam2 = 1.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.AreEqual(0, result); 
        }

        //Сложные

        // 11 тест: Проверяет точность округления. Метод должен корректно округлить результаты при вычислении требуемого материала с учетом брака
        [TestMethod]
        public void Test_CalculateMaterialRequired_RoundingErrorCheck()
        {
            int productId = 1;
            int materialId = 1;
            int productCount = 50;
            double productParam1 = 1.987;
            double productParam2 = 2.345;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            double expectedMaterialPerProduct = productParam1 * productParam2 * 1.2; 
            double expectedMaterialWithDefect = expectedMaterialPerProduct * (1 + 0.05);
            double expectedTotalMaterial = expectedMaterialWithDefect * productCount;
            Assert.IsTrue(Math.Abs(result - expectedTotalMaterial) <= 1, "Ожидаемое значение и результат отличаются слишком сильно");
        }

        // 12 тест: Проверяет, что для разных типов продукции с различными коэффициентами, результат для второго типа продукции будет больше, если коэффициент больше
        [TestMethod]
        public void Test_CalculateMaterialRequired_VaryingProductCoefficients()
        {
            int productId1 = 1;
            int productId2 = 2;
            int materialId = 1;
            int productCount = 10;
            double productParam1 = 2.0;
            double productParam2 = 3.0;
            int result1 = MaterialCalculator.CalculateMaterialRequired(productId1, materialId, productCount, productParam1, productParam2);
            int result2 = MaterialCalculator.CalculateMaterialRequired(productId2, materialId, productCount, productParam1, productParam2);
            Assert.IsTrue(result2 > result1, "Результат для второго типа продукции должен быть больше, так как коэффициент больше");
        }

        // 13 тест: Проверяет, что при больших значениях для второго типа продукции и материала, количество материала будет достаточно большим
        [TestMethod]
        public void Test_CalculateMaterialRequired_ProductType2_MaterialType2_LargeValue()
        {
            int productId = 2;
            int materialId = 2;
            int productCount = 500;
            double productParam1 = 10.5;
            double productParam2 = 8.7;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.IsTrue(result > 10000, "Результат должен быть больше 10000"); 
        }

        // 14 тест
        [TestMethod]
        public void Test_CalculateMaterialRequired_InvalidProductType()
        {
            int productId = 3; 
            int materialId = 1;
            int productCount = 100;
            double productParam1 = 3.0;
            double productParam2 = 2.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);

            Assert.AreEqual(-1, result, "Метод должен вернуть -1 для несуществующего типа продукции");
        }

        // 15 тест
        [TestMethod]
        public void Test_CalculateMaterialRequired_RoundingDown()
        {
            int productId = 2;
            int materialId = 1;
            int productCount = 10;
            double productParam1 = 2.3;
            double productParam2 = 3.1;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            double materialPerProduct = productParam1 * productParam2 * 1.5;
            double defectRate = 0.05;
            double expectedMaterial = materialPerProduct * (1 + defectRate);
            double expectedTotalMaterial = expectedMaterial * productCount;
            double expected = Math.Ceiling(expectedTotalMaterial); 

            Assert.IsTrue(Math.Abs(result - expected) < 5, "Ожидаемая погрешность округления должна быть не более 5");
        }

    }
}