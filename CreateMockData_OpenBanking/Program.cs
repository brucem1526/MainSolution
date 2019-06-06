using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using CreateMockData_OpenBanking.Models;
using System;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace CreateMockData_OpenBanking
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 1; i <= 100; i++)
            //{
            //    MockAccountsData();
            //    DelayTactic();
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    MockPartiesData(i);
            //    DelayTactic();
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    MockBalancesData(i);
            //    DelayTactic();
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    MockTransactionData(i);
            //    DelayTactic();
            //}
        }

        public static void DelayTactic()
        {
            int milliseconds = 1000;
            Thread.Sleep(milliseconds);
        }

        #region Parties
        // Account Id's
        public static long[] GetAccountNumber()
        {
            long[] accountIds = new long[100] { 1011115182,1012320214
,1012621201
,1014498177
,1015211131
,1019202022
,1020201111
,1022115169
,1032061624
,1052313102
,1113169410
,1115181122
,1216312121
,1219122419
,1231230161
,1252200169
,1311184223
,1315225162
,1316201811
,1317022817
,1322231702
,1417172125
,1421692241
,1423902322
,1522102422
,1523141862
,1573122512
,1692511013
,1710201414
,1742114151
,1782231761
,1815251491
,1824412221
,1902912162
,1911515410
,1911714823
,1931821862
,1946131761
,2011652123
,2011812221
,2015251219
,2015621251
,2020122079
,2061592913
,2078019158
,2113112216
,2113122095
,2121613391
,2122541424
,2155130241
,2201391241
,2219412220
,2220216111
,2224140324
,2224256148
,2315206511
,2316242067
,2317215121
,2320023019
,2361515025
,2412202131
,2423137128
,2430116724
,2452313211
,2511230180
,2513414251
,2514952210
,2517303194
,2518151917
,2524971712
,2551110202
,2561025182
,3117181916
,3131211177
,3196131721
,3211781421
,3258211601
,4159201421
,4161319922
,5199615201
,5223821181
,5820102524
,6016194122
,6123122461
,6151023191
,6185208211
,7121425281
,7160177212
,7202518111
,7242416012
,8122412472
,8151081810
,8186121522
,8201524242
,9136222522
,9142311172
,9242110242
,9317420251
,9522146162
,9818810411 };

            return accountIds;

        }

        enum EmailDomains
        {
            gmail = 0,
            yahoo = 1,
            outlook = 2,
            hotmail = 3,
            webmail = 4
        }

        enum Provinces
        {
            Eastern_Cape = 0,
            Free_State = 1,
            Gauteng = 2,
            KwaZulu_Natal = 3,
            Limpopo = 4,
            Mpumalanga = 5,
            Northern_Cape = 6,
            North_West = 7,
            WesternC_ape = 8
        }

        public static string GeneratePartyNumber(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            var number = 0;
            for (int i = 0; i < size; i++)
            {
                number = Convert.ToInt32(Math.Floor(26 * random.NextDouble()));
                builder.Append(number);
            }

            return builder.ToString().Substring(0, 11);
        }

        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            var name = Name.Substring(0, Name.Length / 2);
            var surname = Name.Substring((Name.Length / 2), (Name.Length / 2));
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower()) + " " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(surname.ToLower());
        }

        public static string GenerateEmailAddress(string name)
        {
            var random = new Random();
            var number = random.Next(0, 4);
            var domain = (EmailDomains)Enum.Parse(typeof(EmailDomains), number.ToString(), true);
            var email = name.Split(' ');
            return email[0] + "." + email[1] + "@" + domain + ".com";
        }

        public static string GeneratePhoneNumber(int length)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            var number = 0;
            for (int i = 0; i < length; i++)
            {
                number = Convert.ToInt32(Math.Floor(26 * random.NextDouble()));
                builder.Append(number);
            }

            return "0" + builder.ToString().Substring(0, 9);
        }

        public static string GenerateMobileNumber(int length)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            var number = 0;
            for (int i = 0; i < length; i++)
            {
                number = Convert.ToInt32(Math.Floor(26 * random.NextDouble()));
                builder.Append(number);
            }

            return "0" + builder.ToString().Substring(0, 9);
        }

        public static string GenerateAddress(int counter)
        {
            return "This is an address..." + counter;
        }

        public static string GenerateAddressLine()
        {
            Random random = new Random();
            return "This is an address LINE..." + random.Next(0, 100);
        }

        public static string GenerateRandomName()
        {
            Random r = new Random();
            var len = r.Next(5, 20);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        public static string GenerateBuildingNumber()
        {
            var random = new Random();
            return random.Next(1000, 4000).ToString();
        }

        public static string GeneratePostCode()
        {
            var random = new Random();
            return random.Next(4000, 8000).ToString();
        }

        public static string GenerateProvince()
        {
            var random = new Random();
            var number = random.Next(0, 8);
            var province = (Provinces)Enum.Parse(typeof(Provinces), number.ToString(), true);
            var newProvince = province.ToString().Split('_');
            if (newProvince.Length == 1)
            {
                return newProvince[0];
            }
            else
            {
                return newProvince[0] + " " + newProvince[1];
            }
        }

        private static void MockPartiesData(int counter)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var insertCommand = @"INSERT INTO [dbo].[Parties]
                                   ([AccountId],[Name],[EmailAddress],[Mobile],[Address],[Type])
                                    VALUES
                                    (@AccountId,@Name,@EmailAddress,@Mobile,@Address,@Type)";

                var AccountId = GetAccountNumber()[counter];
                var Name = GenerateName(11);
                var EmailAddress = GenerateEmailAddress(Name);
                var Mobile = GenerateMobileNumber(9);
                var Address = GenerateAddress(counter);
                var Type = "Sole";



                var party = new Party()
                {
                    AccountId = AccountId,
                    Name = Name,
                    EmailAddress = EmailAddress,
                    Mobile = Mobile,
                    Address = Address,
                    Type = Type,

                };

                var affectedRows = db.Execute(insertCommand, party);

                if (affectedRows > 0)
                {
                    // Part Saved
                    Console.WriteLine("Party:  " + Name + "  inserted into database...");
                    counter++;
                }
                else
                {
                    // Party Not Saved
                    Console.WriteLine("Party:  " + Name + " an error occurred....");
                }

            }
        }


        #endregion

        #region Accounts

        public static string GenerateAccountNumber()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            var number = 0;
            for (int i = 0; i < 12; i++)
            {
                number = Convert.ToInt32(Math.Floor(26 * random.NextDouble()));
                builder.Append(number);
            }

            var accountNumber = builder.ToString().Substring(0, 10);

            if (accountNumber.Length != 10) return string.Empty;

            return accountNumber;
        }

        enum AccountSubType
        {
            Savings = 0,
            Credit = 1,
            Insure = 2,
        }

        public static string GenerateAccountSubType()
        {
            var random = new Random();
            var number = random.Next(0, 3);
            var accountType = (AccountSubType)Enum.Parse(typeof(AccountSubType), number.ToString(), true);
            return accountType.ToString();
           
        }

        public static void MockAccountsData()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var insertCommand = @"INSERT INTO [dbo].[Accounts]
                                   ([AccountId],[AccountType],[AccountSubType])
                                    VALUES
                                    (@AccountId,@AccountType,@AccountSubType)";

                var AccountId =  (GenerateAccountNumber() == string.Empty ? GenerateAccountNumber() : GenerateAccountNumber());
                var AccountType = "Global One";
                var AccountSubType = GenerateAccountSubType();

                var account = new Account()
                {
                    AccountId = Convert.ToInt64(AccountId),
                    AccountType = AccountType,
                    AccountSubType = GenerateAccountSubType()
                };

                var affectedRows = db.Execute(insertCommand, account);

                if (affectedRows > 0)
                {
                    // Part Saved
                    Console.WriteLine("Account:  " + AccountId + "  inserted into database...");
                }
                else
                {
                    // Party Not Saved
                    Console.WriteLine("Account:  " + AccountId + " an error occurred....");
                }

            }
        }

        #endregion

        #region Balances
   
        enum CreditDebit
        {
            Credit = 0,
            Debit = 1,
        }

        public static string GenerateCreditDebit()
        {
            var random = new Random();
            var number = random.Next(0, 2);
            var creditDebit = (AccountSubType)Enum.Parse(typeof(AccountSubType), number.ToString(), true);
            return creditDebit.ToString();

        }

        public static decimal GenerateAmount()
        {
            var rand = new Random();
            var item = new decimal(rand.NextDouble());
            return 123456 * item;
        }

        private static void MockBalancesData(int counter)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var insertCommand = @"INSERT INTO [dbo].[Balances]
                                   ([AccountId],[CreditDebitIndicator],[Type],[Amount],[Date])
                                    VALUES
                                    (@AccountId,@CreditDebitIndicator,@Type,@Amount,@Date)";

                var AccountId = GetAccountNumber()[counter];
                var CreditDebitIndicator = GenerateCreditDebit();
                var Type = "Available";
                var Amount = GenerateAmount();
                var Date = DateTime.Now;

                var party = new Balance()
                {
                    AccountId = AccountId,
                    CreditDebitIndicator = CreditDebitIndicator,
                    Type = Type,
                    Amount = Math.Round(Amount, 2),
                    Date = Date
                };

                var affectedRows = db.Execute(insertCommand, party);

                if (affectedRows > 0)
                {
                    // Part Saved
                    Console.WriteLine("Balance:  " + Amount + "  inserted into database...");
                    counter++;
                }
                else
                {
                    // Party Not Saved
                    Console.WriteLine("Balance:  " + Amount + " an error occurred....");
                }

            }
        }


        #endregion

        #region Transaction
        enum MerchantCategoryName
        {
            Groceries = 0,
            Car = 1,
            Home = 2,
            Entertainment = 3,
            Loan = 4
        }

        public static string GenerateMerchantCategoryName()
        {
            var random = new Random();
            var number = random.Next(0, 2);
            var merchantCategoryName = (MerchantCategoryName)Enum.Parse(typeof(MerchantCategoryName), number.ToString(), true);
            return merchantCategoryName.ToString();

        }

        private static void MockTransactionData(int counter)
        {


            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var insertCommand = @"INSERT INTO [dbo].[Transactions]
                                   ([AccountId],[Status],[CreditDebitIndicator],[BookingDateTime],[Balance],[Merchant],[MerchantCategoryName])
                                    VALUES
                                    (@AccountId,@Status,@CreditDebitIndicator,@BookingDateTime,@Balance,@Merchant,@MerchantCategoryName)";

                var AccountId = GetAccountNumber()[counter];
                var Status = "Approved";
                var CreditDebitIndicator = GenerateCreditDebit();
                var BookingDateTime = DateTime.Now;
                var Balance = GenerateAmount();
                var Merchant = GenerateRandomName();
                var MerchantCategoryName = GenerateMerchantCategoryName();


                var transaction = new Transaction()
                {
                    AccountId = AccountId,
                    Status = Status,
                    CreditDebitIndicator = CreditDebitIndicator,
                    BookingDateTime = BookingDateTime,
                    Balance = Math.Round(Balance, 2),
                    Merchant = Merchant,
                    MerchantCategoryName = MerchantCategoryName
                };

                var affectedRows = db.Execute(insertCommand, transaction);

                if (affectedRows > 0)
                {
                    // Part Saved
                    Console.WriteLine("Transaction:  " + AccountId + "  inserted into database...");
                    counter++;
                }
                else
                {
                    // Party Not Saved
                    Console.WriteLine("Transaction:  " + AccountId + " an error occurred....");
                }

            }
        }


        #endregion
    }
}
