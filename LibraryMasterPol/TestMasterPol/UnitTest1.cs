using LibraryMasterPol;

namespace TestMasterPol
{
    [TestClass]
    public class UnitTest1
    {
        // ������� �����

        //1 ����:  ���������, ��� ��� ���������� ��������� ������ 0, ����� ���������� 0 ������ ���������
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
        //2 ����: ���������, ��� ���� ������� ������������ ID �������� (��������, 3 ������ ������������ ��������), ����� ���������� -1
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

        //4 ����: ���������, ��� ���� ���� �� ���������� ��������� ����� 0, �� ������������ 0 ������ ���������
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


        //5 ����: ���������, ��� ��� ����� ������� ���������� ��������� ����� ��������� ������������ ��������� ���������� ���������, �� �� ������ ������ ��� ������� ������ ������
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

        // 6 ����: ���������, ��� ����� ��������� �������� ��� ������� ��������� ���������� ��������� � ���������
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

        //7 ����: ��������� ������ ������ ���������� ��������� ��� �������� ������ � ��������� �����������
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
        //9 ����: ���������, ��� ��� ����������� ��������� ���������� ��� ��������� ��������� ���������
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

        // 10 ����: �������� � ���������� ���� 1 � ����������� 0
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

        //�������

        // 11 ����: ��������� �������� ����������. ����� ������ ��������� ��������� ���������� ��� ���������� ���������� ��������� � ������ �����
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
            Assert.IsTrue(Math.Abs(result - expectedTotalMaterial) <= 1, "��������� �������� � ��������� ���������� ������� ������");
        }

        // 12 ����: ���������, ��� ��� ������ ����� ��������� � ���������� ��������������, ��������� ��� ������� ���� ��������� ����� ������, ���� ����������� ������
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
            Assert.IsTrue(result2 > result1, "��������� ��� ������� ���� ��������� ������ ���� ������, ��� ��� ����������� ������");
        }

        // 13 ����: ���������, ��� ��� ������� ��������� ��� ������� ���� ��������� � ���������, ���������� ��������� ����� ���������� �������
        [TestMethod]
        public void Test_CalculateMaterialRequired_ProductType2_MaterialType2_LargeValue()
        {
            int productId = 2;
            int materialId = 2;
            int productCount = 500;
            double productParam1 = 10.5;
            double productParam2 = 8.7;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);
            Assert.IsTrue(result > 10000, "��������� ������ ���� ������ 10000"); 
        }

        // 14 ����
        [TestMethod]
        public void Test_CalculateMaterialRequired_InvalidProductType()
        {
            int productId = 3; 
            int materialId = 1;
            int productCount = 100;
            double productParam1 = 3.0;
            double productParam2 = 2.0;
            int result = MaterialCalculator.CalculateMaterialRequired(productId, materialId, productCount, productParam1, productParam2);

            Assert.AreEqual(-1, result, "����� ������ ������� -1 ��� ��������������� ���� ���������");
        }

        // 15 ����
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

            Assert.IsTrue(Math.Abs(result - expected) < 5, "��������� ����������� ���������� ������ ���� �� ����� 5");
        }

    }
}