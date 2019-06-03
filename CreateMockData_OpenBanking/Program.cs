using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using CreateMockData_OpenBanking.Models;
using System;
using System.Text;
using System.Globalization;
using System.Linq;

namespace CreateMockData_OpenBanking
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i <= 100; i++)
            //{
            //    // MockPartiesData();
            //}
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
            Free_State =1,
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

        public static string GenerateAddress()
        {
            Random random = new Random();
            return "This is an address..." + random.Next(0, 100);
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
            }else
            {
                return newProvince[0] + " " + newProvince[1];
            }
        }
        private static void MockPartiesData()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var insertCommand = @"INSERT INTO [dbo].[Parties]
                                   ([PartyNumber],[PartyType],[Name],[EmailAddress],[Phone],[Mobile]
                                   ,[Address],[AddressType],[AddressLine],[StreetName],[BuildingNumber]
                                   ,[PostCode],[TownName],[CountrySubDivision],[Country])
                                    VALUES
                                    (@PartyNumber,@PartyType,@Name,@EmailAddress,@Phone,@Mobile,
                                     @Address,@AddressType,@AddressLine,@StreetName,@BuildingNumber,
                                    @PostCode,@TownName,@CountrySubDivision,@Country)";

                var partyNumber = GeneratePartyNumber(11);
                var partType = "Sole";
                var name = GenerateName(11);
                var email = GenerateEmailAddress(name);
                var phone = GeneratePhoneNumber(9);
                var mobile = GenerateMobileNumber(9);
                var address = GenerateAddress();
                var addressType = "Residential";
                var addressLine = GenerateAddressLine();
                var streetName = GenerateRandomName();
                var buildingNumber = GenerateBuildingNumber();
                var postCode = GeneratePostCode();
                var townName = GenerateRandomName();
                var countrySubDivision = GenerateProvince();
                var country = "South Africa";


                var party = new Party()
                {
                    PartyNumber = partyNumber,
                    PartyType = partType,
                    Name = name,
                    EmailAddress = email,
                    Phone = phone,
                    Mobile = mobile,
                    Address = address,
                    AddressLine = addressLine,
                    AddressType = addressType,
                    StreetName = streetName,
                    BuildingNumber = buildingNumber,
                    PostCode = postCode,
                    TownName = townName,
                    CountrySubDivision = countrySubDivision,
                    Country = country
                };

                var affectedRows = db.Execute(insertCommand, party);

                if (affectedRows > 0)
                {
                    // Part Saved
                    Console.WriteLine("Party:  " + partyNumber + "  inserted into database...");
                }
                else
                {
                    // Party Not Saved
                    Console.WriteLine("Party:  " + partyNumber + " an error occurred....");
                }

            }
        }
    }
}
