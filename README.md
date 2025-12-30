# Mortgage Calculator Web Application

A mortgage loan calculator web application built with C# and ASP.NET Core MVC. This application helps users calculate monthly mortgage payments, determine loan eligibility, and understand the financial implications of their home purchase.

## Project Status

This project is actively under development as part of a Computer Science capstone.
It originated as a console-based mortgage calculator and is being incrementally
refactored and expanded into a full ASP.NET Core MVC web application.

## Features

- **Mortgage Payment Calculator**: Calculate monthly mortgage payments including principal and interest
- **Loan Eligibility Assessment**: Determine if the borrower qualifies based on income-to-payment ratio
- **Comprehensive Fee Calculation**: Includes:
  - Origination fees
  - Closing costs
  - Property tax
  - Homeowners insurance
  - HOA fees
  - Private Mortgage Insurance (PMI) when applicable
- **Multiple Loan Terms**: Support for different loan term lengths
- **User-Friendly Interface**: Clean, responsive design for easy data entry
- **Results Flow**: Temporarily preserves loan input to support recalculation and adjustment

## Technology Stack

- **Framework**: ASP.NET Core MVC
- **.NET Version**: .NET 10
- **Architecture**: Clean architecture with separate domain layer

## Project Structure

MortgageLoanCalculator/
- MortgageLoanCalculator/            # ASP.NET Core MVC web app
  - Controllers/
    - HomeController.cs
    - LoanController.cs
  - Models/                           # ViewModels
    - MortgageResultViewModel.cs
    - ErrorViewModel.cs
  - Views/
    - Home/
    - Loan/
    - Shared/
  - Program.cs

- MortgageLoanCalculator.Domain/     # Domain / business logic
  - Loan.cs
  - LoanCalculator.cs
  - LoanDecisionService.cs
  - MortgageCalculatorConstants.cs

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- A code editor (Visual Studio 2025, Visual Studio Code, or Rider)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/padgettanna/Mortgage-Calculator-Web.git
cd Mortgage-Calculator-Web
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
cd MortgageLoanCalculator
dotnet run
```

5. Open your browser and navigate to `https://localhost:5001` or the URL shown in the terminal

## Usage

1. **Enter Borrower Information**:
   - First and Last Name
   - Annual Income

2. **Enter Loan Details**:
   - Purchase Price
   - Down Payment
   - Annual Interest Rate
   - Loan Term (years)
   - Annual HOA Fees (if applicable)

3. **Calculate**: Click the calculate button to see:
   - Monthly payment breakdown (Principal & Interest)
   - Total monthly payment including all fees
   - Payment-to-income ratio
   - Loan eligibility decision
   - Equity percentage and value

4. **Reset**: Clear the form to start a new calculation

## Key Components

### LoanCalculator
Handles all mortgage calculation logic including:
- Loan amount with origination fees and closing costs
- Monthly payment (Principal & Interest)
- PMI calculations
- Property tax and homeowners insurance
- Total monthly payment

### LoanDecisionService
Evaluates loan eligibility based on debt-to-income ratio standards

### MortgageCalculatorConstants
Stores financial constants such as:
- Origination fee percentage
- Closing costs
- Property tax rates
- Insurance rates
- PMI thresholds

## Author

Anna Padgett

## Acknowledgments

Built with ASP.NET Core and modern web development best practices.
