
export function calculateLoanPayment(principal: number, weeklyPayment: number, term: number): {totalRepayment: number, totalInterest: number, establishmentFee: number} {
    const interestRate = 0.06; // 6% per annum
    const establishmentFeeRate = 0.06; // 1% of principal
    const totalPeriods = term * 12; // monthly frequency

    let roundedPeriods = 0.00;
    let periods = 0;
    
    const establishmentFee = principal * establishmentFeeRate;
    const totalInterest = (principal + establishmentFee) * (Math.pow(1 + interestRate / totalPeriods, totalPeriods) - 1);
    const totalRepayment = (principal + totalInterest + establishmentFee) * (interestRate / totalPeriods) / (1 - Math.pow(1 + interestRate / totalPeriods, -totalPeriods));

    // Convert weekly payment to monthly payment
    const monthlyPayment = weeklyPayment * 52 / 12;
    // Calculate the number of periods based on the monthly payment
    periods = -Math.log(1 - ((interestRate / totalPeriods) * (principal + totalInterest + establishmentFee) / monthlyPayment)) / Math.log(1 + (interestRate / totalPeriods));
    // Round up the number of periods to the nearest integer
    roundedPeriods = Math.ceil(periods);
  
    // Calculate the total repayment based on the rounded number of periods
    const totalRepaymentRounded = roundedPeriods * monthlyPayment;

    return {
      totalRepayment: totalRepaymentRounded,
      totalInterest: totalInterest + totalRepayment,
      establishmentFee: establishmentFee
    };
  }

