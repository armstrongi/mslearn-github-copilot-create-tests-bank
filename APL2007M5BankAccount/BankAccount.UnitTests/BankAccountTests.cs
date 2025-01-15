using BankAccountApp;
using System;
using Xunit;

namespace BankAccountUnitTests
{
    public class BankAccountTests
    {
        [Fact]
        public void CreateAccount_ValidData()
        {
            // Arrange
            var accountNumber = "123";
            var initialBalance = 1000;
            var accountHolderName = "John Doe";
            var accountType = "Savings";
            var dateOpened = DateTime.Now;

            // Act
            var account = new BankAccount(accountNumber, initialBalance, accountHolderName, accountType, dateOpened);

            // Assert
            Assert.Equal(accountNumber, account.AccountNumber);
            Assert.Equal(initialBalance, account.Balance);
            Assert.Equal(accountHolderName, account.AccountHolderName);
            Assert.Equal(accountType, account.AccountType);
            Assert.Equal(dateOpened, account.DateOpened);
        }

        [Fact]
        public void CreateAccount_InitialBalanceZero()
        {
            // Arrange
            var accountNumber = "124";
            var initialBalance = 0;
            var accountHolderName = "Jane Doe";
            var accountType = "Checking";
            var dateOpened = DateTime.Now;

            // Act
            var account = new BankAccount(accountNumber, initialBalance, accountHolderName, accountType, dateOpened);

            // Assert
            Assert.Equal(accountNumber, account.AccountNumber);
            Assert.Equal(initialBalance, account.Balance);
            Assert.Equal(accountHolderName, account.AccountHolderName);
            Assert.Equal(accountType, account.AccountType);
            Assert.Equal(dateOpened, account.DateOpened);
        }

        [Fact]
        public void CreateAccount_InitialBalanceNegative()
        {
            // Arrange
            var accountNumber = "125";
            var initialBalance = -100;
            var accountHolderName = "Jim Doe";
            var accountType = "Business";
            var dateOpened = DateTime.Now;

            // Act
            var account = new BankAccount(accountNumber, initialBalance, accountHolderName, accountType, dateOpened);

            // Assert
            Assert.Equal(accountNumber, account.AccountNumber);
            Assert.Equal(initialBalance, account.Balance);
            Assert.Equal(accountHolderName, account.AccountHolderName);
            Assert.Equal(accountType, account.AccountType);
            Assert.Equal(dateOpened, account.DateOpened);
        }
        
        [Fact]
        public void Credit_PositiveAmount()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Credit(200);

            // Assert
            Assert.Equal(1200, account.Balance);
        }

        [Fact]
        public void Credit_ZeroAmount()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Credit(0));
            Assert.Equal("Credit amount must be positive.", exception.Message);            
        }

        [Fact]
        public void Credit_NegativeAmount()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Credit(0));
            Assert.Equal("Credit amount must be positive.", exception.Message);          
        }

        [Fact]
        public void Debit_AmountWithinBalance()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Debit(200);

            // Assert
            Assert.Equal(800, account.Balance);
        }

        [Fact]
        public void Debit_AmountExceedingBalance()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => account.Debit(1200));
            Assert.Equal("Insufficient balance for debit.", exception.Message);
        }

        [Fact]
        public void Debit_ZeroAmount()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Debit(0));
            Assert.Equal("Debit amount must be positive.", exception.Message);
        }

        [Fact]
        public void Debit_NegativeAmount()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => account.Debit(-200));
            Assert.Equal("Debit amount must be positive.", exception.Message);
        }

        [Fact]
        public void Transfer_SuccessfulTransfer()
        {
            // Arrange
            var account1 = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Doe", "Savings", DateTime.Now);

            // Act
            account1.Transfer(account2, 200);

            // Assert
            Assert.Equal(800, account1.Balance);
            Assert.Equal(700, account2.Balance);
        }

        [Fact]
        public void Transfer_InsufficientBalance()
        {
            // Arrange
            var account1 = new BankAccount("123", 100, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Doe", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => account1.Transfer(account2, 200));
            Assert.Equal("Insufficient balance for transfer.", exception.Message);
        }

        [Fact]
        public void Transfer_ExceedsLimitForDifferentOwners()
        {
            // Arrange
            var account1 = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Smith", "Savings", DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => account1.Transfer(account2, 600));
            Assert.Equal("Transfer amount exceeds maximum limit for different account owners.", exception.Message);
        }

        [Fact]
        public void GetBalance_InitialBalance()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            var balance = account.GetBalance();

            // Assert
            Assert.Equal(1000, balance);
        }

        [Fact]
        public void GetBalance_AfterCredit()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Credit(200);
            var balance = account.GetBalance();

            // Assert
            Assert.Equal(1200, balance);
        }

        [Fact]
        public void GetBalance_AfterDebit()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Debit(200);
            var balance = account.GetBalance();

            // Assert
            Assert.Equal(800, balance);
        }

        [Fact]
        public void CalculateInterest_PositiveBalance()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var expectedInterest = 50; // Assuming 5% interest rate

            // Act
            var interest = account.CalculateInterest(0.05);

            // Assert
            Assert.Equal(expectedInterest, interest);
        }

        [Fact]
        public void CalculateInterest_ZeroBalance()
        {
            // Arrange
            var account = new BankAccount("123", 0, "John Doe", "Savings", DateTime.Now);
            var expectedInterest = 0;

            // Act
            var interest = account.CalculateInterest(0.05);

            // Assert
            Assert.Equal(expectedInterest, interest);
        }

        [Fact]
        public void CalculateInterest_NegativeBalance()
        {
            // Arrange
            var account = new BankAccount("123", -1000, "John Doe", "Savings", DateTime.Now);
            var expectedInterest = 0; // Assuming no interest on negative balance

            // Act
            var interest = account.CalculateInterest(0.05);

            // Assert
            Assert.Equal(expectedInterest, interest);
        }

        [Fact]
        public void CalculateInterest_HighInterestRate()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var expectedInterest = 200; // Assuming 20% interest rate

            // Act
            var interest = account.CalculateInterest(0.20);

            // Assert
            Assert.Equal(expectedInterest, interest);
        }

        [Fact]
        public void CalculateInterest_LowInterestRate()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var expectedInterest = 10; // Assuming 1% interest rate

            // Act
            var interest = account.CalculateInterest(0.01);

            // Assert
            Assert.Equal(expectedInterest, interest);
        }
    }
}