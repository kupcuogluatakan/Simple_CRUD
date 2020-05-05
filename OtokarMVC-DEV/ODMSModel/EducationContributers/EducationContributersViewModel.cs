namespace ODMSModel.EducationContributers
{
    public class EducationContributersViewModel : ModelBase
    {
        public EducationContributersViewModel()
        {
        }


        public bool IsRequestRoot { get; set; }

        public string EducationCode { get; set; }

        public int RowNumber { get; set; }

        public string TCIdentity { get; set; }

        public string FullName { get; set; }

        public string WorkingCompany { get; set; }

        public int DealerId { get; set; }

        public string Grade { get; set; }

    }
}
