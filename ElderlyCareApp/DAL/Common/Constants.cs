namespace DAL.Common
{
    public static class Constants
    {
        #region Role

        public const string Admin = "Admin";
        public const string Caregiver = "Caregiver";
        public const string Representative = "Representative";
        public const string Customer = "Customer";

        #endregion

        #region Gender

        public const string Male = "Male";
        public const string Female = "Female";

        #endregion

        #region ResourceCategory

        public const string SeniorFitnessPlans = "Senior Fitness Plans";
        public const string MobilityAndFlexibilityWorkouts = "Mobility & Flexibility Workouts";
        public const string LowImpactExercises = "Low-Impact Exercises";
        public const string StrengtheningExercisesForSeniors = "Strengthening Exercises for Older Adults";
        public const string HealthyMealPlanningForSeniors = "Healthy Meal Planning for Seniors";
        public const string AgeAppropriateDiets = "Age-Appropriate Diets";
        public const string CommonHealthConcernsInOlderAdults = "Common Health Concerns in Older Adults";

        #endregion

        #region BookingStatus

        public const string InReview = "InReview";
        public const string InProccess = "InProccess";
        public const string Cancelled = "Cancelled";
        public const string Confirmed = "Confirmed";
        public const string Completed = "Completed";

        #endregion

        #region ReceiptStatus

        public const string Pending = "Pending";
        public const string Paid = "Paid";

        #endregion

        #region PaymentMethod

        public const string Cash = "InCash";
        public const string BankTransfer = "BankTransfer";

        #endregion

    }
}
