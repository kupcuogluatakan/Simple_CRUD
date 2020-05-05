using ODMSData;
using ODMSModel.ViewModel;

namespace ODMSBusiness
{
    public class TestBL : BaseBusiness
    {
        private TestData data = new TestData();

        public void TestDML()
        {
            data.DmlTest(new MultiLanguageContentViewModel());
        }

    }
}
