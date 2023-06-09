﻿namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;
        private readonly IFrequentFlyerNumberValidator _validator;

        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator)
        {
            _validator = validator ?? throw new System.ArgumentNullException(nameof(validator));
        }



        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {

            var isValidFrequentFlyerNumber = _validator.IsValid(application.FrequentFlyerNumber);

            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }



            if (!isValidFrequentFlyerNumber) return CreditCardApplicationDecision.ReferredToHuman;


            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }
    }
}
